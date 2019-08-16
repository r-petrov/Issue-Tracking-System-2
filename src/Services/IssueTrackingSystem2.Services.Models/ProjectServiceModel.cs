namespace IssueTrackingSystem2.Services.Models
{
    using IssueTrackingSystem2.Data.Models;
    using IssueTrackingSystem2.Services.Mapping;
    using System;
    using System.Collections.Generic;

    public class ProjectServiceModel : IMapFrom<Project>, IMapTo<Project>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ProjectKey { get; set; }

        public string LeaderId { get; set; }

        public ApplicationUserServiceModel Leader { get; set; }

        public ICollection<MilestoneServiceModel> Milestones { get; set; }

        public ICollection<PriorityServiceModel> Priorities { get; set; }

        public ICollection<LabelServiceModel> Labels { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
