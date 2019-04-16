namespace learning_together_api.Controllers
{
    using System.Collections.Generic;
    using Data;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Services;

    public class DisciplinesController : GetDataController<Discipline>
    {
        private readonly ICategoryService categoryService;

        public DisciplinesController(IDisciplineService service, ICategoryService categoryService) : base(service)
        {
            this.categoryService = categoryService;
        }

        [HttpGet("categories")]
        [AllowAnonymous]
        public IActionResult Categories()
        {
            IEnumerable<Category> locations = this.categoryService.GetAll();
            return this.Ok(locations);
        }
    }
}