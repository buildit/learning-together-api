namespace learning_together_api.Services
{
    using System.Collections.Generic;
    using Data;

    public interface IGetDataService<out T>
        where T : IdEntity
    {
        T Retrieve(int id);

        IEnumerable<T> GetAll();
    }
}