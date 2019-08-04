namespace IssueTrackingSystem2.Services.Models
{
    using IssueTrackingSystem2.Data.Models;
    using IssueTrackingSystem2.Services.Mapping;
    using System;
    using System.Collections.Generic;

    public class LabelServiceModel : IMapFrom<Label>, IMapFrom<ProjectLabel>, IMapFrom<IssueLabel>
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Color { get; set; }

        public string CreatorId { get; set; }

        public ApplicationUserServiceModel CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        public virtual ICollection<ProjectServiceModel> Projects { get; set; }

        public virtual ICollection<IssueServiceModel> Issues { get; set; }
    }
}