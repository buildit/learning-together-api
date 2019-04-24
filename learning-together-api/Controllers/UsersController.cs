namespace learning_together_api.Controllers
{
    using System.Collections.Generic;
    using AutoMapper;
    using Data;
    using Data.Mappers;
    using Exceptions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Services;

    public class UsersController : LearnTogetherController
    {
        private readonly AppSettings appSettings;
        private readonly IMapper mapper;
        private readonly IUserService userService;
        private readonly IImageStorageService imageService;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly ILogger<UsersController> logger;

        // TODO: move logger to base
        public UsersController(IUserService userService, IMapper mapper, IOptions<AppSettings> appSettings, IImageStorageService imageService, IHostingEnvironment hostingEnvironment, ILogger<UsersController> logger)
        {
            this.userService = userService;
            this.imageService = imageService;
            this.hostingEnvironment = hostingEnvironment;
            this.logger = logger;
            this.mapper = mapper;
            this.appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] UserDto userDto)
        {
            User user = this.userService.Authenticate(userDto.Username, userDto.Password);

            if (user == null)
            {
                return this.BadRequest(new { message = "Username or password is incorrect" });
            }

            string tokenString = SecurityService.GetTokenString(this.appSettings.Secret, user.Id.ToString());

            return this.Ok(new
            {
                user.Id,
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
                User createdUser = this.userService.Create(user, userDto.Password);
                return this.Ok(createdUser.Id);
            }
            catch (AppException ex)
            {
                return this.BadRequest(new { message = ex.Message });
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
            User user = this.mapper.Map<User>(userDto);
            user.Id = id;

            try
            {
                this.userService.Update(user, userDto.Password);
                return this.Ok();
            }
            catch (AppException ex)
            {
                return this.BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id > 0)
            {
                return this.BadRequest("Delete not yet implemented");
            }

            this.userService.Delete(id);
            return this.Ok();
        }
    }
}