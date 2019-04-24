namespace learning_together_tests
{
    using System.Collections.Generic;
    using System.Linq;
    using learning_together_api.Data;
    using learning_together_api.Services;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Xunit;

    public class UserServiceTests
    {
        [Fact]
        public void UserService_TestCreate_HashAndSaltPw()
        {
            var mockSet = new Mock<DbSet<User>>();
            var mockContext = new Mock<DataContext>();
            var users = new List<User> { }.AsQueryable();

            mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(users.Provider);
            mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(users.Expression);
            mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(users.ElementType);
            mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(users.GetEnumerator());

            mockContext.Setup(m => m.Users).Returns(mockSet.Object);

            var service = new UserService(mockContext.Object);
            User user = new User();
            user.Username = "testUser";
            user.RoleId = 1;
            user.LocationId = 1;
            user.ImageUrl = "https://dogeplanet.com";
            user.FirstName = "First";
            user.LastName = "Last";
            service.Create(user, "password");

            mockSet.Verify(m => m.Add(It.Is<User>(u => u.PasswordHash != null && u.PasswordSalt != null)));
        }

        [Fact]
        public void UserService_TestUpdate_NewPassword()
        {
            // service.Update(user, "password");
        }

        [Fact]
        public void UserService_TestUpdate_NewUsername()
        {
            // service.Update(user, "password");
        }
    }
}