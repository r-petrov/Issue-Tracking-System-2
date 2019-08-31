namespace IssueTrackingSystem2.Web.ViewModels.Milestone
{
    using AutoMapper;
    using IssueTrackingSystem2.Common.Enums;
    using IssueTrackingSystem2.Services.Mapping;
    using IssueTrackingSystem2.Services.Models;
    using IssueTrackingSystem2.Web.ViewModels.Issue;
    using IssueTrackingSystem2.Web.ViewModels.Project;
    using IssueTrackingSystem2.Web.ViewModels.Status;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class MilestoneListViewModel : IMapFrom<MilestoneServiceModel>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Title { get; set; }

        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Completion Date")]
        public DateTime CompletionDate { get; set; }

        public virtual StatusConciseViewModel Status { get; set; }

        public virtual ProjectConciseViewModel Project { get; set; }

        public virtual ICollection<ProjectListIssueViewModel> Issues { get; set; }

        public virtual ICollection<ProjectDetailsIssueViewModel> CompletedIssues { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<MilestoneServiceModel, MilestoneListViewModel>()
                .ForMember(dest => dest.CompletedIssues, mapper => mapper.MapFrom(
                    src => src.Issues.Where(issue => issue.Status.Name == IssueStatuses.Closed.ToString()
                        || issue.Status.Name.ToLower() == IssueStatuses.Resolved.ToString().ToLower())));
        }
    }
}
