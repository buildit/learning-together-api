namespace learning_together_api.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Data;

    public abstract class NamedSearchableService<T> : DataQueryService<T>
        where T : NamedEntity
    {
        protected NamedSearchableService(DataContext context, IEnumerable<T> collection) : base(context, collection) { }

        public IEnumerable<T> Search(string search)
        {
            return this.collection.Where(c => c.Name.Contains(search));
        }
    }
}