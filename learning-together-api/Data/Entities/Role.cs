namespace learning_together_api.Data
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("roles", Schema = "admin")]
    public class Role : NamedEntity
    {
        public Role()
        {
        }

        public Role(int id, string name) : base(id, name)
        {
        }
    }
}