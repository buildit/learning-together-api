namespace learning_together_api.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Data;
    using pathways_common.Entities;

    public abstract class NamedSearchableService<T> : DataQueryService<T>
        where T : NamedEntity
    {
        protected NamedSearchableService(DataContext context, IEnumerable<T> collection) : base(context, collection) { }

        public IEnumerable<T> Search(string search)
        {
            search = search.ToLower();
            return this.collection.Where(c => c.Name.ToLower().Contains(search));
        }
    }
}