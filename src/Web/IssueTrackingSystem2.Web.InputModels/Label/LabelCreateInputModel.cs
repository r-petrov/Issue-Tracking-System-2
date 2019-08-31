namespace IssueTrackingSystem2.Web.InputModels.Label
{
    using AutoMapper;
    using IssueTrackingSystem2.Services.Mapping;
    using IssueTrackingSystem2.Services.Models;
    using System;

    public class LabelCreateInputModel : IMapTo<LabelServiceModel>, IMapFrom<LabelServiceModel>, IHaveCustomMappings
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Color { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<LabelCreateInputModel, LabelServiceModel>()
                .ForMember(dest => dest.CreatedAt, mapper => mapper.MapFrom(src => DateTime.UtcNow));
        }
    }
}
