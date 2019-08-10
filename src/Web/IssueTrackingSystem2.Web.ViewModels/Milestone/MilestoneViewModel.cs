namespace IssueTrackingSystem2.Web.ViewModels.Milestone
{
    using IssueTrackingSystem2.Services.Mapping;
    using IssueTrackingSystem2.Services.Models;
    using IssueTrackingSystem2.Web.ViewModels.Issue;
    using IssueTrackingSystem2.Web.ViewModels.Status;
    using System;
    using System.Collections.Generic;

    public class MilestoneViewModel : IMapFrom<MilestoneServiceModel>
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public DateTime ScheduledStartDate { get; set; }

        public DateTime ScheduledCompletionDate { get; set; }

        public DateTime? ActualStartDate { get; set; }

        public DateTime? ActualCompletionDate { get; set; }

        public DashboardStatusViewModel Status { get; set; }

        public virtual ICollection<DashboardIssueViewModel> Issues { get; set; }
    }
}
