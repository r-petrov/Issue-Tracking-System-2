namespace IssueTrackingSystem2.Web.ViewModels.Project
{
    using IssueTrackingSystem2.Services.Mapping;
    using IssueTrackingSystem2.Services.Models;
    using IssueTrackingSystem2.Web.ViewModels.Label;
    using IssueTrackingSystem2.Web.ViewModels.Milestone;
    using IssueTrackingSystem2.Web.ViewModels.Priority;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ProjectDetailsViewModel : IMapFrom<ProjectServiceModel>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        [Display(Name = "Project Key")]
        public string ProjectKey { get; set; }

        [Display(Name = "Leader ID")]
        public string LeaderId { get; set; }

        public ApplicationUserViewModel Leader { get; set; }

        public ICollection<MilestoneViewModel> Milestones { get; set; }

        public ICollection<PriorityConciseViewModel> Priorities { get; set; }

        public ICollection<LabelConciseViewModel> Labels { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
