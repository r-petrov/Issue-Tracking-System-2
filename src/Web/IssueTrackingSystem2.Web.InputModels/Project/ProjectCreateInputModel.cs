namespace IssueTrackingSystem2.Web.InputModels.Project
{
    using AutoMapper;
    using IssueTrackingSystem2.Services.Mapping;
    using IssueTrackingSystem2.Services.Models;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ProjectCreateInputModel : IMapTo<ProjectServiceModel>//, IHaveCustomMappings
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Project Leader")]
        public string LeaderId { get; set; }

        [Required]
        public string Priorities { get; set; }

        //public void CreateMappings(IProfileExpression configuration)
        //{
        ////    configuration.CreateMap<ProjectCreateInputModel, ProjectServiceModel>()
        ////        .ForMember(dest => dest.CreatedOn, mapper => mapper.MapFrom(src => DateTime.UtcNow));
        //}
    }
}
