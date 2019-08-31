using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IssueTrackingSystem2.Common.Infrastructure.Constants;
using IssueTrackingSystem2.Services.Data.Issue;
using IssueTrackingSystem2.Services.Data.Label;
using IssueTrackingSystem2.Services.Data.Project;
using IssueTrackingSystem2.Services.Mapping;
using IssueTrackingSystem2.Services.Models;
using IssueTrackingSystem2.Web.Infrastructure.Constants;
using IssueTrackingSystem2.Web.Infrastructure.Filters;
using IssueTrackingSystem2.Web.InputModels;
using IssueTrackingSystem2.Web.InputModels.Label;
using IssueTrackingSystem2.Web.ViewModels.Issue;
using IssueTrackingSystem2.Web.ViewModels.Label;
using IssueTrackingSystem2.Web.ViewModels.Project;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IssueTrackingSystem2.Web.Controllers
{
    public class LabelController : Controller
    {
        private readonly ILabelService labelService;
        private readonly IProjectService projectService;
        private readonly IIssueService issueService;

        public LabelController(ILabelService labelService, IProjectService projectService, IIssueService issueService)
        {
            this.labelService = labelService;
            this.projectService = projectService;
            this.issueService = issueService;
        }

        // GET: Label
        public ActionResult List()
        {
            var labelListServiceModels = this.labelService.All();
            var labelListViewModels = labelListServiceModels.To<LabelListViewModel>().ToList();
            var projectListServiceModels = this.projectService.All();
            var projectConciseListViewModels = projectListServiceModels.To<ProjectConciseViewModel>().ToList();
            var issueListServiceModels = this.issueService.All();
            var issueConciseListViewModels = issueListServiceModels.To<IssueConciseViewModel>().ToList();

            var labelsViewModel = new LabelsViewModel()
            {
                Labels = labelListViewModels,
                Projects = projectConciseListViewModels,
                Issues = issueConciseListViewModels,
            };

            return this.View(labelsViewModel);
        }

        // GET: Label/Details/5
        public async Task<ActionResult> Details(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    throw new Exception(string.Format(
                        format: MessagesConstants.NullOrEmptyArgument,
                        arg0: nameof(id)));
                }

                var labelServiceModel = await this.labelService.ByIdAsync(id);
                var labelDetailsViewModel = labelServiceModel.To<LabelDetailsViewModel>();

                return this.View(labelDetailsViewModel);
            }
            catch (Exception ex)
            {
                this.ViewData[ValuesConstants.InvalidArgument] = ex.Message;

                return this.RedirectToAction(actionName: nameof(this.List));
            }
        }

        // GET: Label/Create
        [HttpGet]
        [IssueAssigneeFilterAttribute]
        public ActionResult Create(string leaderId, string assigneeId)
        {
            this.ViewData[ValuesConstants.LeaderId] = leaderId;
            this.ViewData[ValuesConstants.AssigneeId] = assigneeId;

            return this.View();
        }

        // POST: Label/Create
        [HttpPost]
        [IssueAssigneeFilterAttribute]
        public async Task<ActionResult> Create(
            LabelCreateInputModel labelCreateInputModel,
            string leaderId,
            string assigneeId)
        {
            try
            {
                if (labelCreateInputModel == null)
                {
                    this.ViewData[ValuesConstants.InvalidArgument] = string.Format(
                        format: MessagesConstants.NullOrEmptyArgument,
                        arg0: nameof(labelCreateInputModel));

                    return this.View();
                }

                if (!this.ModelState.IsValid)
                {
                    this.ViewData[ValuesConstants.LeaderId] = leaderId;
                    this.ViewData[ValuesConstants.AssigneeId] = assigneeId;

                    return this.View(labelCreateInputModel);
                }

                var labelServiceModel = labelCreateInputModel.To<LabelServiceModel>();
                labelServiceModel.CreatorId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

                var labelServiceModelResult = await this.labelService.CreateAsync(labelServiceModel);

                return this.RedirectToAction(nameof(this.List));
            }
            catch (Exception ex)
            {
                this.ViewData[ValuesConstants.InvalidArgument] = ex.Message;

                return this.View(labelCreateInputModel);
            }
        }

        // GET: Label/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Label/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(List));
            }
            catch
            {
                return View();
            }
        }
    }
}