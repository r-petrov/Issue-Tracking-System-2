namespace IssueTrackingSystem2.Services.Models
{
    using IssueTrackingSystem2.Data.Models;
    using IssueTrackingSystem2.Services.Mapping;
    using Microsoft.AspNetCore.Identity;

    public class ApplicationRoleServiceModel : IMapFrom<ApplicationRole>, IMapFrom<IdentityUserRole<string>>
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}