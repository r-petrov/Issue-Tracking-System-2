namespace IssueTrackingSystem2.Services.Data.Priority
{
    using IssueTrackingSystem2.Services.Models;
    using System.Threading.Tasks;

    public interface IPriorityService
    {
        Task<PriorityServiceModel> CreateAsync(PriorityServiceModel projectServiceModel);
    }
}
