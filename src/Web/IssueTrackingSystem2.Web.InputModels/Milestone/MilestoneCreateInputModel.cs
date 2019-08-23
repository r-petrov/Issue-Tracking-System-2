namespace IssueTrackingSystem2.Web.InputModels.Milestone
{
    using AutoMapper;
    using IssueTrackingSystem2.Common.Enums;
    using IssueTrackingSystem2.Services.Mapping;
    using IssueTrackingSystem2.Services.Models;
    using IssueTrackingSystem2.Web.InputModels.Project;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class MilestoneCreateInputModel : IMapTo<MilestoneServiceModel>, IHaveCustomMappings
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Scheduled Start Date")]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "Scheduled Completion Date")]
        public DateTime CompletionDate { get; set; }

        public string Status { get; set; }

        public ProjectConciseInputModel Project { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<MilestoneCreateInputModel, MilestoneServiceModel>()
                .ForMember(dest => dest.Status, mapper => mapper.MapFrom(src => new StatusServiceModel()
                {
                    Name = src.Status,
                    Type = StatusTypes.Milestone.ToString(),
                }));
        }

        //[Required]
        //[Display(Name = "Actual Start Date")]
        //public DateTime? ActualStartDate { get; set; }

        //[Required]
        //[Display(Name = "Actual Completion Date")]
        //public DateTime? ActualCompletionDate { get; set; }

    }
}
