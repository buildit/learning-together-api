namespace learning_together_api.Controllers
{
    using System;
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
        private readonly IWorkshopAttendeeService workshopAttendeeService;

        public WorkshopsController(IWorkshopService workshopService, IWorkshopAttendeeService workshopAttendeeService, IMapper mapper)
        {
            this.service = workshopService;
            this.workshopAttendeeService = workshopAttendeeService;
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

        [AllowAnonymous]
        [HttpGet("filter")]
        public IActionResult GetFiltered(int? categoryId, DateTime? startDate, DateTime? endDate)
        {
            IEnumerable<Workshop> filtered = this.service.GetAll(categoryId, startDate, endDate);
            IList<WorkshopDto> workshopDtos = this.mapper.Map<IList<WorkshopDto>>(filtered);
            return this.Ok(workshopDtos);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Workshop workshop = this.service.GetLoaded(id);
            WorkshopDto dto = this.mapper.Map<WorkshopDto>(workshop);
            return this.Ok(dto);
        }

        [HttpPut("{id}/enroll")]
        public IActionResult Enroll(int id)
        {
            int userId = int.Parse(this.User.Identity.Name);
            this.workshopAttendeeService.Enroll(id, userId);
            return this.Ok();
        }

        [HttpDelete("{id}/enroll")]
        public IActionResult Unenroll(int id)
        {
            int userId = int.Parse(this.User.Identity.Name);
            this.workshopAttendeeService.Unenroll(id, userId);
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