namespace IssueTrackingSystem2.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using IssueTrackingSystem2.Common.Enums;
    using IssueTrackingSystem2.Services.Data.Milestone;
    using IssueTrackingSystem2.Services.Data.Project;
    using IssueTrackingSystem2.Services.Mapping;
    using IssueTrackingSystem2.Services.Models;
    using IssueTrackingSystem2.Web.Infrastructure.Constants;
    using IssueTrackingSystem2.Web.Infrastructure.Filters;
    using IssueTrackingSystem2.Web.InputModels.Milestone;
    using IssueTrackingSystem2.Web.InputModels.Project;
    using IssueTrackingSystem2.Web.ViewModels.Milestone;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    public class MilestoneController : BaseController
    {
        private readonly IMilestoneService milestoneService;

        public MilestoneController(IMilestoneService milestoneService, IProjectService projectService)
            : base(projectService)
        {
            this.milestoneService = milestoneService;
        }

        // GET: Milestone
        public ActionResult Index()
        {
            return this.View();
        }

        // GET: Milestone/Details/5
        public async Task<ActionResult> Details(string id)
        {
            var milestoneServiceModel = await this.milestoneService.ByIdAsync(id);
            var milestoneViewModel = milestoneServiceModel.To<MilestoneDetailsViewModel>();

            return this.View(milestoneViewModel);
        }

        // GET: Milestone/Create
        [HttpGet]
        [ProjectLeaderFilter]
        public async Task<ActionResult> Create(string projectId, string leaderId)
        {
            var milestoneCreateInputModel = new MilestoneCreateInputModel()
            {
                Project = new ProjectConciseInputModel()
                {
                    Id = projectId,
                    LeaderId = leaderId,
                },
            };

            return this.View(milestoneCreateInputModel);
        }

        // POST: Milestone/Create
        [HttpPost]
        [ProjectLeaderFilter]
        public async Task<ActionResult> Create(MilestoneCreateInputModel inputModel, string projectId, string leaderId)
        {
            //try
            //{
                if (!this.ModelState.IsValid)
                {
                    return this.View(inputModel);
                }

                var milestoneServiceModel = inputModel.To<MilestoneServiceModel>();
                milestoneServiceModel.Project = await this.ProjectService.ByIdAsync(projectId);
                var milestoneServiceModelResult = await this.milestoneService.CreateAsync(milestoneServiceModel);

                return this.RedirectToAction(
                    actionName: nameof(this.Details),
                    routeValues: new { id = milestoneServiceModelResult.Id });
            //}
            //catch
            //{
            //    // TODO: Implement Exception logging in file in dropbox or some other file datastore
            //    return this.View();
            //}
        }

        // GET: Milestone/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Milestone/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Milestone/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Milestone/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}