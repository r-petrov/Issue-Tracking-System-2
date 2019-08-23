namespace IssueTrackingSystem2.Services.Models
{
    using IssueTrackingSystem2.Data.Models;
    using IssueTrackingSystem2.Services.Mapping;
    using System;
    using System.Collections.Generic;

    public class MilestoneServiceModel : IMapFrom<Milestone>, IMapTo<Milestone>
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime CompletionDate { get; set; }

        public int StatusId { get; set; }

        public StatusServiceModel Status { get; set; }

        public string ProjectId { get; set; }

        public ProjectServiceModel Project { get; set; }

        public virtual ICollection<IssueServiceModel> Issues { get; set; }
    }
}
