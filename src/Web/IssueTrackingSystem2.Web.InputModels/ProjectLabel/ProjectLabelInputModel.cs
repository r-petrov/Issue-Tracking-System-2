namespace IssueTrackingSystem2.Web.InputModels.ProjectLabel
{
    using IssueTrackingSystem2.Services.Data;
    using IssueTrackingSystem2.Services.Mapping;
    using System.ComponentModel.DataAnnotations;

    public class ProjectLabelInputModel : IMapTo<ProjectLabelServiceModel>, IMapFrom<ProjectLabelServiceModel>
    {
        [Required]
        public string ProjectId { get; set; }

        [Required]
        [Display(Name = "Leader ID")]
        public string LabelId { get; set; }
    }
}
