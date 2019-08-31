namespace IssueTrackingSystem2.Services.Data.Label
{
    using IssueTrackingSystem2.Data.Common.Repositories;
    using IssueTrackingSystem2.Data.Models;
    using IssueTrackingSystem2.Services.Mapping;
    using IssueTrackingSystem2.Services.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class LabelService : ILabelService
    {
        private readonly IDeletableEntityRepository<Label> repository;

        public LabelService(IDeletableEntityRepository<Label> repository)
        {
            this.repository = repository;
        }

        public IEnumerable<LabelServiceModel> All()
        {
            var labels = this.repository.All().ToList();
            var labelListServiceModels = labels.To<LabelServiceModel>();

            return labelListServiceModels;
        }
    }
}
