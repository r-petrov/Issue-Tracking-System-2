namespace IssueTrackingSystem2.Web.ViewModels
{
    using IssueTrackingSystem2.Services.Mapping;
    using IssueTrackingSystem2.Services.Models;

    public class ApplicationRoleViewModel : IMapFrom<ApplicationRoleServiceModel>
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
