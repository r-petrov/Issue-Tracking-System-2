namespace IssueTrackingSystem2.Web.ViewModels.Label
{
    using IssueTrackingSystem2.Services.Mapping;
    using IssueTrackingSystem2.Services.Models;

    public class LabelConciseViewModel : IMapFrom<LabelServiceModel>
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Color { get; set; }
    }
}
