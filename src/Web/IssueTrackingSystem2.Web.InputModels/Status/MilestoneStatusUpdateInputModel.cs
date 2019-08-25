namespace IssueTrackingSystem2.Web.ViewModels.Status
{
    using AutoMapper;
    using IssueTrackingSystem2.Common.Enums;
    using IssueTrackingSystem2.Services.Mapping;
    using IssueTrackingSystem2.Services.Models;
    using IssueTrackingSystem2.Web.Infrastructure.Attributes;
    using System.ComponentModel.DataAnnotations;

    public class MilestoneStatusUpdateInputModel : IMapTo<StatusServiceModel>, IHaveCustomMappings
    {
        public int Id { get; set; }

        [Required]
        [EnumValidation(typeof(MilestoneStatuses))]
        public string Name { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<MilestoneStatusUpdateInputModel, StatusServiceModel>()
                .ForMember(dest => dest.Type, mapper => mapper.MapFrom(src => StatusTypes.Milestone.ToString()));
        }
    }
}
