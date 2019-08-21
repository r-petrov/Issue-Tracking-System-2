namespace IssueTrackingSystem2.Services.Models
{
    using IssueTrackingSystem2.Data.Models;
    using IssueTrackingSystem2.Services.Mapping;
    using System.Collections.Generic;

    public class ApplicationUserServiceModel : IMapFrom<ApplicationUser>, IMapTo<ApplicationUser>
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        //public List<IdentityUserRoleServiceModel> Roles { get; set; }
    }
}