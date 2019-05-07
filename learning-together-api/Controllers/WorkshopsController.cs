namespace learning_together_api.Controllers
{
    using System;
    using System.Collections.Generic;
    using AutoMapper;
    using Data;
    using Data.Mappers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;
    using Services;

    public class WorkshopsController : UserCacheLearnTogetherController
    {
        private readonly IMapper mapper;
        private readonly IWorkshopService service;
        private readonly IWorkshopAttendeeService workshopAttendeeService;

        public WorkshopsController(IWorkshopService workshopService, IWorkshopAttendeeService workshopAttendeeService, IMapper mapper, IUserService userService, IMemoryCache memoryCache)
            : base(userService, memoryCache)
        {
            this.service = workshopService;
            this.workshopAttendeeService = workshopAttendeeService;
            this.mapper = mapper;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] WorkshopDto dto)
        {
            if (!this.TryValidateModel(dto)) return this.BadRequest(new {message = "Something's wrong."});

            int educatorId = this.GetUserId(this.User.Identity.Name);

            Workshop workshop = this.mapper.Map<Workshop>(dto);

            workshop.EducatorId = educatorId;

            workshop = this.service.Create(workshop);
            return this.Ok(workshop.Id);
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
        public IActionResult GetFiltered(int? categoryId, int? locationId, DateTime? startDate, DateTime? endDate)
        {
            IEnumerable<Workshop> filtered = this.service.GetAll(categoryId, locationId, startDate, endDate);
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
            int userId = this.GetUserId(this.User.Identity.Name);
            this.workshopAttendeeService.Enroll(id, userId);
            return this.Ok();
        }

        [HttpDelete("{id}/enroll")]
        public IActionResult Unenroll(int id)
        {
            int userId = this.GetUserId(this.User.Identity.Name);
            this.workshopAttendeeService.Unenroll(id, userId);
            return this.Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] WorkshopDto dto)
        {
            int userId = this.GetUserId(this.User.Identity.Name);
            Workshop workshop = this.mapper.Map<Workshop>(dto);
            this.service.Update(userId, id, workshop);
            return this.Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            int userId = this.GetUserId(this.User.Identity.Name);
            this.service.Cancel(userId, id);
            return this.Ok();
        }
    }
}