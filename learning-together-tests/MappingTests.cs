namespace learning_together_tests
{
    using AutoMapper;
    using learning_together_api.Data;
    using Xunit;

    public class MappingTests
    {
        [Fact]
        public void TestMappers()
        {
            Mapper.Initialize(m => m.AddProfile<AutoMapperProfile>());
            Mapper.AssertConfigurationIsValid();
        }
    }
}