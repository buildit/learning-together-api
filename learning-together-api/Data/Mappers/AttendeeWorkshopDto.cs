namespace learning_together_api.Data.Mappers
{
    using System;

    public class AttendeeWorkshopDto
    {
        public int WorkshopId { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public DateTime StartDate { get; set; }
    }
}