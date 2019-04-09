namespace learning_together_api.Data
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("disciplines", Schema = "admin")]
    public class Discipline : NamedEntity
    {
        public ICollection<UserInterest> UserInterests { get; set; }
    }
}