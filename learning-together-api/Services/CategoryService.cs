namespace learning_together_api.Services
{
    using Data;

    public class CategoryService : NamedSearchableService<Category>, ICategoryService
    {
        public CategoryService(DataContext context)
            : base(context, context.Categories) { }
    }
}