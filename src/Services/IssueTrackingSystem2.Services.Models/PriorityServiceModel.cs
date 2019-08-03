namespace IssueTrackingSystem2.Services.Models
{
    using IssueTrackingSystem2.Data.Models;
    using IssueTrackingSystem2.Services.Mapping;

    public class PriorityServiceModel : IMapFrom<Priority>, IMapTo<Priority>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ProjectId { get; set; }

        public ProjectServiceModel Project { get; set; }
    }
}