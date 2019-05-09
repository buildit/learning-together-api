namespace learning_together_api.Services
{
    using Data;
    using pathways_common.Interfaces;

    public interface ICategoryService : IGetDataService<Category>, ISearchService<Category> { }
}