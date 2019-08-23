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
    using System.Text;

    public class MilestoneDetailsViewModel : IMapFrom<MilestoneServiceModel>
    {
        public string Id { get; set; }

        public string Title { get; set; }

        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Completion Date")]
        public DateTime CompletionDate { get; set; }

        public ProjectConciseViewModel Project { get; set; }

        public StatusConciseViewModel Status { get; set; }

        public virtual ICollection<IssueListViewModel> Issues { get; set; }

        [Display(Name = "Completed Issues")]
        public virtual ICollection<IssueListViewModel> CompletedIssues { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<MilestoneServiceModel, MilestoneListViewModel>()
                .ForMember(dest => dest.CompletedIssues, mapper => mapper.MapFrom(
                    src => src.Issues.Where(issue => issue.Status.Name == IssueStatuses.Closed.ToString()
                        || issue.Status.Name.ToLower() == IssueStatuses.Resolved.ToString().ToLower())));
        }
    }
}
