using AutoMapper;
using IssueTrackingSystem2.Common.Enums;
using IssueTrackingSystem2.Services.Mapping;
using IssueTrackingSystem2.Services.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IssueTrackingSystem2.Web.ViewModels.Status
{
    public class MilestoneCreateStatusInputModel : IMapTo<StatusServiceModel>, IHaveCustomMappings
    {
        [Required]
        public string Name { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<MilestoneCreateStatusInputModel, StatusServiceModel>()
                .ForMember(dest => dest.Type, mapper => mapper.MapFrom(src => StatusTypes.Milestone.ToString()));
        }
    }
}
