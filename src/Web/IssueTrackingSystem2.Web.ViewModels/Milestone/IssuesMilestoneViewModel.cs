namespace IssueTrackingSystem2.Web.ViewModels.Milestone
{
    using IssueTrackingSystem2.Services.Mapping;
    using IssueTrackingSystem2.Services.Models;
    using IssueTrackingSystem2.Web.ViewModels.Project;

    public class IssuesMilestoneViewModel : IMapFrom<MilestoneServiceModel>
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public ProjectConciseViewModel Project { get; set; }
    }
}
