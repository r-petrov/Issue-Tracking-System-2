namespace IssueTrackingSystem2.Services.Models
{
    using IssueTrackingSystem2.Data.Models;
    using IssueTrackingSystem2.Services.Mapping;
    using System.Collections.Generic;

    public class StatusServiceModel : IMapFrom<Status>, IMapTo<Status>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public virtual ICollection<MilestoneServiceModel> Milestones { get; set; }

        public virtual ICollection<IssueServiceModel> Issues { get; set; }
    }
}