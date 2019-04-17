namespace learning_together_api.Services
{
    using Data;

    public class RoleService : DataQueryService<Role>, IRoleService
    {
        public RoleService(DataContext context)
            : base(context, context.Roles) { }
    }
}