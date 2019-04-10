namespace learning_together_api.Data
{
    public abstract class NamedEntity : IdEntity
    {
        public string Name { get; set; }
    }
}