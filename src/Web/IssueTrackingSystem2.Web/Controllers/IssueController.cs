namespace IssueTrackingSystem2.Web.Controllers
{
    using IssueTrackingSystem2.Common.Infrastructure.Constants;
    using IssueTrackingSystem2.Services.Data.ApplicationUsers;
    using IssueTrackingSystem2.Services.Data.Issue;
    using IssueTrackingSystem2.Services.Data.Milestone;
    using IssueTrackingSystem2.Services.Data.Status;
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
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class IssueController : BaseController
    {
        private readonly IIssueService issueService;
        private readonly IMilestoneService milestoneService;
        private readonly IStatusService statusService;

        public IssueController(
            IIssueService issueService,
            IMilestoneService milestoneService,
            IStatusService statusService,
            IApplicationUserService applicationUserService)
                : base(applicationUserService)
        {
            this.issueService = issueService;
            this.milestoneService = milestoneService;
            this.statusService = statusService;
        }

        // GET: Issue/Details/5
        public async Task<ActionResult> Details(string id)
        {
            var issueServiceModel = await this.issueService.ByIdAsync(id);
            var issueDetailsViewModel = issueServiceModel.To<IssueDetailsViewModel>();

            return this.View(issueDetailsViewModel);
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
            SelectList prioritiesSelectList = this.GetDropdownPriorities(milestoneServiceModel.Project.Priorities);
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
                SelectList prioritiesSelectList = this.GetDropdownPriorities(milestoneServiceModel.Project.Priorities);
                this.ViewData[GlobalConstants.Priorities] = prioritiesSelectList;

                var usersSelectList = this.GetDropdownUsers();
                this.ViewData[GlobalConstants.Users] = usersSelectList;

                return this.View(issueCreateInputModel);
            }

            var issueServiceModel = issueCreateInputModel.To<IssueServiceModel>();
            issueServiceModel.MilestoneId = milestoneId;
            //issueServiceModel.Milestone = await this.milestoneService.ByIdAsync(milestoneId);
            issueServiceModel.Comments.Add(new CommentServiceModel()
            {
                CreatedAt = DateTime.UtcNow,
                CreatorId = issueCreateInputModel.AssigneeId,
                Text = issueCreateInputModel.Comment,
            });

            var issueServiceModelResult = await this.issueService.CreateAsync(issueServiceModel);

            return this.RedirectToAction(
                actionName: nameof(this.Details),
                routeValues: new { id = issueServiceModelResult.Id });
        }

        //// GET: Issue/Edit/5
        [HttpGet]
        [IssueAssigneeFilter]
        public async Task<ActionResult> Update(string id, string leaderId, string assigneeId)
        {
            var issueServiceModel = await this.issueService.ByIdAsync(id);
            if (issueServiceModel == null)
            {
                throw new Exception(string.Format(
                    format: MessagesConstants.NullItem,
                    arg0: GlobalConstants.Issue,
                    arg1: nameof(id),
                    arg2: id));
            }

            SelectList prioritiesSelectList = this.GetDropdownPriorities(issueServiceModel.Milestone.Project.Priorities);
            this.ViewData[GlobalConstants.Priorities] = prioritiesSelectList;

            // TODO: Try with view component
            var usersSelectList = this.GetDropdownUsers();
            this.ViewData[GlobalConstants.Users] = usersSelectList;

            var issueUpdateInputModel = issueServiceModel.To<IssueUpdateInputModel>();
            var availableStatuses = this.statusService.GetAvailableIssueStatuses(issueUpdateInputModel.StatusName);
            this.ViewData[GlobalConstants.Statuses] = availableStatuses;

            return this.View(issueUpdateInputModel);
        }

        // POST: Issue/Edit/5
        [HttpPost]
        [IssueAssigneeFilter]
        public async Task<ActionResult> Update(
            IssueUpdateInputModel issueUpdateInputModel,
            string milestoneId,
            string leaderId,
            string assigneeId)
        {
            if (issueUpdateInputModel == null)
            {
                this.ViewData[ValuesConstants.InvalidArgument] = string.Format(
                    format: MessagesConstants.NullOrEmptyArgument,
                    arg0: nameof(issueUpdateInputModel));

                return this.View();
            }

            if (!this.ModelState.IsValid)
            {
                issueUpdateInputModel.Milestone = this.SetMilestoneConciseInputModel(
                    milestoneId: milestoneId,
                    leaderId: leaderId);

                var milestoneServiceModel = await this.milestoneService.ByIdAsync(milestoneId);
                SelectList prioritiesSelectList = this.GetDropdownPriorities(milestoneServiceModel.Project.Priorities);
                this.ViewData[GlobalConstants.Priorities] = prioritiesSelectList;

                // TODO: Try with view component
                var usersSelectList = this.GetDropdownUsers();
                this.ViewData[GlobalConstants.Users] = usersSelectList;

                return this.View(issueUpdateInputModel);
            }

            var issueServiceModel = issueUpdateInputModel.To<IssueServiceModel>();
            var issueServiceModelResult = await this.issueService.UpdateAsync(issueServiceModel);

            return this.RedirectToAction(
                actionName: nameof(this.Details),
                routeValues: new { id = issueServiceModelResult.Id });
        }

        private MilestoneConciseInputModel SetMilestoneConciseInputModel(string milestoneId, string leaderId)
        {
            return new MilestoneConciseInputModel()
            {
                Id = milestoneId,
                ProjectLeaderId = leaderId,
            };
        }

        private SelectList GetDropdownPriorities(ICollection<PriorityServiceModel> priorities)
        {
            var prioritiesServiceModel = priorities
                            .Select(priority => priority.Name)
                            .ToList();

            var prioritiesSelectList = new SelectList(prioritiesServiceModel);
            return prioritiesSelectList;
        }
    }
}