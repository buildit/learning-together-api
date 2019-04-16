namespace learning_together_api.Data.Mappers
{
    using System.Collections.Generic;

    public class UserDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public int? RoleId { get; set; }

        public Role Role { get; set; }

        public int? LocationId { get; set; }

        public Location Location { get; set; }

        public string ImageUrl { get; set; }

        public List<DisciplineDto> UserInterests { get; set; }

        public List<AttendeeWorkshopDto> WorkshopsAttending { get; set; }

        public List<AttendeeWorkshopDto> WorkshopsTeaching { get; set; }
    }
}