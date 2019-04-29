namespace learning_together_api.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Data;

    public interface IWorkshopService : IGetDataService<Workshop>, ISearchService<Workshop>
    {
        Workshop Create(Workshop workshop);

        IQueryable<Workshop> GetLoaded(bool initFilters = true);

        Workshop GetLoaded(int id);

        void Cancel(int userId, int id);

        IQueryable<Workshop> GetByStartDateRange(DateTime startDate, DateTime endDate);

        IQueryable<Workshop> GetByCategory(int categoryId);

        IEnumerable<Workshop> GetAll(int? categoryId, int? locationId, DateTime? startDate, DateTime? endDate);

        void Update(int userId, int id, Workshop dto);
    }
}