namespace IssueTrackingSystem2.Services.Data.Issue
{
    using IssueTrackingSystem2.Services.Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IIssueService
    {
        IEnumerable<IssueServiceModel> ListAll();

        IEnumerable<IssueServiceModel> List(string milestoneId);
        //IQueryable<IssueServiceModel> All(string milestoneId);

        Task<IssueServiceModel> CreateAsync(IssueServiceModel issueServiceModel);

        Task<IssueServiceModel> ByIdAsync(string id);

        Task<IssueServiceModel> UpdateAsync(IssueServiceModel issueServiceModel);
    }
}
