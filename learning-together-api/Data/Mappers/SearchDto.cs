namespace learning_together_api.Data.Mappers
{
    using System.Collections.Generic;

    public class SearchDto
    {
        public IEnumerable<UserDto> UserResults { get; set; }

        public IEnumerable<WorkshopDto> WorkshopResults { get; set; }

        public IEnumerable<CategoryDto> CategoryResults { get; set; }

        public IEnumerable<LocationDto> LocationResults { get; set; }
    }
}