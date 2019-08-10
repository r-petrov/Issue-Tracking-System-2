namespace IssueTrackingSystem2.Web.ViewModels.Status
{
    using IssueTrackingSystem2.Services.Mapping;
    using IssueTrackingSystem2.Services.Models;

    public class DashboardStatusViewModel : IMapFrom<StatusServiceModel>
    {
        public string Name { get; set; }
    }
}
