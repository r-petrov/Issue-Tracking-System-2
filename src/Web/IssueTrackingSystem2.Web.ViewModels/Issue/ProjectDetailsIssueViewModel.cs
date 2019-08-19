namespace IssueTrackingSystem2.Web.ViewModels.Issue
{
    using IssueTrackingSystem2.Services.Mapping;
    using IssueTrackingSystem2.Services.Models;
    using IssueTrackingSystem2.Web.ViewModels.Status;

    public class ProjectDetailsIssueViewModel : IMapFrom<IssueServiceModel>
    {
        public StatusConciseViewModel Status { get; set; }
    }
}
