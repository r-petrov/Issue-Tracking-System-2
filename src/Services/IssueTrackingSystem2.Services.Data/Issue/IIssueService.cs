namespace IssueTrackingSystem2.Services.Data.Issue
{
    using IssueTrackingSystem2.Services.Models;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IIssueService
    {
        IQueryable<IssueServiceModel> All(string milestoneId);

        Task<IssueServiceModel> CreateAsync(IssueServiceModel issueServiceModel);

        Task<IssueServiceModel> ByIdAsync(string id);

        Task<IssueServiceModel> UpdateAsync(IssueServiceModel issueServiceModel);
    }
}
