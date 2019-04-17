namespace learning_together_api.Services
{
    using Data;

    public class DisciplineService : NamedSearchableService<Discipline>, IDisciplineService
    {
        public DisciplineService(DataContext context)
            : base(context, context.Disciplines) { }
    }
}