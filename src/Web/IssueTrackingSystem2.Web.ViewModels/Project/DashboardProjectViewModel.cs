namespace IssueTrackingSystem2.Web.ViewModels.Project
{
    using AutoMapper;
    using IssueTrackingSystem2.Services.Mapping;
    using IssueTrackingSystem2.Services.Models;
    using IssueTrackingSystem2.Web.ViewModels.Label;
    using IssueTrackingSystem2.Web.ViewModels.Milestone;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class DashboardProjectViewModel : IMapFrom<ProjectServiceModel>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ProjectKey { get; set; }

        public ApplicationUserViewModel Leader { get; set; }

        public ICollection<DashboardLabelViewModel> Labels { get; set; }

        public ICollection<MilestoneViewModel> Milestones { get; set; }

        public int MilestonesCount { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ProjectServiceModel, DashboardProjectViewModel>()
                .ForMember(dest => dest.MilestonesCount, mapper => mapper.MapFrom(src => src.Milestones.Count));
        }
    }
}
