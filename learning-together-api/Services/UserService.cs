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

            // check if username exists
            if (user == null)
            {
                return null;
            }

            // check if password is correct
            if (!SecurityService.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                return null;
            }

            // authentication successful
            return user;
        }

        public User Create(User user, string password)
        {
            // validation
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

        public void Update(User userParam, string password = null)
        {
            User user = this.context.Users.Find(userParam.Id);

            if (user == null)
            {
                throw new AppException("User not found");
            }

            if (userParam.Username != user.Username)
            {
                // username has changed so check if the new username is already taken
                if (this.context.Users.Any(x => x.Username == userParam.Username))
                {
                    throw new AppException($"Username {userParam.Username} is already taken");
                }
            }

            // update user properties
            user.FirstName = userParam.FirstName;
            user.LastName = userParam.LastName;
            user.Username = userParam.Username;

            // update password if it was entered
            if (!string.IsNullOrWhiteSpace(password))
            {
                SecurityService.CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }

            this.context.Users.Update(user);
            this.context.SaveChanges();
        }

        public void Delete(int id)
        {
            User user = this.context.Users.Find(id);
            if (user != null)
            {
                this.context.Users.Remove(user);
                this.context.SaveChanges();
            }
        }

        public override IEnumerable<User> FindByName(string name)
        {
            throw new NotImplementedException();
        }

        public User GetByIdWithIncludes(int id)
        {
            return this.context.Users.Where(u => u.Id == id)
                .Include(u => u.Location)
                .Include(u => u.Role)
                .Include(u => u.UserInterests).ThenInclude(ui => ui.Discipline)
                .FirstOrDefault();
        }
    }
}