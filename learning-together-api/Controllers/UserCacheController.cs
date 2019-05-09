namespace learning_together_api.Controllers
{
    using Data;
    using Microsoft.Extensions.Caching.Memory;
    using pathways_common.Controllers;
    using pathways_common.Interfaces;

    public class UserCacheController : CacheResolvingController<User>
    {
        public UserCacheController(IResolveService<User> userService, IMemoryCache memoryCache) 
            : base(userService, memoryCache)
        {
        }
    }
}