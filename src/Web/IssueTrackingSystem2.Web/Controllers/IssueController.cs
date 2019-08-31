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
    using IssueTrackingSystem2.Web.ViewModels.Milestone;
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

        public ActionResult List(string milestoneId)
        {
            var issueListServiceModels = this.issueService.AllByMilestoneId(milestoneId);
            var issueListViewModels = issueListServiceModels.To<IssueListViewModel>().ToList();
            var milestoneServiceModel = this.milestoneService.ByIdAsync(milestoneId).GetAwaiter().GetResult();
            var milestoneConciseViewModel = milestoneServiceModel.To<IssuesMilestoneViewModel>();

            var issuesViewModel = new IssuesViewModel()
            {
                Issues = issueListViewModels,
                Milestone = milestoneConciseViewModel,
            };

            return this.View(issuesViewModel);
        }

        public ActionResult ListAll()
        {
            var issueListServiceModels = this.issueService.All();
            var issueListViewModels = issueListServiceModels.To<IssueListViewModel>().ToList();

            return this.View(issueListViewModels);
        }

        // GET: Issue/Details/5
        public async Task<ActionResult> Details(string id)
        {
            try
            {
                var issueServiceModel = await this.issueService.ByIdAsync(id);
                if (issueServiceModel == null)
                {
                    throw new Exception(string.Format(
                            format: MessagesConstants.NullOrEmptyArgument,
                            arg0: nameof(id)));
                }

                var issueDetailsViewModel = issueServiceModel.To<IssueDetailsViewModel>();

                return this.View(issueDetailsViewModel);
            }
            catch (Exception ex)
            {
                this.ViewData[ValuesConstants.InvalidArgument] = ex.Message;

                return this.RedirectToAction(nameof(this.List));
            }
        }

        // GET: Issue/Create
        [HttpGet]
        [ProjectLeaderFilter]
        public async Task<ActionResult> Create(string milestoneId, string leaderId)
        {
            try
            {
                var issueCreateInputModel = new IssueCreateInputModel()
                {
                    Milestone = this.SetMilestoneConciseInputModel(milestoneId, leaderId),
                };

                await this.SetDropdowns(milestoneId);

                return this.View(issueCreateInputModel);
            }
            catch (Exception ex)
            {
                this.ViewData[ValuesConstants.InvalidArgument] = ex.Message;

                return this.RedirectToAction(
                    actionName: nameof(this.List),
                    routeValues: new { milestoneId = milestoneId });
            }
        }

        // POST: Issue/Create
        [HttpPost]
        [ProjectLeaderFilter]
        public async Task<ActionResult> Create(
            IssueCreateInputModel issueCreateInputModel,
            string milestoneId,
            string leaderId)
        {
            try
            {
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

                    await this.SetDropdowns(milestoneId);

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
            catch (Exception ex)
            {
                this.ViewData[ValuesConstants.InvalidArgument] = ex.Message;
                issueCreateInputModel.Milestone = this.SetMilestoneConciseInputModel(
                    milestoneId: milestoneId,
                    leaderId: leaderId);

                await this.SetDropdowns(milestoneId);

                return this.View(issueCreateInputModel);
            }
        }

        //// GET: Issue/Edit/5
        [HttpGet]
        [IssueAssigneeFilter]
        public async Task<ActionResult> Update(string id, string leaderId, string assigneeId)
        {
            try
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

                //SelectList prioritiesSelectList = this.GetDropdownPriorities(issueServiceModel.Milestone.Project.Priorities);
                //this.ViewData[GlobalConstants.Priorities] = prioritiesSelectList;

                //var usersSelectList = this.GetDropdownUsers();
                //this.ViewData[GlobalConstants.Users] = usersSelectList;

                await this.SetDropdowns(issueServiceModel.MilestoneId);

                var issueUpdateInputModel = issueServiceModel.To<IssueUpdateInputModel>();
                var availableStatuses = this.statusService.GetAvailableIssueStatuses(issueUpdateInputModel.StatusName);
                this.ViewData[GlobalConstants.Statuses] = availableStatuses;

                return this.View(issueUpdateInputModel);
            }
            catch (Exception ex)
            {
                this.ViewData[ValuesConstants.InvalidArgument] = ex.Message;

                return this.RedirectToAction(actionName: nameof(this.Details), routeValues: new { id = id });
            }
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
            try
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

                    await this.SetDropdowns(milestoneId: milestoneId);

                    return this.View(issueUpdateInputModel);
                }

                var issueServiceModel = issueUpdateInputModel.To<IssueServiceModel>();
                var issueServiceModelResult = await this.issueService.UpdateAsync(issueServiceModel);

                return this.RedirectToAction(
                    actionName: nameof(this.Details),
                    routeValues: new { id = issueServiceModelResult.Id });
            }
            catch (Exception ex)
            {
                this.ViewData[ValuesConstants.InvalidArgument] = ex.Message;
                issueUpdateInputModel.Milestone = this.SetMilestoneConciseInputModel(
                        milestoneId: milestoneId,
                        leaderId: leaderId);

                await this.SetDropdowns(milestoneId: milestoneId);

                return this.View(issueUpdateInputModel);
            }
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

        private async Task SetDropdowns(string milestoneId)
        {
            var milestoneServiceModel = await this.milestoneService.ByIdAsync(milestoneId);
            if (milestoneServiceModel == null)
            {
                throw new Exception(string.Format(
                        format: MessagesConstants.NullItem,
                        arg0: GlobalConstants.Milestone,
                        arg1: nameof(milestoneId),
                        arg2: milestoneId));
            }

            SelectList prioritiesSelectList = this.GetDropdownPriorities(milestoneServiceModel.Project.Priorities);
            this.ViewData[GlobalConstants.Priorities] = prioritiesSelectList;

            var usersSelectList = this.GetDropdownUsers();
            this.ViewData[GlobalConstants.Users] = usersSelectList;
        }
    }
}