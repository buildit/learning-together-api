namespace learning_together_api.Data
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("users", Schema = "admin")]
    public class User : IdEntity
    {
        public User()
        {
        }

        public User(string username, string directoryName)
        {
            this.Username = username;
            this.DirectoryName = directoryName;
        }

        public string ImageUrl { get; set; }

        public string DirectoryName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public int? RoleId { get; set; }

        public Role Role { get; set; }

        public int? LocationId { get; set; }

        public bool? Deactivated { get; set; }

        public Location Location { get; set; }

        public ICollection<Workshop> WorkshopsTeaching { get; set; }

        public ICollection<UserInterest> UserInterests { get; set; }

        public ICollection<WorkshopAttendee> WorkshopsAttending { get; set; }
    }
}