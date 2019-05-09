namespace learning_together_api.Controllers
{
    using Data;
    using pathways_common.Controllers;
    using Services;

    public class LocationsController : GetDataController<Location>
    {
        public LocationsController(ILocationService service) : base(service) { }
    }
}