namespace learning_together_api.Services
{
    using Data;
    using pathways_common.Interfaces.Services;

    public interface ILocationService : IGetDataService<Location>, ISearchService<Location>
    {
    }
}