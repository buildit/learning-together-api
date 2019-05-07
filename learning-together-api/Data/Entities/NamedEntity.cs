namespace learning_together_api.Data
{
    public abstract class NamedEntity : IdEntity
    {
        protected NamedEntity()
        {
        }

        protected NamedEntity(int id, string name) : base(id)
        {
            this.Name = name;
        }

        public string Name { get; set; }
    }
}