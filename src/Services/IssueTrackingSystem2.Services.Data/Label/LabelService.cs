namespace IssueTrackingSystem2.Services.Data.Label
{
    using IssueTrackingSystem2.Common.Infrastructure.Constants;
    using IssueTrackingSystem2.Data.Common.Repositories;
    using IssueTrackingSystem2.Data.Models;
    using IssueTrackingSystem2.Services.Mapping;
    using IssueTrackingSystem2.Services.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

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

        public async Task<LabelServiceModel> ByIdAsync(string id)
        {
            var label = await this.repository.ByIdAsync(id);
            if (label == null)
            {
                throw new Exception(string.Format(
                    format: MessagesConstants.NullItem,
                    arg0: nameof(label),
                    arg1: nameof(id),
                    arg2: id));
            }

            var labelServiceModel = label.To<LabelServiceModel>();

            return labelServiceModel;
        }

        public async Task<LabelServiceModel> CreateAsync(LabelServiceModel labelServiceModel)
        {
            var label = labelServiceModel.To<Label>();
            var labelResult = await this.repository.AddAsync(label);
            var labelServiceModelResult = labelResult.To<LabelServiceModel>();

            return labelServiceModelResult;
        }
    }
}
