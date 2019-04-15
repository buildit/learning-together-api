namespace learning_together_api.Services
{
    using System;
    using System.Collections.Generic;
    using Data;

    public class CategoryService : DataQueryService<Category>, ICategoryService
    {
        public CategoryService(DataContext context)
            : base(context, context.Categories) { }

        public override IEnumerable<Category> FindByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}