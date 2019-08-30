namespace IssueTrackingSystem2.Web.ViewModels.Comment
{
    using IssueTrackingSystem2.Services.Mapping;
    using IssueTrackingSystem2.Services.Models;
    using System;

    public class CommentListViewModel : IMapFrom<CommentServiceModel>
    {
        public string Id { get; set; }

        public string Text { get; set; }

        public ApplicationUserViewModel CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
