namespace learning_together_api.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Data;
    using Microsoft.EntityFrameworkCore;

    public class WorkshopService : NamedSearchableService<Workshop>, IWorkshopService
    {
        public WorkshopService(DataContext context) : base(context, context.Workshops) { }

        public Workshop Create(Workshop workshop)
        {
            this.context.Workshops.Add(workshop);
            this.context.SaveChanges();
            return workshop;
        }

        public IQueryable<Workshop> GetLoaded()
        {
            return this.context.Workshops
                .Include((c => c.Educator))
                .Include(c => c.Location);
        }

        public Workshop GetLoaded(int id)
        {
            return this.context.Workshops.Where(c => c.Id == id)
                .Include(c => c.Educator)
                .Include(c => c.Location)
                .Include(c => c.WorkshopAttendees)
                .Include(c => c.WorkshopTopics).FirstOrDefault();
        }

        public IQueryable<Workshop> GetByStartDateRange(DateTime startDate, DateTime endDate)
        {
            IQueryable<Workshop> workshops = this.GetLoaded();
            return workshops.Where(w => w.Start >= startDate && w.Start <= endDate);
        }

        public IQueryable<Workshop> GetByCategory(int categoryId)
        {
            return this.GetLoaded().Where(w => w.CategoryId == categoryId);
        }

        public IEnumerable<Workshop> GetAll(int? categoryId, DateTime? startDate, DateTime? endDate)
        {
            IQueryable<Workshop> workshops = this.GetLoaded();
            if (categoryId.HasValue)
            {
                workshops = workshops.Where(w => w.CategoryId == categoryId);
            }

            if (startDate.HasValue)
            {
                workshops = workshops.Where(w => w.Start >= startDate && w.Start <= endDate);
            }

            return workshops;
        }
    }
}