using IssueTrackingSystem2.Services.Mapping;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace IssueTrackingSystem2.Services.Models
{
    public class IdentityUserRoleServiceModel : IMapFrom<IdentityUserRole<string>>
    {
        public string UserId { get; set; }

        public string RoleId { get; set; }
    }
}
