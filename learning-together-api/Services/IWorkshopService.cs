namespace learning_together_api.Services
{
    using System.Collections.Generic;
    using Data;

    public interface IWorkshopService : IGetDataService<Workshop>
    {
        Workshop Create(Workshop workhop);

        IEnumerable<Workshop> GetLoaded();
    }
}