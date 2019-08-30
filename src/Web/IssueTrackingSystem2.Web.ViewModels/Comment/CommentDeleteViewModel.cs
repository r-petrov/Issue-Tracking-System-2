namespace IssueTrackingSystem2.Web.ViewModels.Comment
{
    using IssueTrackingSystem2.Web.ViewModels.Issue;

    public class CommentDeleteViewModel
    {
        public CommentDetailsViewModel Comment { get; set; }

        public CommentDeleteIssueViewModel Issue { get; set; }
    }
}
