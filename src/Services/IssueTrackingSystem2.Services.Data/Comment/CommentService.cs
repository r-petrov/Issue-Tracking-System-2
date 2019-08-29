namespace IssueTrackingSystem2.Services.Data.Comment
{
    using IssueTrackingSystem2.Data.Common.Repositories;
    using IssueTrackingSystem2.Data.Models;
    using IssueTrackingSystem2.Services.Mapping;
    using IssueTrackingSystem2.Services.Models;
    using System.Linq;

    public class CommentService : ICommentService
    {
        private readonly IDeletableEntityRepository<Comment> repository;

        public CommentService(IDeletableEntityRepository<Comment> repository)
        {
            this.repository = repository;
        }

        public IQueryable<CommentServiceModel> All(string issueId)
        {
            var comments = this.repository.All().Where(comment => comment.IssueId == issueId);
            var commentServiceModels = comments.To<CommentServiceModel>();

            return commentServiceModels;
        }
    }
}
