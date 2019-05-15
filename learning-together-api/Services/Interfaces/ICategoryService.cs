namespace learning_together_api.Services
{
    using Data;
    using pathways_common.Interfaces.Services;

    public interface ICategoryService : IGetDataService<Category>, ISearchService<Category>
    {
    }
}