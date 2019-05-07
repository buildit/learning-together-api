namespace learning_together_api.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Data;
    using Exceptions;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;

    public class UserService : DataQueryService<User>, IUserService
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

        public void Update(int userId, User userParam)
        {
            if (userId != userParam.Id) throw new UnauthorizedAccessException();

            User user = this.context.Users.FirstOrDefault(u => u.Id == userParam.Id);

            if (user == null) throw new AppException("User not found");

            if (userParam.Username != user.Username)
                if (this.context.Users.Any(x => x.Username == userParam.Username))
                    throw new AppException($"Username {userParam.Username} is already taken");

            // update user properties
            user.FirstName = userParam.FirstName;
            user.LastName = userParam.LastName;
            user.Username = userParam.Username;
            user.ImageUrl = userParam.ImageUrl;
            user.LocationId = userParam.LocationId;
            user.RoleId = userParam.RoleId;

            this.context.Users.Update(user);
            this.context.SaveChanges();
        }

        public void Delete(int userId, int id)
        {
            if (userId != id) throw new UnauthorizedAccessException();

            User user = this.context.Users.FirstOrDefault(u => u.Id == id);
            if (user == null) return;

            user.Deactivated = true;
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
            return this.context.Users.FirstOrDefault(u => u.Username == username);
        }

        public User RetrieveOrCreate(string username, string name)
        {
            User user = this.context.Users.FirstOrDefault(u => u.Username == username && u.DirectoryName == name);

            if (user != null || string.IsNullOrEmpty(username)) return user;

            user = new User(username, name);
            return this.Create(user);
        }

        public IEnumerable<User> Search(string search)
        {
            search = search.ToLower();
            return this.context.Users.Where(u => u.FirstName.ToLower().Contains(search) || u.LastName.ToLower().Contains(search));
        }
    }
}