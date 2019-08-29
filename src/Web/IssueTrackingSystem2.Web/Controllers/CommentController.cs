namespace IssueTrackingSystem2.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using IssueTrackingSystem2.Services.Data.Comment;
    using IssueTrackingSystem2.Services.Data.Issue;
    using IssueTrackingSystem2.Services.Mapping;
    using IssueTrackingSystem2.Services.Models;
    using IssueTrackingSystem2.Web.ViewModels.Comment;
    using IssueTrackingSystem2.Web.ViewModels.Issue;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

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
            var commentListServiceModels = this.commentService.All(issueId).ToList();
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
        public ActionResult Create()
        {
            return View();
        }

        // POST: Comment/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return this.RedirectToAction(nameof(this.List));
            }
            catch
            {
                return View();
            }
        }

        // GET: Comment/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Comment/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(this.List));
            }
            catch
            {
                return View();
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
    }
}