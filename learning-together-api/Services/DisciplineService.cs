namespace learning_together_api.Services
{
    using System;
    using System.Collections.Generic;
    using Data;

    public class DisciplineService : DataQueryService<Discipline>, IDisciplineService
    {
        public DisciplineService(DataContext context)
            : base(context, context.Disciplines) { }

        public override IEnumerable<Discipline> FindByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}