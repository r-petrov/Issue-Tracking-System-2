namespace IssueTrackingSystem2.Web.ViewModels.Issue
{
    using IssueTrackingSystem2.Web.ViewModels.Milestone;
    using System.Collections.Generic;

    public class IssuesViewModel
    {
        public ICollection<IssueListViewModel> Issues { get; set; }

        public IssuesMilestoneViewModel Milestone { get; set; }
    }
}
