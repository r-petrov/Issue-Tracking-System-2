namespace IssueTrackingSystem2.Web.InputModels.Comment
{
    using AutoMapper;
    using IssueTrackingSystem2.Services.Mapping;
    using IssueTrackingSystem2.Services.Models;
    using IssueTrackingSystem2.Web.InputModels.Issue;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class CommentCreateInputModel : IMapTo<CommentServiceModel>, IMapFrom<CommentServiceModel>, IHaveCustomMappings
    {
        public string Text { get; set; }

        public IssueConciseInputModel Issue { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<CommentCreateInputModel, CommentServiceModel>()
                .ForMember(dest => dest.CreatedAt, mapper => mapper.MapFrom(src => DateTime.UtcNow));
        }
    }
}
