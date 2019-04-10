namespace learning_together_api.Controllers
{
    using Data;
    using Services;

    public class LocationsController : GetDataController<Location>
    {
        public LocationsController(ILocationService service) : base(service) { }
    }
}