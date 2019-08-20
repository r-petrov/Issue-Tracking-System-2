using IssueTrackingSystem2.Services.Mapping;
using IssueTrackingSystem2.Services.Models;

namespace IssueTrackingSystem2.Web.InputModels.Project
{
    public class LabelConciseInputModel : IMapFrom<LabelServiceModel>
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Color { get; set; }
    }
}