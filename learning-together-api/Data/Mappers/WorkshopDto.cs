namespace learning_together_api.Data.Mappers
{
    using System;
    using System.Collections.Generic;
    using pathways_common.Entities;

    public class WorkshopDto : AuditedNamedEntity
    {
        public int EducatorId { get; set; }

        public UserDto Educator { get; set; }

        public int LocationId { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public Location Location { get; set; }

        public string Description { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public string Webex { get; set; }

        public string ImageUrl { get; set; }

        public string Room { get; set; }

        public bool Cancelled { get; set; }

        public string RobinEventId { get; set; }

        public string ArchiveLink { get; set; }

        public ICollection<WorkshopAttendeeDto> WorkshopAttendees { get; set; }

        public ICollection<DisciplineDto> WorkshopTopics { get; set; }
    }
}