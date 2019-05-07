namespace learning_together_api.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Data;
    using Exceptions;
    using Microsoft.EntityFrameworkCore;

    public class UserService : DataQueryService<User>, IUserService
    {
        public UserService(DataContext context) : base(context, context.Users) { }

        public User Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return null;
            }

            User user = this.context.Users.SingleOrDefault(x => x.Username == username);

            if (user == null)
            {
                return null;
            }

            if (!SecurityService.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                return null;
            }

            return user;
        }

        public User Create(User user, string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new AppException("Password is required");
            }

            if (this.context.Users.Any(x => x.Username == user.Username))
            {
                throw new AppException($"Username {user.Username} is already taken");
            }

            SecurityService.CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            this.context.Users.Add(user);
            this.context.SaveChanges();

            return user;
        }

        public void Update(int userId, User userParam, string password = null)
        {
            if (userId != userParam.Id) throw new UnauthorizedAccessException();

            User user = this.context.Users.FirstOrDefault(u => u.Id == userParam.Id);

            if (user == null)
            {
                throw new AppException("User not found");
            }

            if (userParam.Username != user.Username)
            {
                if (this.context.Users.Any(x => x.Username == userParam.Username))
                {
                    throw new AppException($"Username {userParam.Username} is already taken");
                }
            }

            // update password if it was entered
            if (!string.IsNullOrWhiteSpace(password))
            {
                SecurityService.CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }

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

        public IEnumerable<User> Search(string search)
        {
            search = search.ToLower();
            return this.context.Users.Where(u => u.FirstName.ToLower().Contains(search) || u.LastName.ToLower().Contains(search));
        }
    }
}