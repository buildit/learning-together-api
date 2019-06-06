namespace learning_together_api.Controllers
{
    using System;
    using System.Collections.Generic;
    using AutoMapper;
    using Data.Mappers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.Logging;
    using Microsoft.Graph;
    using pathways_common;
    using pathways_common.Authentication;
    using pathways_common.Controllers;
    using pathways_common.Core;
    using pathways_common.Extensions;
    using pathways_common.Interfaces.Services;
    using Services;
    using User = Data.User;

    public class UsersController : CacheResolvingController<User>
    {
        private readonly IMSGraphService graphService;
        private readonly ILogger<UsersController> logger;
        private readonly IMapper mapper;
        private readonly IUserService userService;

        // TODO: move logger to base
        public UsersController(IServiceHost serviceHost, IMapper mapper, ILogger<UsersController> logger, IMemoryCache memoryCache)
            : base(serviceHost.GetUserService(), memoryCache)
        {
            this.userService = serviceHost.GetUserService();
            this.graphService = serviceHost.GetMicrosoftGraphService();
            this.logger = logger;
            this.mapper = mapper;
        }

        [HttpPost("authenticate")]
        [MsalUiRequiredExceptionFilter(Scopes = new[] { PathwaysConstants.Graph.ScopeUserRead })]
        public IActionResult Authenticate([FromBody] UserDto userDto)
        {
            string authenticatedEmail = this.User.Claims.GetEmail();
            string authenticatedName = this.User.Claims.GetName();

            IGraphServiceClient graphClient = this.graphService.GetGraphServiceClient(this.HttpContext, new[] { PathwaysConstants.Graph.ScopeUserRead });
            Microsoft.Graph.User me = graphClient.Me.Request().GetAsync().Result;
            string graphEmail = me.Mail;

            if (authenticatedEmail != userDto.Username) return this.BadRequest("Token and e-mail do not match.");

            User user = this.userService.RetrieveOrCreate(graphEmail, authenticatedEmail, authenticatedName);

            this.userService.SetLogonTime(user);

            string tokenString = this.Request.Headers["Authorization"];

            return this.Ok(new
            {
                user.Id,
                user.DirectoryName,
                user.OrganizationId,
                user.LastLogin,
                user.Username,
                user.FirstName,
                user.LastName,
                user.ImageUrl,
                Token = tokenString
            });
        }

        [AllowAnonymous]
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
            int userId = this.GetUserId(this.User.Claims.GetEmail());
            User user = this.mapper.Map<User>(userDto);
            user.Id = id;

            try
            {
                this.userService.Update(userId, user);
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
            int userId = this.GetUserId(this.User.Claims.GetEmail());

            if (id > 0) return this.BadRequest("Delete not yet implemented");

            this.userService.Delete(userId, id);
            return this.Ok();
        }
    }
}