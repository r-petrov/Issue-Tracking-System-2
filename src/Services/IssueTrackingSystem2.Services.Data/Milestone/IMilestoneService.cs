namespace IssueTrackingSystem2.Services.Data.Milestone
{
    using IssueTrackingSystem2.Services.Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IMilestoneService
    {
        IEnumerable<MilestoneServiceModel> All(string projectId);
        //IQueryable<MilestoneServiceModel> All(string projectId);

        Task<MilestoneServiceModel> CreateAsync(MilestoneServiceModel milestoneServiceModel);

        Task<MilestoneServiceModel> ByIdAsync(string id);

        Task<MilestoneServiceModel> UpdateAsync(MilestoneServiceModel milestoneServiceModel);
    }
}
