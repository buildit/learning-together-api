namespace learning_together_api.Services
{
    using System;
    using System.Collections.Generic;
    using Data;

    public class LocationService : DataQueryService<Location>, ILocationService
    {
        public LocationService(DataContext context)
            : base(context, context.Locations) { }

        public override IEnumerable<Location> FindByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}