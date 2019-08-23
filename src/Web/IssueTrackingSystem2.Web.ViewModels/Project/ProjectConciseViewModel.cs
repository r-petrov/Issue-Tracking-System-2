namespace IssueTrackingSystem2.Web.ViewModels.Project
{
    using IssueTrackingSystem2.Services.Mapping;
    using IssueTrackingSystem2.Services.Models;

    public class ProjectConciseViewModel : IMapFrom<ProjectServiceModel>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string LeaderId { get; set; }
    }
}
