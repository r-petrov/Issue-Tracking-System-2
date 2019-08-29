namespace IssueTrackingSystem2.Web.ViewModels.Issue
{
    using AutoMapper;
    using IssueTrackingSystem2.Services.Mapping;
    using IssueTrackingSystem2.Services.Models;
    using IssueTrackingSystem2.Web.ViewModels.Label;
    using IssueTrackingSystem2.Web.ViewModels.Status;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class IssueListViewModel : IMapFrom<IssueServiceModel>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public virtual StatusConciseViewModel Status { get; set; }

        public DateTime DueDate { get; set; }

        public virtual ApplicationUserViewModel Assignee { get; set; }

        public virtual ICollection<LabelConciseViewModel> Labels { get; set; }

        public int Comments { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<IssueServiceModel, IssueListViewModel>()
                .ForMember(dest => dest.Comments, mapper => mapper.MapFrom(src => src.Comments.Count));
        }
    }
}
