namespace learning_together_api.Services
{
    using System;
    using System.Collections.Generic;
    using Data;
    using Microsoft.EntityFrameworkCore;

    public class WorkshopService : DataQueryService<Workshop>, IWorkshopService
    {
        public WorkshopService(DataContext context) : base(context, context.Workshops) { }

        public override IEnumerable<Workshop> FindByName(string name)
        {
            throw new NotImplementedException();
        }

        public Workshop Create(Workshop workshop)
        {
            this.context.Workshops.Add(workshop);
            this.context.SaveChanges();
            return workshop;
        }

        public IEnumerable<Workshop> GetLoaded()
        {
            return this.context.Workshops.Include((c => c.Educator)).Include(c => c.Location);
        }
    }
}