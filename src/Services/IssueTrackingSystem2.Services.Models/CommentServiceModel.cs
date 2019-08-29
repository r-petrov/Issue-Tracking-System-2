namespace IssueTrackingSystem2.Services.Models
{
    using IssueTrackingSystem2.Data.Models;
    using IssueTrackingSystem2.Services.Mapping;
    using System;

    public class CommentServiceModel : IMapFrom<Comment>, IMapTo<Comment>
    {
        public string Id { get; set; }

        public string Text { get; set; }

        public string IssueId { get; set; }

        public IssueServiceModel Issue { get; set; }

        public string CreatorId { get; set; }

        public ApplicationUserServiceModel CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}