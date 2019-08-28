namespace IssueTrackingSystem2.Web.InputModels.Milestone
{
    using IssueTrackingSystem2.Services.Mapping;
    using IssueTrackingSystem2.Services.Models;

    public class MilestoneConciseInputModel : IMapFrom<MilestoneServiceModel>, IMapTo<MilestoneServiceModel>
    {
        public string Id { get; set; }

        public string ProjectLeaderId { get; set; }
    }
}
