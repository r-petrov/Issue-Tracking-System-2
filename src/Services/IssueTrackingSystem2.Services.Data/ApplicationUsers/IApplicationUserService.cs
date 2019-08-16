using IssueTrackingSystem2.Services.Models;
using System.Collections.Generic;

namespace IssueTrackingSystem2.Services.Data.ApplicationUsers
{
    public interface IApplicationUserService
    {
        IEnumerable<ApplicationUserServiceModel> GetAllApplicationUsers();
    }
}
