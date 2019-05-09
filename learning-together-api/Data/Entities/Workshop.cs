namespace learning_together_api.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using pathways_common.Entities;

    [Table("workshops", Schema = "workshop")]
    public class Workshop : NamedEntity
    {
        public int EducatorId { get; set; }

        public string ImageUrl { get; set; }

        public User Educator { get; set; }

        public int LocationId { get; set; }

        public Location Location { get; set; }

        public string Description { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public string Webex { get; set; }

        public string Room { get; set; }

        public bool? Cancelled { get; set; }

        public int? CategoryId { get; set; }

        public Category Category { get; set; }

        public ICollection<WorkshopAttendee> WorkshopAttendees { get; set; }

        public ICollection<WorkshopTopic> WorkshopTopics { get; set; }
    }
}