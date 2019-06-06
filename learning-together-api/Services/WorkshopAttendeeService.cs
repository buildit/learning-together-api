namespace learning_together_api.Services
{
    using System;
    using System.Linq;
    using Data;

    public class WorkshopAttendeeService : IWorkshopAttendeeService
    {
        private DataContext context;

        public WorkshopAttendeeService(DataContext context)
        {
            this.context = context;
        }

        public void Enroll(int workshopId, int userId)
        {
            WorkshopAttendee wa = new WorkshopAttendee();
            wa.WorkshopId = workshopId;
            wa.UserId = userId;
            wa.CreatedDate = DateTime.Now;

            this.context.WorkshopAttendees.Add(wa);
            this.context.SaveChanges();
        }

        public void Unenroll(int workshopId, int userId)
        {
            WorkshopAttendee wa = this.context.WorkshopAttendees.FirstOrDefault(w => w.WorkshopId == workshopId && w.UserId == userId);
            this.context.Remove(wa);
            this.context.SaveChanges();
        }
    }
}