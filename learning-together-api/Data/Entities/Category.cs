namespace learning_together_api.Data
{
    using System.ComponentModel.DataAnnotations.Schema;
    using pathways_common.Entities;

    [Table("category", Schema = "admin")]
    public class Category : NamedEntity
    {
        public Category()
        {
        }

        public Category(int id, string name) : base(id, name)
        {
        }
    }
}