namespace IssueTrackingSystem2.Services.Data.Comment
{
    using IssueTrackingSystem2.Services.Models;
    using System.Linq;

    public interface ICommentService
    {
        IQueryable<CommentServiceModel> All(string issueId);
    }
}
