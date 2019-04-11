namespace learning_together_api.Services
{
    using System;
    using System.Collections.Generic;
    using Data;

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
    }
}