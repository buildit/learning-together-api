namespace learning_together_tests
{
    using System;
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
            Mock<DbSet<User>> mockSet = new Mock<DbSet<User>>();
            Mock<DataContext> mockContext = new Mock<DataContext>();
            IQueryable<User> users = new List<User>().AsQueryable();

            mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(users.Provider);
            mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(users.Expression);
            mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(users.ElementType);
            mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(users.GetEnumerator());

            mockContext.Setup(m => m.Users).Returns(mockSet.Object);

            UserService service = new UserService(mockContext.Object);
            User user = new User();
            user.Username = "testUser";
            user.RoleId = 1;
            user.LocationId = 1;
            user.ImageUrl = "https://dogeplanet.com";
            user.FirstName = "First";
            user.LastName = "Last";
        }

        [Fact]
        public void UserService_TestUpdate_CapturesFields()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void UserService_TestUpdate_Secured()
        {
            throw new NotImplementedException();
        }
    }
}