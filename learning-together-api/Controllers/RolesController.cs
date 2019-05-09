namespace learning_together_api.Controllers
{
    using Data;
    using pathways_common.Controllers;
    using Services;

    public class RolesController : GetDataController<Role>
    {
        public RolesController(IRoleService service) : base(service) { }
    }
}