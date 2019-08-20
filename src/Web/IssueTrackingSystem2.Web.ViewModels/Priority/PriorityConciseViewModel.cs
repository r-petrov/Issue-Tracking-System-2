namespace IssueTrackingSystem2.Web.ViewModels.Priority
{
    using IssueTrackingSystem2.Services.Mapping;
    using IssueTrackingSystem2.Services.Models;

    public class PriorityConciseViewModel : IMapFrom<PriorityServiceModel>
    {
        public string Name { get; set; }
    }
}
