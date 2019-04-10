namespace learning_together_api.Controllers
{
    using System.Collections.Generic;
    using AutoMapper;
    using Data;
    using Data.Mappers;
    using Exceptions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using Services;

    public class UsersController : LearnTogetherController
    {
        private readonly AppSettings appSettings;
        private readonly IMapper mapper;
        private readonly IUserService userService;

        public UsersController(IUserService userService, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            this.userService = userService;
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
                this.userService.Create(user, userDto.Password);
                return this.Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return this.BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<User> users = this.userService.GetAll();
            IList<UserDto> userDtos = this.mapper.Map<IList<UserDto>>(users);
            return this.Ok(userDtos);
        }

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
            if (id > 0)
            {
                return this.BadRequest("Update not yet implemented");
            }

            // map dto to entity and set id
            User user = this.mapper.Map<User>(userDto);
            user.Id = id;

            try
            {
                // save 
                this.userService.Update(user, userDto.Password);
                return this.Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
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