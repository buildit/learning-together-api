namespace learning_together_api.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Data;
    using pathways_common.Interfaces;

    public abstract class DataQueryService<T> : IGetDataService<T>
        where T : IIdEntity
    {
        protected readonly IEnumerable<T> collection;
        protected readonly DataContext context;

        protected DataQueryService(DataContext context, IEnumerable<T> collection)
        {
            this.context = context;
            this.collection = collection;
        }

        public T Retrieve(int id)
        {
            return this.collection.FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<T> GetAll()
        {
            return this.collection;
        }
    }
}