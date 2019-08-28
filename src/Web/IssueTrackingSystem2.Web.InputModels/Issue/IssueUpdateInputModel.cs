namespace IssueTrackingSystem2.Web.InputModels.Issue
{
    using AutoMapper;
    using IssueTrackingSystem2.Common.Enums;
    using IssueTrackingSystem2.Services.Mapping;
    using IssueTrackingSystem2.Services.Models;
    using IssueTrackingSystem2.Web.Infrastructure.Attributes;
    using IssueTrackingSystem2.Web.InputModels.Milestone;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class IssueUpdateInputModel : IMapFrom<IssueServiceModel>, IMapTo<IssueServiceModel>, IHaveCustomMappings
    {
        public string Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Display(Name = "Due Date")]
        [DateTimeValidation(nameof(DueDate))]
        public DateTime DueDate { get; set; }

        [Required]
        public string Priority { get; set; }

        [Required]
        [EnumValidation(typeof(IssueStatuses))]
        [Display(Name = "Status Name")]
        public string StatusName { get; set; }

        [Required]
        [Display(Name = "Issue Assignee")]
        public string AssigneeId { get; set; }

        public MilestoneConciseInputModel Milestone { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<IssueUpdateInputModel, IssueServiceModel>()
                .ForMember(dest => dest.Status, mapper => mapper.MapFrom(src => new StatusServiceModel()
                {
                    Name = src.StatusName,
                    Type = StatusTypes.Issue.ToString(),
                }));
        }
    }
}
