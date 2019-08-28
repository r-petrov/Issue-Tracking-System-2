namespace IssueTrackingSystem2.Services.Data.Issue
{
    using IssueTrackingSystem2.Services.Models;
    using System.Threading.Tasks;

    public interface IIssueService
    {
        Task<IssueServiceModel> CreateAsync(IssueServiceModel issueServiceModel);

        Task<IssueServiceModel> ByIdAsync(string id);

        Task<IssueServiceModel> UpdateAsync(IssueServiceModel issueServiceModel);
    }
}
