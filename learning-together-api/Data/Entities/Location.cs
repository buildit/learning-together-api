namespace learning_together_api.Data
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("locations", Schema = "admin")]
    public class Location : NamedEntity
    {
        public Location()
        {
        }

        public Location(int id, string name) : base(id, name)
        {
        }
    }
}