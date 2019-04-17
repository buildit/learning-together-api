namespace learning_together_api.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Data;

    public interface IWorkshopService : IGetDataService<Workshop>, ISearchService<Workshop>
    {
        Workshop Create(Workshop workshop);

        IQueryable<Workshop> GetLoaded();

        Workshop GetLoaded(int id);

        void Cancel(int id);

        IQueryable<Workshop> GetByStartDateRange(DateTime startDate, DateTime endDate);

        IQueryable<Workshop> GetByCategory(int categoryId);

        IEnumerable<Workshop> GetAll(int? categoryId, DateTime? startDate, DateTime? endDate);
    }
}