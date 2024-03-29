﻿namespace IssueTrackingSystem2.Web.Controllers
{
    using IssueTrackingSystem2.Common;
    using IssueTrackingSystem2.Common.Infrastructure.Constants;
    using IssueTrackingSystem2.Services.Data.ApplicationUsers;
    using IssueTrackingSystem2.Services.Data.Label;
    using IssueTrackingSystem2.Services.Data.Project;
    using IssueTrackingSystem2.Services.Mapping;
    using IssueTrackingSystem2.Services.Models;
    using IssueTrackingSystem2.Web.Infrastructure.Constants;
    using IssueTrackingSystem2.Web.Infrastructure.Filters;
    using IssueTrackingSystem2.Web.InputModels.Project;
    using IssueTrackingSystem2.Web.InputModels.ProjectLabel;
    using IssueTrackingSystem2.Web.ViewModels.Project;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ProjectController : BaseController
    {
        private readonly IProjectService projectService;
        private readonly ILabelService labelService;

        public ProjectController(IProjectService projectService, IApplicationUserService applicationUserService, ILabelService labelService)
                : base(applicationUserService: applicationUserService, labelService: labelService)
        {
            this.projectService = projectService;
            this.labelService = labelService;
        }

        public ActionResult List()
        {
            // TODO: Add Pagination
            // TODO: Add filters, i.e. Project name
            var projectListServiceModels = this.projectService.All();
            var projectListViewModels = projectListServiceModels
                    .To<ProjectListViewModel>()
                    .OrderByDescending(project => project.CreatedOn);

            return this.View(projectListViewModels);
        }

        // GET: Project/Details/5
        [HttpGet]
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

                var project = await this.projectService.ByIdAsync(id);
                var projectViewModel = project.To<ProjectDetailsViewModel>();

                return this.View(projectViewModel);
            }
            catch (Exception ex)
            {
                this.ViewData[ValuesConstants.InvalidArgument] = ex.Message;

                return this.RedirectToAction(actionName: nameof(this.List));
            }
        }

        [HttpGet]
        [ProjectLeaderFilter]
        public async Task<ActionResult> Update(string id, string leaderId)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    throw new Exception(string.Format(
                        format: MessagesConstants.NullOrEmptyArgument,
                        arg0: nameof(id)));
                }

                var projectServiceModel = await this.projectService.ByIdAsync(id);
                var projectUpdateInputModel = projectServiceModel.To<ProjectUpdateInputModel>();

                if (this.User.IsInRole(GlobalConstants.AdministratorRoleName))
                {
                    var usersSelectList = this.GetDropdownUsers();
                    this.ViewData[GlobalConstants.Users] = usersSelectList;
                }

                return this.View(projectUpdateInputModel);
            }
            catch (Exception ex)
            {
                this.ViewData[ValuesConstants.InvalidArgument] = ex.Message;

                return this.RedirectToAction(actionName: nameof(this.Details), routeValues: new { id = id });
            }
        }

        [HttpPost]
        [ProjectLeaderFilter]
        public async Task<ActionResult> Update(ProjectUpdateInputModel projectUpdateInputModel, string leaderId)
        {
            try
            {
                if (projectUpdateInputModel == null)
                {
                    this.ViewData[ValuesConstants.InvalidArgument] = string.Format(
                        format: MessagesConstants.NullOrEmptyArgument,
                        arg0: nameof(projectUpdateInputModel));

                    return this.View();
                }

                if (!this.ModelState.IsValid)
                {
                    if (this.User.IsInRole(GlobalConstants.AdministratorRoleName))
                    {
                        var usersSelectList = this.GetDropdownUsers();
                        this.ViewData[GlobalConstants.Users] = usersSelectList;
                    }

                    return this.View(projectUpdateInputModel);
                }

                var serviceModel = projectUpdateInputModel.To<ProjectServiceModel>();
                var updatedProjectServiceModel = await this.projectService.UpdateAsync(serviceModel);

                return this.RedirectToAction(
                    actionName: nameof(this.Details),
                    routeValues: new { id = updatedProjectServiceModel.Id });
            }
            catch (Exception ex)
            {
                this.ViewData[ValuesConstants.InvalidArgument] = ex.Message;
                if (this.User.IsInRole(GlobalConstants.AdministratorRoleName))
                {
                    var usersSelectList = this.GetDropdownUsers();
                    this.ViewData[GlobalConstants.Users] = usersSelectList;
                }

                return this.View(projectUpdateInputModel);
            }
        }

        [HttpGet]
        [ProjectLeaderFilter]
        public async Task<ActionResult> AddLabel(string projectId, string leaderId)
        {
            var projectLabelInputModel = new ProjectLabelInputModel()
            {
                ProjectId = projectId,
            };

            var labelsSelectList = this.GetDropDownLabels();
            this.ViewData[GlobalConstants.Labels] = labelsSelectList;
            this.ViewData[GlobalConstants.LeaderId] = leaderId;

            return this.View(projectLabelInputModel);
        }

        [HttpPost]
        [ProjectLeaderFilter]
        public async Task<ActionResult> AddLabel(ProjectLabelInputModel projectLabelInputModel, string leaderId)
        {
            if (projectLabelInputModel == null)
            {
                this.ViewData[ValuesConstants.InvalidArgument] = string.Format(
                        format: MessagesConstants.NullOrEmptyArgument,
                        arg0: nameof(projectLabelInputModel));

                var labelsSelectList = this.GetDropDownLabels();
                this.ViewData[GlobalConstants.Labels] = labelsSelectList;
                this.ViewData[GlobalConstants.LeaderId] = leaderId;

                return this.View();
            }

            if (!this.ModelState.IsValid)
            {
                var labelsSelectList = this.GetDropDownLabels();
                this.ViewData[GlobalConstants.Labels] = labelsSelectList;
                this.ViewData[GlobalConstants.LeaderId] = leaderId;

                return this.View(projectLabelInputModel);
            }

            var projectLabelServiceModel = new ProjectLabelServiceModel()
            {
                ProjectId = projectLabelInputModel.ProjectId,
                LabelId = projectLabelInputModel.LabelId,
            };

            //var projectLabelServiceModel = projectLabelInputModel.To<ProjectLabelServiceModel>();
            var projectServiceModel = await this.projectService.ByIdAsync(projectLabelInputModel.ProjectId);
            var labelServiceModel = await this.labelService.ByIdAsync(projectLabelInputModel.LabelId);
            projectLabelServiceModel.Project = projectServiceModel;
            projectLabelServiceModel.Label = labelServiceModel;

            var projectLabelServiceModelResult = await this.projectService.AddLabelAsync(projectLabelServiceModel);

            return this.RedirectToAction(
                actionName: nameof(this.Details),
                routeValues: new { id = projectLabelServiceModelResult.ProjectId });
        }


        //private async Task<IList<LabelServiceModel>> GetLabels(ICollection<string> labelIds)
        //{
        //    IList<LabelServiceModel> labels = new List<LabelServiceModel>();

        //    foreach (var id in labelIds)
        //    {
        //        var label = await this.labelService.ByIdAsync(id);
        //        if (label != null)
        //        {
        //            labels.Add(label);
        //        }
        //    }

        //    return labels;
        //}
    }
}