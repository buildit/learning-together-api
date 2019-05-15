namespace learning_together_api.Controllers
{
    using System.Collections.Generic;
    using AutoMapper;
    using Data;
    using Data.Mappers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using pathways_common;
    using pathways_common.Controllers;
    using pathways_common.Extensions;
    using Services;

    public class UsersController : CacheResolvingController<User>
    {
        private readonly AppSettings appSettings;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly IImageStorageService imageService;
        private readonly ILogger<UsersController> logger;
        private readonly IMapper mapper;
        private readonly IMemoryCache memoryCache;
        private readonly IUserService userService;

        // TODO: move logger to base
        public UsersController(IUserService userService, IMapper mapper, IOptions<AppSettings> appSettings, IImageStorageService imageService, IHostingEnvironment hostingEnvironment, ILogger<UsersController> logger, IMemoryCache memoryCache)
            : base(userService, memoryCache)
        {
            this.userService = userService;
            this.imageService = imageService;
            this.hostingEnvironment = hostingEnvironment;
            this.logger = logger;
            this.memoryCache = memoryCache;
            this.mapper = mapper;
            this.appSettings = appSettings.Value;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] UserDto userDto)
        {
            string authenticatedEmail = this.User.Claims.GetEmail();
            string authenticatedName = this.User.Claims.GetName();

            if (authenticatedEmail != userDto.Username) return this.BadRequest("Token and e-mail do not match.");

            User user = this.userService.RetrieveOrCreate(authenticatedEmail, authenticatedName);

            string tokenString = this.Request.Headers["Bearer"];

            return this.Ok(new
            {
                user.Id,
                user.DirectoryName,
                user.Username,
                user.FirstName,
                user.LastName,
                user.ImageUrl,
                Token = tokenString
            });
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] UserDto userDto)
        {
            User user = this.mapper.Map<User>(userDto);

            try
            {
                User createdUser = this.userService.Create(user);
                return this.Ok(createdUser.Id);
            }
            catch (AppException ex)
            {
                return this.BadRequest(new {message = ex.Message});
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<User> users = this.userService.GetAll();
            IList<UserDto> userDtos = this.mapper.Map<IList<UserDto>>(users);
            return this.Ok(userDtos);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            User user = this.userService.GetByIdWithIncludes(id);
            UserDto userDto = this.mapper.Map<UserDto>(user);
            return this.Ok(userDto);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UserDto userDto)
        {
            int userId = this.GetUserId(this.User.Identity.Name);
            User user = this.mapper.Map<User>(userDto);
            user.Id = id;

            try
            {
                this.userService.Update(userId, user);
                return this.Ok();
            }
            catch (AppException ex)
            {
                return this.BadRequest(new {message = ex.Message});
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            int userId = this.GetUserId(this.User.Identity.Name);

            if (id > 0) return this.BadRequest("Delete not yet implemented");

            this.userService.Delete(userId, id);
            return this.Ok();
        }
    }
}