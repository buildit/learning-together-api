namespace learning_together_api.Data
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("workshopattendees", Schema = "workshop")]
    public class WorkshopAttendee
    {
        public int UserId { get; set; }

        public User User { get; set; }

        public int WorkshopId { get; set; }

        public Workshop Workshop { get; set; }
    }
}