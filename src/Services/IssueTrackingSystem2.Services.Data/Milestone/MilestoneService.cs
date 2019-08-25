namespace IssueTrackingSystem2.Services.Data.Milestone
{
    using IssueTrackingSystem2.Common.Infrastructure.Constants;
    using IssueTrackingSystem2.Data.Common.Repositories;
    using IssueTrackingSystem2.Data.Models;
    using IssueTrackingSystem2.Services.Mapping;
    using IssueTrackingSystem2.Services.Models;
    using System;
    using System.Threading.Tasks;

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

        public async Task<MilestoneServiceModel> UpdateAsync(MilestoneServiceModel milestoneServiceModel)
        {
            var milestone = await this.repository.ByIdAsync(milestoneServiceModel.Id);
            if (milestone == null)
            {
                throw new Exception(string.Format(
                   format: MessagesConstants.NullItem,
                   arg0: GlobalConstants.Milestone,
                   arg1: nameof(MilestoneServiceModel.Id),
                   arg2: milestoneServiceModel.Id));
            }

            milestone.Title = milestoneServiceModel.Title;
            milestone.Description = milestoneServiceModel.Description;
            milestone.StartDate = milestoneServiceModel.StartDate;
            milestone.CompletionDate = milestoneServiceModel.CompletionDate;
            milestone.StatusId = milestoneServiceModel.StatusId;
            milestone.Status.Name = milestoneServiceModel.Status.Name;
            milestone.Status.Type = milestoneServiceModel.Status.Type;

            var milestoneResult = await this.repository.UpdateAsync(milestone);
            var milestoneServiceModelResult = milestoneResult.To<MilestoneServiceModel>();

            return milestoneServiceModelResult;
        }
    }
}
