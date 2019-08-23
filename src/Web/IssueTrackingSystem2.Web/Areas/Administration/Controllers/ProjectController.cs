﻿namespace IssueTrackingSystem2.Web.Areas.Administration.Controllers
{
    using IssueTrackingSystem2.Common;
    using IssueTrackingSystem2.Services.Data.ApplicationUsers;
    using IssueTrackingSystem2.Services.Data.Project;
    using IssueTrackingSystem2.Services.Models;
    using IssueTrackingSystem2.Web.Infrastructure.Constants;
    using IssueTrackingSystem2.Web.InputModels.Project;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ProjectController : AdministrationController
    {
        private readonly IProjectService projectService;

        public ProjectController(IProjectService projectService, IApplicationUserService applicationUserService)
            : base(applicationUserService)
        {
            this.projectService = projectService;
        }

        // GET: Project/Create
        [HttpGet]
        public ActionResult Create()
        {
            var usersSelectList = this.GetDropdownUsers();
            this.ViewData[GlobalConstants.Users] = usersSelectList;

            return this.View();
        }

        // POST: Project/Create
        [HttpPost]
        public async Task<ActionResult> Create(ProjectCreateInputModel inputModel)
        {
            //try
            //{

            if (inputModel == null)
            {
                this.ViewData[ValuesConstants.InvalidArgument] = string.Format(
                    format: MessagesConstants.NullOrEmptyArgument,
                    arg0: nameof(inputModel));

                return this.View();
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            //var projectServiceModel = inputModel.To<ProjectServiceModel>();
            var projectServiceModel = new ProjectServiceModel()
            {
                Name = inputModel.Name,
                Description = inputModel.Description,
                LeaderId = inputModel.LeaderId,
            };

            projectServiceModel.ProjectKey = this.GenerateProjectKey(inputModel);
            projectServiceModel.Priorities = this.GeneratePriorities(inputModel);

            var projectServiceModelResult = await this.projectService.CreateAsync(projectServiceModel);

            return this.RedirectToRoute(
                routeName: ValuesConstants.DefaultRouteName,
                routeValues: new
                {
                    controller = ValuesConstants.ProjectControllerName,
                    action = ValuesConstants.DetailsActionName,
                    id = projectServiceModelResult.Id,
                });
            //}
            //catch
            //{
            //    // TODO: Implement Exception logging in file in dropbox or some other file datastore
            //    return this.View(inputModel);
            //}
        }

        private string GenerateProjectKey(ProjectCreateInputModel inputModel)
        {
            var projectNameParts = inputModel.Name.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var projectKey = new System.Text.StringBuilder();
            foreach (var projectNamePart in projectNameParts)
            {
                projectKey.Append(projectNamePart[0]);
            }

            return projectKey.ToString();
        }

        private IList<PriorityServiceModel> GeneratePriorities(ProjectCreateInputModel inputModel)
        {
            var priorityNames = inputModel.Priorities.Split(
                new char[] { ',', ';', ' ' },
                StringSplitOptions.RemoveEmptyEntries);

            IList<PriorityServiceModel> priorities = new List<PriorityServiceModel>();
            foreach (var priorityName in priorityNames)
            {
                priorities.Add(
                    new PriorityServiceModel()
                    {
                        Name = priorityName,
                    });
            }

            return priorities;
        }
    }
}