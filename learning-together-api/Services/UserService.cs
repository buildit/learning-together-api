namespace learning_together_api.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using pathways_common;
    using pathways_common.Extensions;

    public class UserService : LearningTogetherDataQueryService<User>, IUserService
    {
        public UserService(DataContext context) : base(context, context.Users)
        {
        }

        public User Create(User user)
        {
            if (string.IsNullOrEmpty(user.Username)) throw new AppException("Invalid username passed to the service");

            User existingUser = this.Retrieve(user.Username);

            Func<User> func;
            if (existingUser == null)
            {
                user.ImageUrl = IdentityExtensions.IdentityConstants.DefaultAvatar;
                EntityEntry<User> entityEntry = this.context.Users.Add(user);
                func = () => entityEntry.Entity;
            }
            else if (existingUser.DirectoryName != user.DirectoryName)
            {
                existingUser.DirectoryName = user.DirectoryName;
                func = () => existingUser;
            }
            else
            {
                throw new AppException($"A user with username {user.Username} already exists.");
            }

            this.context.SaveChanges();

            return func();
        }

        public void Update(User userParam)
        {
            User user = this.context.Users.FirstOrDefault(u => u.Id == userParam.Id);

            if (user == null) throw new AppException("User not found");

            // update user properties
            user.FirstName = userParam.FirstName;
            user.LastName = userParam.LastName;
            user.ImageUrl = userParam.ImageUrl;
            user.LocationId = userParam.LocationId;
            user.RoleId = userParam.RoleId;

            this.UpdateDisciplineAssociations(user.Id, userParam);

            this.context.Users.Update(user);
            this.context.SaveChanges();
        }

        public void Delete(int id)
        {
            User user = this.context.Users.FirstOrDefault(u => u.Id == id);
            if (user == null) return;

            user.Deactivated = true;
            this.context.SaveChanges();
        }

        public void Update(int userId, User userParam)
        {
            if (userId != userParam.Id) throw new UnauthorizedAccessException();

            this.Update(userParam);
        }

        public void Delete(int userId, int id)
        {
            if (userId != id) throw new UnauthorizedAccessException();

            this.Delete(id);
        }

        public User GetByIdWithIncludes(int id)
        {
            User first = this.context.Users.Where(u => u.Id == id)
                .Include(u => u.Location)
                .Include(u => u.Role)
                .Include(u => u.UserInterests).ThenInclude(ui => ui.Discipline)
                .First();

            this.context.Entry(first)
                .Collection(u => u.WorkshopsAttending)
                .Query()
                .Where(w => w.Workshop.Cancelled == false)
                .Include(w => w.Workshop)
                .Load();

            this.context.Entry(first)
                .Collection(u => u.WorkshopsTeaching)
                .Query()
                .Where(w => w.Cancelled == false)
                .Load();

            return first;
        }

        public User Retrieve(string username)
        {
            return this.context.Users.FirstOrDefault(u => u.Username == username);
        }

        public User RetrieveOrCreate(string username, string name)
        {
            User user = this.collection.FirstOrDefault(u => u.Username == username && u.DirectoryName == name);

            if (user != null || string.IsNullOrEmpty(username)) return user;

            user = new User(username, name);
            return this.Create(user);
        }

        public IEnumerable<User> Search(string search)
        {
            search = search.ToLower();
            return this.context.Users.Where(u => u.FirstName.ToLower().Contains(search) || u.LastName.ToLower().Contains(search));
        }

        private void UpdateDisciplineAssociations(int userId, User userParam)
        {
            IQueryable<UserInterest> existingInterests = this.context.UserInterests.Where(i => i.UserId == userId);

            if (existingInterests.Any()) this.context.UserInterests.RemoveRange(existingInterests);

            foreach (UserInterest newInterest in userParam.UserInterests)
            {
                newInterest.UserId = userId;
                this.context.UserInterests.Add(newInterest);
            }
        }
    }
}