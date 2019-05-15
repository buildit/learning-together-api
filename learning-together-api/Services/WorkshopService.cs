namespace learning_together_api.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Data;
    using Microsoft.EntityFrameworkCore;

    public class WorkshopService : LearningTogetherDataQueryService<Workshop>, IWorkshopService
    {
        public WorkshopService(DataContext context) : base(context, context.Workshops)
        {
        }

        public Workshop Create(Workshop workshop)
        {
            this.context.Workshops.Add(workshop);
            this.context.SaveChanges();

            this.SetWorkshopDiscipline(workshop.Id, workshop.CategoryId);
            this.context.SaveChanges();

            return workshop;
        }

        public IQueryable<Workshop> GetLoaded(bool initFilters = true)
        {
            IQueryable<Workshop> contextWorkshops = this.context.Workshops;

            if (initFilters)
            {
                contextWorkshops = contextWorkshops.Where(w => w.Cancelled != true && w.Start >= DateTime.Today);
            }

            return contextWorkshops
                .Include(c => c.Educator)
                .Include(c => c.Location);
        }

        public Workshop GetLoaded(int id)
        {
            return this.context.Workshops.Where(c => c.Id == id)
                .Include(c => c.Educator)
                .Include(c => c.Location)
                .Include(c => c.Category)
                .Include(c => c.WorkshopAttendees).ThenInclude(wa => wa.User)
                .Include(c => c.WorkshopTopics).ThenInclude(wt => wt.Discipline).FirstOrDefault();
        }

        public void Cancel(int userId, int id)
        {
            Workshop workshop = this.collection.FirstOrDefault(w => w.Id == id);
            if (workshop == null)
            {
                throw new InvalidOperationException($"Could not find workshop with id {id}");
            }

            if (userId != workshop.EducatorId) throw new UnauthorizedAccessException();

            workshop.Cancelled = true;
            this.context.Workshops.Update(workshop);
            this.context.SaveChanges();
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

        public IEnumerable<Workshop> GetAll(int? categoryId, int? locationId, DateTime? startDate, DateTime? endDate)
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

            if (locationId.HasValue)
            {
                workshops = workshops.Where(w => w.LocationId == locationId);
            }

            return workshops;
        }

        public void Update(int userId, int id, Workshop workshop)
        {
            Workshop oldWorkshop = this.context.Workshops.First(w => w.Id == id);
            if (userId != oldWorkshop.EducatorId) throw new UnauthorizedAccessException();

            oldWorkshop.ImageUrl = workshop.ImageUrl;
            oldWorkshop.LocationId = workshop.LocationId;
            oldWorkshop.End = workshop.End;
            oldWorkshop.Room = workshop.Room;
            oldWorkshop.Start = workshop.Start;
            oldWorkshop.Webex = workshop.Webex;
            oldWorkshop.CategoryId = workshop.CategoryId;
            oldWorkshop.Name = workshop.Name;
            oldWorkshop.Description = workshop.Description;

            this.SetWorkshopDiscipline(id, workshop.CategoryId);

            this.context.Workshops.Update(oldWorkshop);
            this.context.SaveChanges();
        }

        public IEnumerable<Workshop> Search(string search)
        {
            search = search.ToLower();
            return this.GetLoaded().Where(w => w.Name.ToLower().Contains(search));
        }

        private void SetWorkshopDiscipline(int id, int? workshopCategoryId)
        {
            IQueryable<WorkshopTopic> workshopTopics = this.context.WorkshopTopics.Where(w => w.WorkshopId == id);

            if (workshopTopics.Any()) this.context.WorkshopTopics.RemoveRange(workshopTopics);

            if (workshopCategoryId.HasValue)
            {
                Discipline discipline = this.context.Disciplines.First(w => w.CategoryId == workshopCategoryId);
                WorkshopTopic wt = new WorkshopTopic(id, discipline.Id);
                this.context.WorkshopTopics.Add(wt);
            }
        }
    }
}