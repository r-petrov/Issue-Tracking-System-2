namespace IssueTrackingSystem2.Web.InputModels.Project
{
    using AutoMapper;
    using IssueTrackingSystem2.Services.Mapping;
    using IssueTrackingSystem2.Services.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class ProjectUpdateInputModel : IMapFrom<ProjectServiceModel>, IMapTo<ProjectServiceModel>, IHaveCustomMappings
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string ProjectKey { get; set; }

        [Required]
        [Display(Name = "Project Leader")]
        public string LeaderId { get; set; }

        [Required]
        public string Priorities { get; set; }

        public ICollection<LabelConciseInputModel> Labels { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ProjectUpdateInputModel, ProjectServiceModel>()
                .ForMember(dest => dest.Priorities, 
                    mapper => mapper.MapFrom(src => src.Priorities.Split(
                        new char[] { ',', ';', ' ' },
                        StringSplitOptions.RemoveEmptyEntries).Select(priorityName => new PriorityServiceModel()
                        {
                            Name = priorityName
                        })
                    ));

            configuration.CreateMap<ProjectServiceModel, ProjectUpdateInputModel>()
                .ForMember(dest => dest.Priorities, mapper => mapper.MapFrom(src => string.Join(", ", src.Priorities.Select(priority => priority.Name))));
        }
    }
}
