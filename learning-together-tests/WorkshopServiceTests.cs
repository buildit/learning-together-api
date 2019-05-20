namespace learning_together_tests
{
    using System.Collections.Generic;
    using System.Linq;
    using learning_together_api.Data;
    using learning_together_api.Services;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Xunit;

    public class WorkshopServiceTests
    {
        [Fact]
        public void WorkshopService_Persist()
        {
            var mockSet = new Mock<DbSet<Workshop>>();
            var mockTopicSet = new Mock<DbSet<WorkshopTopic>>();
            Mock<DataContext> mockContext = new Mock<DataContext>();
            var collection = new List<Workshop>().AsQueryable();

            mockSet.As<IQueryable<Workshop>>().Setup(m => m.Provider).Returns(collection.Provider);
            mockSet.As<IQueryable<Workshop>>().Setup(m => m.Expression).Returns(collection.Expression);
            mockSet.As<IQueryable<Workshop>>().Setup(m => m.ElementType).Returns(collection.ElementType);
            mockSet.As<IQueryable<Workshop>>().Setup(m => m.GetEnumerator()).Returns(collection.GetEnumerator());

            var topics = new List<WorkshopTopic>().AsQueryable();

            mockTopicSet.As<IQueryable<WorkshopTopic>>().Setup(m => m.Provider).Returns(topics.Provider);
            mockTopicSet.As<IQueryable<WorkshopTopic>>().Setup(m => m.Expression).Returns(topics.Expression);
            mockTopicSet.As<IQueryable<WorkshopTopic>>().Setup(m => m.ElementType).Returns(topics.ElementType);
            mockTopicSet.As<IQueryable<WorkshopTopic>>().Setup(m => m.GetEnumerator()).Returns(topics.GetEnumerator());

            mockContext.Setup(m => m.Workshops).Returns(mockSet.Object);
            mockContext.Setup(m => m.WorkshopTopics).Returns(mockTopicSet.Object);

            var service = new WorkshopService(mockContext.Object);
            var entity = new Workshop();
            entity.Name = "Name";
            entity.RobinEventId = "RobinEvent";

            Workshop workshop = service.Create(entity);
        }
    }
}