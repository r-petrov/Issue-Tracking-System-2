namespace IssueTrackingSystem2.Web.ViewModels.Project
{
    using AutoMapper;
    using IssueTrackingSystem2.Common.Enums;
    using IssueTrackingSystem2.Services.Mapping;
    using IssueTrackingSystem2.Services.Models;
    using IssueTrackingSystem2.Web.ViewModels.Label;
    using IssueTrackingSystem2.Web.ViewModels.Milestone;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class DashboardProjectViewModel : IMapFrom<ProjectServiceModel>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ProjectKey { get; set; }

        public ApplicationUserViewModel Leader { get; set; }

        public ICollection<LabelConciseViewModel> Labels { get; set; }

        public ICollection<MilestoneViewModel> Milestones { get; set; }

        public ICollection<MilestoneViewModel> CompletedMilestones { get; set; }

        public DateTime CreatedOn { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ProjectServiceModel, DashboardProjectViewModel>()
                .ForMember(dest => dest.CompletedMilestones, mapper => mapper.MapFrom(
                    src => src.Milestones.Where(milestone =>
                        milestone.Status.Name == MilestoneStatuses.Completed.ToString())));
        }
    }
}
