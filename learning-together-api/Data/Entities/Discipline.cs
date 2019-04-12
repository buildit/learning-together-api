namespace learning_together_api.Data
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("disciplines", Schema = "admin")]
    public class Discipline : NamedEntity
    {
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public int? ParentDisciplineId { get; set; }

        public Discipline ParentDiscipline { get; set; }

        public ICollection<UserInterest> UserInterests { get; set; }

        public ICollection<WorkshopTopic> WorkshopTopics { get; set; }
    }
}