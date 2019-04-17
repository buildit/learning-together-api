namespace learning_together_api.Services
{
    using System.Collections.Generic;

    public interface ISearchService<T>
    {
        IEnumerable<T> Search(string search);
    }
}