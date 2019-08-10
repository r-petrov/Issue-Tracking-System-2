namespace IssueTrackingSystem2.Web.ViewModels.Issue
{
    using IssueTrackingSystem2.Services.Mapping;
    using IssueTrackingSystem2.Services.Models;

    public class DashboardIssueViewModel : IMapFrom<IssueServiceModel>
    {
        public string AssigneeId { get; set; }
    }
}
