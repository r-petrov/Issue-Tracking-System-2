namespace IssueTrackingSystem2.Web.ViewModels.Comment
{
    using IssueTrackingSystem2.Web.ViewModels.Issue;
    using System.Collections.Generic;

    public class CommentsViewModel
    {
        public ICollection<CommentListViewModel> Comments { get; set; }

        public CommentListIssueViewModel Issue { get; set; }
    }
}
