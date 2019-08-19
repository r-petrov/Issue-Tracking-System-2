namespace IssueTrackingSystem2.Web.ViewModels.Milestone
{
    using AutoMapper;
    using IssueTrackingSystem2.Common.Enums;
    using IssueTrackingSystem2.Services.Mapping;
    using IssueTrackingSystem2.Services.Models;
    using IssueTrackingSystem2.Web.ViewModels.Issue;
    using IssueTrackingSystem2.Web.ViewModels.Status;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class MilestoneViewModel : IMapFrom<MilestoneServiceModel>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Title { get; set; }

        [Display(Name = "Start Date")]
        public DateTime ScheduledStartDate { get; set; }

        [Display(Name = "Completion Date")]
        public DateTime ScheduledCompletionDate { get; set; }

        [Display(Name = "Start Date")]
        public DateTime? ActualStartDate { get; set; }

        [Display(Name = "Completion Date")]
        public DateTime? ActualCompletionDate { get; set; }

        public StatusConciseViewModel Status { get; set; }

        public virtual ICollection<DashboardIssueViewModel> Issues { get; set; }

        public virtual ICollection<ProjectDetailsIssueViewModel> CompletedIssues { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<MilestoneServiceModel, MilestoneViewModel>()
                .ForMember(dest => dest.CompletedIssues, mapper => mapper.MapFrom(
                    src => src.Issues.Where(issue => issue.Status.Name == IssueStatuses.Closed.ToString()
                        || issue.Status.Name.ToLower() == IssueStatuses.Resolved.ToString().ToLower())));
        }
    }
}
