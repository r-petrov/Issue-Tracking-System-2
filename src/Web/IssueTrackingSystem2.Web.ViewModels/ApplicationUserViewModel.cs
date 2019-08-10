namespace IssueTrackingSystem2.Web.ViewModels
{
    using AutoMapper;
    using IssueTrackingSystem2.Services.Mapping;
    using IssueTrackingSystem2.Services.Models;
    using System.Collections.Generic;

    public class ApplicationUserViewModel : IMapFrom<ApplicationUserServiceModel>
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        //public List<IdentityUserRoleViewModel> Roles { get; set; }
    }
}
