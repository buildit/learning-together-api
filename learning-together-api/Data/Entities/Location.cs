namespace learning_together_api.Data
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("locations", Schema = "admin")]
    public class Location
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}