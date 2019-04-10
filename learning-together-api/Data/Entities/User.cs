namespace learning_together_api.Data
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("users", Schema = "admin")]
    public class User
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public int RoleId { get; set; }

        public Role Role { get; set; }

        public int LocationId { get; set; }

        public Location Location { get; set; }

        public ICollection<UserInterest> UserInterests { get; set; }
    }
}