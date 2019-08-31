namespace IssueTrackingSystem2.Web.ViewModels.Milestone
{
    using IssueTrackingSystem2.Services.Mapping;
    using IssueTrackingSystem2.Services.Models;
    using IssueTrackingSystem2.Web.ViewModels.Issue;
    using IssueTrackingSystem2.Web.ViewModels.Status;
    using System.Collections.Generic;

    public class MilestoneConciseViewModel : IMapFrom<MilestoneServiceModel>
    {
        public ICollection<ProjectListIssueViewModel> Issues { get; set; }

        public StatusConciseViewModel Status { get; set; }
    }
}
