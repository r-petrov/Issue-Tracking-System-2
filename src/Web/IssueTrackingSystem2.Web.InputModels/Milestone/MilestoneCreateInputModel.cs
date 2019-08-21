namespace IssueTrackingSystem2.Web.InputModels.Milestone
{
    using IssueTrackingSystem2.Services.Mapping;
    using IssueTrackingSystem2.Services.Models;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class MilestoneCreateInputModel : IMapTo<MilestoneServiceModel>
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Scheduled Start Date")]
        public DateTime ScheduledStartDate { get; set; }

        [Required]
        [Display(Name = "Scheduled Completion Date")]
        public DateTime ScheduledCompletionDate { get; set; }

        [Required]
        [Display(Name = "Actual Start Date")]
        public DateTime? ActualStartDate { get; set; }

        [Required]
        [Display(Name = "Actual Completion Date")]
        public DateTime? ActualCompletionDate { get; set; }
    }
}
