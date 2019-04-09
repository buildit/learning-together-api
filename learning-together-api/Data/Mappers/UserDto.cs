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

        public Role Role { get; set; }

        public Location Location { get; set; }

        public List<DisciplineDto> UserInterests { get; set; }
    }
}