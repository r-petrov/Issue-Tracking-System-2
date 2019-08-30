namespace IssueTrackingSystem2.Web.ViewModels.Milestone
{
    using IssueTrackingSystem2.Services.Mapping;
    using IssueTrackingSystem2.Services.Models;

    public class CommentDeleteMilestoneViewModel : IMapFrom<MilestoneServiceModel>
    {
        public string ProjectLeaderId { get; set; }
    }
}
