namespace IssueTrackingSystem2.Services.Data.Label
{
    using IssueTrackingSystem2.Services.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ILabelService
    {
        IEnumerable<LabelServiceModel> All();

        Task<LabelServiceModel> ByIdAsync(string id);

        Task<LabelServiceModel> CreateAsync(LabelServiceModel labelServiceModel);
    }
}
