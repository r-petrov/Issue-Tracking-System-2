namespace IssueTrackingSystem2.Services.Data.Priorities
{
    using IssueTrackingSystem2.Data.Common.Repositories;
    using IssueTrackingSystem2.Data.Models;
    using IssueTrackingSystem2.Services.Mapping;
    using IssueTrackingSystem2.Services.Models;
    using System.Threading.Tasks;

    public class PriorityService : IPriorityService
    {
        private readonly IssueTrackingSystem2.Data.Common.Repositories.IDeletableEntityRepository<Priority> repository;

        public PriorityService(IDeletableEntityRepository<Priority> repository)
        {
            this.repository = repository;
        }

        public async Task<PriorityServiceModel> CreateAsync(PriorityServiceModel priorityServiceModel)
        {
            var priority = priorityServiceModel.To<Priority>();
            var priorityResult = await this.repository.AddAsync(priority);
            var priorityServiceModelResult = priorityResult.To<PriorityServiceModel>();

            return priorityServiceModelResult;
        }
    }
}
