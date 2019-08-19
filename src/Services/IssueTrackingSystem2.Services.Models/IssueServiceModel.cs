namespace IssueTrackingSystem2.Services.Models
{
    using IssueTrackingSystem2.Data.Models;
    using IssueTrackingSystem2.Services.Mapping;
    using System;
    using System.Collections.Generic;

    public class IssueServiceModel : IMapFrom<Issue>
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string IssueKey { get; set; }

        public int StatusId { get; set; }

        public StatusServiceModel Status { get; set; }

        public DateTime DueDate { get; set; }

        public string MilestoneId { get; set; }

        public MilestoneServiceModel Milestone { get; set; }

        public string AssigneeId { get; set; }

        public ApplicationUserServiceModel Assignee { get; set; }

        public virtual ICollection<LabelServiceModel> Labels { get; set; }

        public virtual ICollection<CommentServiceModel> Comments { get; set; }
    }
}