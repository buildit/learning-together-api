namespace learning_together_api.Controllers
{
    using System;
    using Microsoft.Extensions.Caching.Memory;
    using Services;

    public abstract class UserCacheLearnTogetherController : LearnTogetherController
    {
        private readonly IMemoryCache memoryCache;
        private readonly IUserService userService;

        protected UserCacheLearnTogetherController(IUserService userService, IMemoryCache memoryCache)
        {
            this.userService = userService;
            this.memoryCache = memoryCache;
        }

        protected int GetUserId(string identityName)
        {
            return this.memoryCache.GetOrCreate(identityName, e =>
            {
                e.SlidingExpiration = TimeSpan.FromHours(4);
                return this.userService.Retrieve(identityName).Id;
            });
        }
    }
}