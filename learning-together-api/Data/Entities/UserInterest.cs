namespace learning_together_api.Data
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("userinterests", Schema = "admin")]
    public class UserInterest
    {
        public int UserId { get; set; }

        public User User { get; set; }

        public int DisciplineId { get; set; }

        public Discipline Discipline { get; set; }
    }
}