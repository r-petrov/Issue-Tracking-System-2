namespace IssueTrackingSystem2.Web.ViewModels.Issue
{
    using IssueTrackingSystem2.Services.Mapping;
    using IssueTrackingSystem2.Services.Models;
    using IssueTrackingSystem2.Web.ViewModels.Label;
    using IssueTrackingSystem2.Web.ViewModels.Milestone;
    using IssueTrackingSystem2.Web.ViewModels.Status;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class IssueDetailsViewModel : IMapFrom<IssueServiceModel>
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string IssueKey { get; set; }

        public StatusConciseViewModel Status { get; set; }

        public string Priority { get; set; }

        [Display(Name = "Due Date")]
        public DateTime DueDate { get; set; }

        public IssueDetailsMilestoneViewModel Milestone { get; set; }

        //public string AssigneeId { get; set; }

        public ApplicationUserViewModel Assignee { get; set; }

        public virtual ICollection<LabelConciseViewModel> Labels { get; set; }

        public virtual ICollection<CommentServiceModel> Comments { get; set; }
    }
}
