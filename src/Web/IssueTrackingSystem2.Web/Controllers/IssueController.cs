namespace IssueTrackingSystem2.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using IssueTrackingSystem2.Common.Infrastructure.Constants;
    using IssueTrackingSystem2.Services.Data.ApplicationUsers;
    using IssueTrackingSystem2.Services.Data.Issue;
    using IssueTrackingSystem2.Services.Data.Milestone;
    using IssueTrackingSystem2.Services.Mapping;
    using IssueTrackingSystem2.Services.Models;
    using IssueTrackingSystem2.Web.Infrastructure.Constants;
    using IssueTrackingSystem2.Web.Infrastructure.Filters;
    using IssueTrackingSystem2.Web.InputModels.Issue;
    using IssueTrackingSystem2.Web.InputModels.Milestone;
    using IssueTrackingSystem2.Web.ViewModels.Issue;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class IssueController : BaseController
    {
        private readonly IIssueService issueService;
        private readonly IMilestoneService milestoneService;

        public IssueController(
            IIssueService issueService, 
            IMilestoneService milestoneService, 
            IApplicationUserService applicationUserService)
                : base(applicationUserService)
        {
            this.issueService = issueService;
            this.milestoneService = milestoneService;
        }

        // GET: Issue/Details/5
        public async Task<ActionResult> Details(string id)
        {
            var issueServiceModel = await this.issueService.ByIdAsync(id);
            var issueDetailsViewModel = issueServiceModel.To<IssueDetailsViewModel>();

            return View();
        }

        // GET: Issue/Create
        [HttpGet]
        [ProjectLeaderFilter]
        public async Task<ActionResult> Create(string milestoneId, string leaderId)
        {
            var issueCreateInputModel = new IssueCreateInputModel()
            {
                Milestone = this.SetMilestoneConciseInputModel(milestoneId, leaderId),
            };

            var milestoneServiceModel = await this.milestoneService.ByIdAsync(milestoneId);
            SelectList prioritiesSelectList = this.GetDropdownPriorities(milestoneServiceModel);
            this.ViewData[GlobalConstants.Priorities] = prioritiesSelectList;

            var usersSelectList = this.GetDropdownUsers();
            this.ViewData[GlobalConstants.Users] = usersSelectList;

            return this.View(issueCreateInputModel);
        }

        // POST: Issue/Create
        [HttpPost]
        [ProjectLeaderFilter]
        public async Task<ActionResult> Create(
            IssueCreateInputModel issueCreateInputModel,
            string milestoneId,
            string leaderId)
        {
            //try
            //{
                if (issueCreateInputModel == null)
                {
                    this.ViewData[ValuesConstants.InvalidArgument] = string.Format(
                        format: MessagesConstants.NullOrEmptyArgument,
                        arg0: nameof(issueCreateInputModel));

                    return this.View();
                }

                if (!this.ModelState.IsValid)
                {
                    issueCreateInputModel.Milestone = this.SetMilestoneConciseInputModel(
                        milestoneId: milestoneId,
                        leaderId: leaderId);

                    var milestoneServiceModel = await this.milestoneService.ByIdAsync(milestoneId);
                    SelectList prioritiesSelectList = this.GetDropdownPriorities(milestoneServiceModel);
                    this.ViewData[GlobalConstants.Priorities] = prioritiesSelectList;

                    var usersSelectList = this.GetDropdownUsers();
                    this.ViewData[GlobalConstants.Users] = usersSelectList;

                    return this.View(issueCreateInputModel);
                }

                var issueServiceModel = issueCreateInputModel.To<IssueServiceModel>();
                issueServiceModel.MilestoneId = milestoneId;
                //issueServiceModel.Milestone = await this.milestoneService.ByIdAsync(milestoneId);

                var issueServiceModelResult = await this.issueService.CreateAsync(issueServiceModel);

                return this.RedirectToAction(
                    actionName: nameof(this.Details), 
                    routeValues: new { id = issueServiceModelResult.Id });
            //}
            //catch
            //{
            //    return View();
            //}
        }

        //// GET: Issue/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: Issue/Edit/5
        //[HttpPost]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Issue/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        private MilestoneConciseInputModel SetMilestoneConciseInputModel(string milestoneId, string leaderId)
        {
            return new MilestoneConciseInputModel()
            {
                Id = milestoneId,
                ProjectLeaderId = leaderId,
            };
        }

        private SelectList GetDropdownPriorities(MilestoneServiceModel milestoneServiceModel)
        {
            var prioritiesServiceModel = milestoneServiceModel.Project.Priorities
                            .Select(priority => priority.Name)
                            .ToList();

            var prioritiesSelectList = new SelectList(prioritiesServiceModel);
            return prioritiesSelectList;
        }
    }
}