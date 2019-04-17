namespace learning_together_api.Services
{
    using Data;

    public interface ICategoryService : IGetDataService<Category>, ISearchService<Category> { }
}