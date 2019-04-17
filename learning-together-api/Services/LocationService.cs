namespace learning_together_api.Services
{
    using Data;

    public class LocationService : NamedSearchableService<Location>, ILocationService
    {
        public LocationService(DataContext context)
            : base(context, context.Locations) { }
    }
}