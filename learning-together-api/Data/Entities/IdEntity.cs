namespace learning_together_api.Data
{
    public abstract class IdEntity
    {
        protected IdEntity()
        {
        }

        protected IdEntity(int id)
        {
            this.Id = id;
        }

        public int Id { get; set; }
    }
}