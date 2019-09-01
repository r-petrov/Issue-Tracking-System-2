namespace IssueTrackingSystem2.Web.Controllers
{
    using IssueTrackingSystem2.Common.Infrastructure.Constants;
    using IssueTrackingSystem2.Services.Data.Milestone;
    using IssueTrackingSystem2.Services.Data.Project;
    using IssueTrackingSystem2.Services.Data.Status;
    using IssueTrackingSystem2.Services.Mapping;
    using IssueTrackingSystem2.Services.Models;
    using IssueTrackingSystem2.Web.Infrastructure.Constants;
    using IssueTrackingSystem2.Web.Infrastructure.Filters;
    using IssueTrackingSystem2.Web.InputModels.Milestone;
    using IssueTrackingSystem2.Web.InputModels.Project;
    using IssueTrackingSystem2.Web.ViewModels.Milestone;
    using IssueTrackingSystem2.Web.ViewModels.Project;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class MilestoneController : BaseController
    {
        private readonly IMilestoneService milestoneService;
        private readonly IStatusService statusService;
        private readonly IProjectService projectService;

        public MilestoneController(
            IMilestoneService milestoneService,
            IProjectService projectService,
            IStatusService statusService)
                : base(projectService)
        {
            this.milestoneService = milestoneService;
            this.statusService = statusService;
            this.projectService = projectService;
        }

        // GET: Milestone
        public ActionResult List(string projectId)
        {
            var milestoneListServiceModels = this.milestoneService.All(projectId);
            var milestoneListViewModels = milestoneListServiceModels.To<MilestoneListViewModel>().ToList();
            var projectServiceModel = this.projectService.ByIdAsync(projectId).GetAwaiter().GetResult();
            var projectConciseViewModel = projectServiceModel.To<ProjectConciseViewModel>();
            var milestonesViewModel = new MilestonesViewModel()
            {
                Milestones = milestoneListViewModels,
                Project = projectConciseViewModel,
            };

            return this.View(milestonesViewModel);
        }

        // GET: Milestone/Details/5
        public async Task<ActionResult> Details(string id)
        {
            try
            {
                var milestoneServiceModel = await this.milestoneService.ByIdAsync(id);
                if (milestoneServiceModel == null)
                {
                    throw new Exception(string.Format(
                        format: MessagesConstants.NullItem,
                        arg0: GlobalConstants.Milestone,
                        arg1: nameof(id),
                        arg2: id));
                }

                var milestoneViewModel = milestoneServiceModel.To<MilestoneDetailsViewModel>();

                return this.View(milestoneViewModel);
            }
            catch (Exception ex)
            {
                this.ViewData[ValuesConstants.InvalidArgument] = ex.Message;

                return this.RedirectToAction(nameof(this.List));
            }
        }

        // GET: Milestone/Create
        [HttpGet]
        [ProjectLeaderFilter]
        public async Task<ActionResult> Create(string projectId, string leaderId)
        {
            var milestoneCreateInputModel = new MilestoneCreateInputModel()
            {
                Project = this.SetProjectConciseInputModel(projectId: projectId, leaderId: leaderId),
            };

            return this.View(milestoneCreateInputModel);
        }

        // POST: Milestone/Create
        [HttpPost]
        [ProjectLeaderFilter]
        public async Task<ActionResult> Create(
            MilestoneCreateInputModel milestoneCreateInputModel,
            string projectId,
            string leaderId)
        {
            try
            {
                if (milestoneCreateInputModel == null)
                {
                    this.ViewData[ValuesConstants.InvalidArgument] = string.Format(
                        format: MessagesConstants.NullOrEmptyArgument,
                        arg0: nameof(milestoneCreateInputModel));

                    return this.View();
                }

                if (!this.ModelState.IsValid)
                {
                    milestoneCreateInputModel.Project = this.SetProjectConciseInputModel(projectId: projectId, leaderId: leaderId);

                    return this.View(milestoneCreateInputModel);
                }

                var milestoneServiceModel = milestoneCreateInputModel.To<MilestoneServiceModel>();
                milestoneServiceModel.ProjectId = projectId;
                //milestoneServiceModel.Project = await this.ProjectService.ByIdAsync(projectId);

                var milestoneServiceModelResult = await this.milestoneService.CreateAsync(milestoneServiceModel);

                return this.RedirectToAction(
                    actionName: nameof(this.Details),
                    routeValues: new { id = milestoneServiceModelResult.Id });
            }
            catch (Exception ex)
            {
                this.ViewData[ValuesConstants.InvalidArgument] = ex.Message;
                milestoneCreateInputModel.Project = this.SetProjectConciseInputModel(
                    projectId: projectId,
                    leaderId: leaderId);

                return this.View(milestoneCreateInputModel);
            }
        }

        // GET: Milestone/Edit/5
        [HttpGet]
        [ProjectLeaderFilter]
        public async Task<ActionResult> Update(string id, string leaderId)
        {
            try
            {
                var milestoneServiceModel = await this.milestoneService.ByIdAsync(id);
                if (milestoneServiceModel == null)
                {
                    throw new Exception(string.Format(
                        format: MessagesConstants.NullItem,
                        arg0: GlobalConstants.Milestone,
                        arg1: nameof(id),
                        arg2: id));
                }

                var milestoneUpdateInputModel = milestoneServiceModel.To<MilestoneUpdateInputModel>();

                this.SetStatusesDropdown(milestoneUpdateInputModel);

                return this.View(milestoneUpdateInputModel);
            }
            catch (Exception ex)
            {
                this.ViewData[ValuesConstants.InvalidArgument] = ex.Message;

                return this.RedirectToAction(actionName: nameof(this.Details), routeValues: new { id = id });
            }
        }

        // POST: Milestone/Edit/5
        [HttpPost]
        [ProjectLeaderFilter]
        public async Task<ActionResult> Update(
            MilestoneUpdateInputModel milestoneUpdateInputModel,
            string projectId,
            string leaderId)
        {
            try
            {
                if (milestoneUpdateInputModel == null)
                {
                    this.ViewData[ValuesConstants.InvalidArgument] = string.Format(
                        format: MessagesConstants.NullOrEmptyArgument,
                        arg0: nameof(milestoneUpdateInputModel));

                    this.SetStatusesDropdown(milestoneUpdateInputModel);

                    return this.View();
                }

                if (!this.ModelState.IsValid)
                {
                    milestoneUpdateInputModel.Project = this.SetProjectConciseInputModel(
                           projectId: projectId,
                           leaderId: leaderId);

                    this.SetStatusesDropdown(milestoneUpdateInputModel);

                    return this.View(milestoneUpdateInputModel);
                }

                var milestoneServiceModel = milestoneUpdateInputModel.To<MilestoneServiceModel>();
                var milestoneServiceModelResult = await this.milestoneService.UpdateAsync(milestoneServiceModel);

                return this.RedirectToAction(
                    actionName: nameof(this.Details),
                    routeValues: new { id = milestoneServiceModelResult.Id });
            }
            catch (Exception ex)
            {
                this.ViewData[ValuesConstants.InvalidArgument] = ex.Message;

                return this.View(milestoneUpdateInputModel);
            }
        }

        private void SetStatusesDropdown(MilestoneUpdateInputModel milestoneUpdateInputModel)
        {
            var availableStatuses = this.statusService.GetAvailableMilestoneStatuses(
                                milestoneUpdateInputModel.StatusName);

            this.ViewData[GlobalConstants.Statuses] = availableStatuses;
        }

        private ProjectConciseInputModel SetProjectConciseInputModel(string projectId, string leaderId)
        {
            return new ProjectConciseInputModel()
            {
                Id = projectId,
                LeaderId = leaderId,
            };
        }
    }
}