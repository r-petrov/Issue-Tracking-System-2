namespace IssueTrackingSystem2.Services.Data.Comment
{
    using IssueTrackingSystem2.Services.Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface ICommentService
    {
        IEnumerable<CommentServiceModel> All(string issueId);
        //IQueryable<CommentServiceModel> All(string issueId);
        Task<CommentServiceModel> ByIdAsync(string id);

        Task<CommentServiceModel> CreateAsync(CommentServiceModel commentServiceModel);

        Task<CommentServiceModel> RemoveSafeAsync(string id);
    }
}
