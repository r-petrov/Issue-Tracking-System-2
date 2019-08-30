namespace IssueTrackingSystem2.Web.Areas.Administration.Controllers
{
    using IssueTrackingSystem2.Common.Infrastructure.Constants;
    using IssueTrackingSystem2.Services.Data.Comment;
    using IssueTrackingSystem2.Services.Data.Issue;
    using IssueTrackingSystem2.Services.Mapping;
    using IssueTrackingSystem2.Services.Models;
    using IssueTrackingSystem2.Web.Infrastructure.Constants;
    using IssueTrackingSystem2.Web.ViewModels.Comment;
    using IssueTrackingSystem2.Web.ViewModels.Issue;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;

    public class CommentController : AdministrationController
    {
        private readonly ICommentService commentService;
        private readonly IIssueService issueService;

        public CommentController(ICommentService commentService, IIssueService issueService)
        {
            this.commentService = commentService;
            this.issueService = issueService;
        }

        //// GET: Comment/Delete/5
        [HttpGet]
        public async Task<ActionResult> Delete(string id, string issueId, string leaderId, string assigneeId)
        {
            try
            {
                var commentServiceModel = await this.commentService.ByIdAsync(id);
                if (commentServiceModel == null)
                {
                    throw new Exception(string.Format(
                            format: MessagesConstants.NullItem,
                            arg0: GlobalConstants.Comment,
                            arg1: nameof(commentServiceModel),
                            arg2: commentServiceModel));
                }

                var issueServiceModel = await this.issueService.ByIdAsync(commentServiceModel.IssueId);
                if (issueServiceModel == null)
                {
                    throw new Exception(string.Format(
                           format: MessagesConstants.NullItem,
                           arg0: GlobalConstants.Issue,
                           arg1: nameof(CommentServiceModel.IssueId),
                           arg2: commentServiceModel.IssueId));
                }

                var commentViewModel = commentServiceModel.To<CommentDetailsViewModel>();
                var issueViewModel = issueServiceModel.To<CommentDeleteIssueViewModel>();
                var commentDeleteViewModel = new CommentDeleteViewModel()
                {
                    Comment = commentViewModel,
                    Issue = issueViewModel,
                };

                return this.View(commentDeleteViewModel);
            }
            catch (Exception ex)
            {
                this.ViewData[ValuesConstants.InvalidArgument] = ex.Message;

                return this.RedirectToRoute(
                    routeName: ValuesConstants.DefaultRouteName,
                    routeValues: new
                    {
                        controller = ValuesConstants.CommentControllerName,
                        action = ValuesConstants.ListActionName,
                        issueId = issueId,
                    });
            }
        }

        // POST: Comment/Delete/5
        [HttpPost]
        public async Task<ActionResult> ConfirmDelete(string id, string issueId, string leaderId, string assigneeId)
        {
            var commentServiceModelResult = await this.commentService.RemoveSafeAsync(id);

            return this.RedirectToRoute(
                    routeName: ValuesConstants.DefaultRouteName,
                    routeValues: new
                    {
                        controller = ValuesConstants.CommentControllerName,
                        action = ValuesConstants.ListActionName,
                        issueId = issueId,
                    });
        }
    }
}