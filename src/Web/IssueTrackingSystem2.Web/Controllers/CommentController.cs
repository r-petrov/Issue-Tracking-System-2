namespace IssueTrackingSystem2.Web.Controllers
{
    using IssueTrackingSystem2.Common.Infrastructure.Constants;
    using IssueTrackingSystem2.Services.Data.Comment;
    using IssueTrackingSystem2.Services.Data.Issue;
    using IssueTrackingSystem2.Services.Mapping;
    using IssueTrackingSystem2.Services.Models;
    using IssueTrackingSystem2.Web.Infrastructure.Constants;
    using IssueTrackingSystem2.Web.Infrastructure.Filters;
    using IssueTrackingSystem2.Web.InputModels.Comment;
    using IssueTrackingSystem2.Web.InputModels.Issue;
    using IssueTrackingSystem2.Web.ViewModels.Comment;
    using IssueTrackingSystem2.Web.ViewModels.Issue;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public class CommentController : BaseController
    {
        private readonly ICommentService commentService;
        private readonly IIssueService issueService;

        public CommentController(ICommentService commentService, IIssueService issueService)
        {
            this.commentService = commentService;
            this.issueService = issueService;
        }

        // GET: Comment
        public ActionResult List(string issueId)
        {
            var commentListServiceModels = this.commentService.All(issueId);
            var commentListViewModels = commentListServiceModels.To<CommentListViewModel>().ToList();
            var issueServieModel = this.issueService.ByIdAsync(issueId).GetAwaiter().GetResult();
            var issueViewModel = issueServieModel.To<CommentListIssueViewModel>();

            var commentsViewModel = new CommentsViewModel()
            {
                Comments = commentListViewModels,
                Issue = issueViewModel,
            };

            return this.View(commentsViewModel);
        }

        // GET: Comment/Create
        [HttpGet]
        [IssueAssigneeFilterAttribute]
        public async Task<ActionResult> Create(string issueId, string leaderId, string assigneeId)
        {
            try
            {
                var issueConciseInputModel = await this.SetIssueConciseInputModel(issueId);
                var commentCreateInputModel = new CommentCreateInputModel()
                {
                    Issue = issueConciseInputModel,
                };

                return this.View(commentCreateInputModel);
            }
            catch (Exception ex)
            {
                this.ViewData[ValuesConstants.InvalidArgument] = ex.Message;

                return this.RedirectToAction(
                    actionName: nameof(this.List),
                    routeValues: new { issueId = issueId });
            }
        }

        // POST: Comment/Create
        [HttpPost]
        [IssueAssigneeFilterAttribute]
        public async Task<ActionResult> Create(
            CommentCreateInputModel commentCreateInputModel,
            string issueId,
            string leaderId,
            string assigneeId)
        {
            try
            {
                if (commentCreateInputModel == null)
                {
                    this.ViewData[ValuesConstants.InvalidArgument] = string.Format(
                        format: MessagesConstants.NullOrEmptyArgument,
                        arg0: nameof(commentCreateInputModel));

                    return this.View();
                }

                if (!this.ModelState.IsValid)
                {
                    var issueConciseInputModel = await this.SetIssueConciseInputModel(issueId);
                    commentCreateInputModel.Issue = issueConciseInputModel;

                    return this.View(commentCreateInputModel);
                }

                var commentServiceModel = commentCreateInputModel.To<CommentServiceModel>();
                commentServiceModel.IssueId = issueId;
                commentServiceModel.CreatorId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

                var commentServiceModelResult = await this.commentService.CreateAsync(commentServiceModel);
                var commentCreateInputModelResult = commentServiceModelResult.To<CommentCreateInputModel>();

                return this.RedirectToAction(
                    actionName: nameof(this.List), 
                    routeValues: new { issueId = issueId });
            }
            catch (Exception ex)
            {
                this.ViewData[ValuesConstants.InvalidArgument] = ex.Message;
                var issueConciseInputModel = await this.SetIssueConciseInputModel(issueId);
                commentCreateInputModel.Issue = issueConciseInputModel;

                return this.View(commentCreateInputModel);
            }
        }

        // GET: Comment/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Comment/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return this.RedirectToAction(nameof(this.List));
            }
            catch
            {
                return View();
            }
        }

        private async Task<IssueConciseInputModel> SetIssueConciseInputModel(string issueId)
        {
            var issueServiceModel = await this.issueService.ByIdAsync(issueId);
            if (issueServiceModel == null)
            {
                throw new Exception(string.Format(
                        format: MessagesConstants.NullItem,
                        arg0: GlobalConstants.Issue,
                        arg1: nameof(issueId),
                        arg2: issueId));
            }

            var issueConciseInputModel = issueServiceModel.To<IssueConciseInputModel>();
            return issueConciseInputModel;
        }
    }
}