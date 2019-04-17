namespace learning_together_api.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Data;
    using Data.Mappers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Services;

    public class SearchController : LearnTogetherController
    {
        private readonly IWorkshopService workshopService;
        private readonly IUserService userService;
        private readonly ICategoryService categoryService;
        private readonly ILocationService locationService;
        private readonly IMapper mapper;

        public SearchController(IWorkshopService workshopService, IUserService userService, ICategoryService categoryService, ILocationService locationService, IMapper mapper)
        {
            this.workshopService = workshopService;
            this.userService = userService;
            this.categoryService = categoryService;
            this.locationService = locationService;
            this.mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Search(string search, int maxResults = 5)
        {
            SearchDto result = new SearchDto();

            int resultsRequired = maxResults;

            // TODO: Super hacky throwaway code. (If you're reading this, that means I never fixed it. I know what I did was lazy. I'm sorry. -CP)

            bool done = false;

            while (!done)
            {
                IEnumerable<Workshop> workshops = this.workshopService.Search(search).Take(resultsRequired);
                IList<WorkshopDto> workshopDtos = this.mapper.Map<IList<WorkshopDto>>(workshops);
                result.WorkshopResults = workshopDtos;
                resultsRequired = resultsRequired - workshopDtos.Count;

                if (resultsRequired <= 0) break;

                IEnumerable<User> users = this.userService.Search(search).Take(resultsRequired);
                IList<UserDto> userDtos = this.mapper.Map<IList<UserDto>>(users);
                result.UserResults = userDtos;
                resultsRequired = resultsRequired - userDtos.Count;

                if (resultsRequired <= 0) break;

                IEnumerable<Category> categories = this.categoryService.Search(search).Take(resultsRequired);
                IList<CategoryDto> categoryDtos = this.mapper.Map<IList<CategoryDto>>(categories);
                result.CategoryResults = categoryDtos;
                resultsRequired = resultsRequired - categoryDtos.Count;

                if (resultsRequired <= 0) break;

                IEnumerable<Location> locations = this.locationService.Search(search).Take(resultsRequired);
                IList<LocationDto> locationDtos = this.mapper.Map<IList<LocationDto>>(locations);
                result.LocationResults = locationDtos;
                resultsRequired = resultsRequired - locationDtos.Count;

                done = true;
            }

            return this.Ok(result);
        }
    }
}