namespace learning_together_api.Services
{
    using System;
    using System.Collections.Generic;
    using Data;

    public class RoleService : DataQueryService<Role>, IRoleService
    {
        public RoleService(DataContext context)
            : base(context, context.Roles) { }

        public override IEnumerable<Role> FindByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}