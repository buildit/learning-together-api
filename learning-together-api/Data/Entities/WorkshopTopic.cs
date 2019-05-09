namespace learning_together_api.Data
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("workshoptopics", Schema = "workshop")]
    public class WorkshopTopic
    {
        public WorkshopTopic() { }

        public WorkshopTopic(int workshopId, int disciplineId)
        {
            this.WorkshopId = workshopId;
            this.DisciplineId = disciplineId;
        }

        public int WorkshopId { get; set; }

        public Workshop Workshop { get; set; }

        public int DisciplineId { get; set; }

        public Discipline Discipline { get; set; }
    }
}