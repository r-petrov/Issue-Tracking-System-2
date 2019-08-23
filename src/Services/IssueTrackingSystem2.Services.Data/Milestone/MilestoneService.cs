namespace IssueTrackingSystem2.Services.Data.Milestone
{
    using IssueTrackingSystem2.Data.Models;
    using IssueTrackingSystem2.Data.Common.Repositories;
    using IssueTrackingSystem2.Services.Mapping;
    using IssueTrackingSystem2.Services.Models;
    using System;
    using System.Threading.Tasks;
    using IssueTrackingSystem2.Common;

    public class MilestoneService : IMilestoneService
    {
        private readonly IDeletableEntityRepository<Milestone> repository;

        public MilestoneService(IDeletableEntityRepository<Milestone> repository)
        {
            this.repository = repository;
        }

        public async Task<MilestoneServiceModel> CreateAsync(MilestoneServiceModel milestoneServiceModel)
        {
            var milestone = milestoneServiceModel.To<Milestone>();
            var milestoneResult = await this.repository.AddAsync(milestone);
            var milestoneServiceModelResult = milestoneResult.To<MilestoneServiceModel>();

            return milestoneServiceModelResult;
        }

        public async Task<MilestoneServiceModel> ByIdAsync(string id)
        {
            var milestone = await this.repository.ByIdAsync(id);
            if (milestone == null)
            {
                throw new Exception(string.Format(
                    format: MessagesConstants.NullItem,
                    arg0: nameof(milestone),
                    arg1: nameof(id),
                    arg2: id));
            }

            var milestoneServiceModel = milestone.To<MilestoneServiceModel>();

            return milestoneServiceModel;
        }
    }
}
