namespace learning_together_api.Data.Mappers
{
    using System;
    using System.Collections.Generic;

    public class UserDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public int? RoleId { get; set; }

        public Role Role { get; set; }

        public int? LocationId { get; set; }

        public Location Location { get; set; }

        public string ImageUrl { get; set; }

        public bool Deactivated { get; set; }

        public string DirectoryName { get; set; }

        public string OrganizationId { get; set; }

        public DateTime LastLogin { get; set; }

        public List<DisciplineDto> UserInterests { get; set; }

        public List<AttendeeWorkshopDto> WorkshopsAttending { get; set; }

        public List<AttendeeWorkshopDto> WorkshopsTeaching { get; set; }
    }
}