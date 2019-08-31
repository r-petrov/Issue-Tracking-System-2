using IssueTrackingSystem2.Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IssueTrackingSystem2.Services.Data.ApplicationUsers
{
    public interface IApplicationUserService
    {
        IEnumerable<ApplicationUserServiceModel> GetAllApplicationUsers();

        Task<ApplicationUserServiceModel> ById(string id);
    }
}
