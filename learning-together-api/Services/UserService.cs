namespace learning_together_api.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using pathways_common;
    using pathways_common.Authentication.Extensions;

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
                // Is actually a new user.
                user.ImageUrl = IdentityExtensions.IdentityConstants.DefaultAvatar;
                EntityEntry<User> entityEntry = this.context.Users.Add(user);
                func = () => entityEntry.Entity;
            }
            else if (existingUser.DirectoryName != user.DirectoryName || existingUser.OrganizationId != user.OrganizationId)
            {
                // Just needs AD info updated.
                existingUser.DirectoryName = user.DirectoryName;
                existingUser.OrganizationId = user.OrganizationId;
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
            User user = this.context.Users
                .Include(u => u.UserInterests)
                .FirstOrDefault(u => u.Id == userParam.Id);

            if (user == null) throw new AppException("User not found");

            // update user properties
            user.FirstName = userParam.FirstName;
            user.LastName = userParam.LastName;
            user.ImageUrl = userParam.ImageUrl;
            user.LocationId = userParam.LocationId;
            user.RoleId = userParam.RoleId;

            user.UserInterests.Clear();

            foreach (UserInterest newInterest in userParam.UserInterests)
            {
                newInterest.UserId = user.Id;
                user.UserInterests.Add(newInterest);
            }

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

        public void SetLogonTime(User user)
        {
            user.LastLogin = DateTime.Now;
            this.context.SaveChanges();
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
            return this.context.Users.FirstOrDefault(u => u.Username == username || u.OrganizationId == username);
        }

        public User RetrieveOrCreate(string graphEmail, string adUsername, string name)
        {
            User user = this.collection.FirstOrDefault(u => u.Username == graphEmail && u.Name == name && u.OrganizationId == adUsername);

            if (user != null || string.IsNullOrEmpty(adUsername)) return user;

            user = new User(graphEmail, adUsername, name);
            return this.Create(user);
        }

        public IEnumerable<User> Search(string search)
        {
            search = search.ToLower();
            return this.context.Users.Where(u => u.FirstName.ToLower().Contains(search) || u.LastName.ToLower().Contains(search));
        }
    }
}