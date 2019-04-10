namespace learning_together_api.Controllers
{
    using Data;
    using Services;

    public class RolesController : GetDataController<Role>
    {
        public RolesController(IRoleService service) : base(service) { }
    }
}