namespace IssueTrackingSystem2.Services.Data.Milestone
{
    using IssueTrackingSystem2.Services.Models;
    using System.Threading.Tasks;

    public interface IMilestoneService
    {
        Task<MilestoneServiceModel> CreateAsync(MilestoneServiceModel milestoneServiceModel);

        Task<MilestoneServiceModel> ByIdAsync(string id);

        Task<MilestoneServiceModel> UpdateAsync(MilestoneServiceModel milestoneServiceModel);
    }
}
