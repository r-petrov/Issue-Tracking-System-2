namespace IssueTrackingSystem2.Web.ViewModels.Issue
{
    using IssueTrackingSystem2.Services.Mapping;
    using IssueTrackingSystem2.Services.Models;
    using IssueTrackingSystem2.Web.ViewModels.Milestone;

    public class CommentDeleteIssueViewModel : IMapFrom<IssueServiceModel>
    {
        public string Id { get; set; }

        public string AssigneeId { get; set; }

        public CommentDeleteMilestoneViewModel Milestone { get; set; }
    }
}
