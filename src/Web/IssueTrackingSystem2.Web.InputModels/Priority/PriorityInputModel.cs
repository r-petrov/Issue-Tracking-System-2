namespace IssueTrackingSystem2.Web.InputModels.Priority
{
    using IssueTrackingSystem2.Services.Mapping;
    using IssueTrackingSystem2.Services.Models;
    using System.ComponentModel.DataAnnotations;

    public class PriorityInputModel : IMapTo<PriorityServiceModel>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string ProjectId { get; set; }
    }
}
