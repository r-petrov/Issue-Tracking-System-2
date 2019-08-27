using AutoMapper;
using IssueTrackingSystem2.Common.Enums;
using IssueTrackingSystem2.Services.Mapping;
using IssueTrackingSystem2.Services.Models;
using IssueTrackingSystem2.Web.Infrastructure.Attributes;
using IssueTrackingSystem2.Web.InputModels.Milestone;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IssueTrackingSystem2.Web.InputModels.Issue
{
    public class IssueCreateInputModel : IMapTo<IssueServiceModel>, IHaveCustomMappings
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Display(Name = "Due Date")]
        public DateTime DueDate { get; set; }

        [Required]
        public string Priority { get; set; }

        [Required]
        [EnumValidation(typeof(IssueStatuses))]
        public string Status { get; set; }

        [Required]
        [Display(Name = "Issue Assignee")]
        public string AssigneeId { get; set; }

        public string Comment { get; set; }

        public MilestoneConciseInputModel Milestone { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<IssueCreateInputModel, IssueServiceModel>()
                .ForMember(dest => dest.Status, mapper => mapper.MapFrom(src => new StatusServiceModel()
                {
                    Name = src.Status,
                    Type = StatusTypes.Issue.ToString(),
                }));
        }
    }
}
