namespace learning_together_api.Services
{
    using Data;

    public interface ILocationService : IGetDataService<Location>, ISearchService<Location> { }
}