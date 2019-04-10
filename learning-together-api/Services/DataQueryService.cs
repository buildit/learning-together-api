namespace learning_together_api.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Data;

    public abstract class DataQueryService<T> : IGetDataService<T>
        where T : IdEntity
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

        public abstract IEnumerable<T> FindByName(string name);
    }
}