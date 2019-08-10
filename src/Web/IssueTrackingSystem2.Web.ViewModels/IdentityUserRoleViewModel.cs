using IssueTrackingSystem2.Services.Mapping;
using IssueTrackingSystem2.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IssueTrackingSystem2.Web.ViewModels
{
    public class IdentityUserRoleViewModel : IMapFrom<IdentityUserRoleServiceModel>
    {
        public string UserId { get; set; }

        public string RoleId { get; set; }
    }
}
