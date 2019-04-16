namespace learning_together_api.Controllers
{
    using System.Collections.Generic;
    using AutoMapper;
    using Data;
    using Data.Mappers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Services;

    public class WorkshopsController : LearnTogetherController
    {
        private readonly IMapper mapper;
        private readonly IWorkshopService service;

        public WorkshopsController(IWorkshopService workshopService, IMapper mapper)
        {
            this.service = workshopService;
            this.mapper = mapper;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] WorkshopDto dto)
        {
            if (!this.TryValidateModel(dto))
            {
                return this.BadRequest(new { message = "Something's wrong." });
            }

            int educatorId = int.Parse(this.User.Identity.Name);

            Workshop workshop = this.mapper.Map<Workshop>(dto);

            workshop.EducatorId = educatorId;

            workshop = this.service.Create(workshop);
            return this.Ok();
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<Workshop> workshops = this.service.GetLoaded();
            IList<WorkshopDto> workshopDtos = this.mapper.Map<IList<WorkshopDto>>(workshops);
            return this.Ok(workshopDtos);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            //User user = this.service.GetByIdWithIncludes(id);
            //UserDto userDto = this.mapper.Map<UserDto>(user);
            return this.Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UserDto userDto)
        {
            return this.BadRequest("Update not yet implemented");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return this.BadRequest("Delete not yet implemented");
        }
    }
}