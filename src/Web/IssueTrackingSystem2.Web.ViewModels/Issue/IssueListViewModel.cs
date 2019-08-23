namespace IssueTrackingSystem2.Web.ViewModels.Issue
{
    using IssueTrackingSystem2.Services.Mapping;
    using IssueTrackingSystem2.Services.Models;
    using IssueTrackingSystem2.Web.ViewModels.Label;
    using IssueTrackingSystem2.Web.ViewModels.Status;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class IssueListViewModel : IMapFrom<IssueServiceModel>
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public virtual StatusConciseViewModel Status { get; set; }

        public DateTime DueDate { get; set; }

        public string AssigneeId { get; set; }

        public virtual ApplicationUserViewModel Assignee { get; set; }

        public virtual ICollection<LabelConciseViewModel> Labels { get; set; }
    }
}
