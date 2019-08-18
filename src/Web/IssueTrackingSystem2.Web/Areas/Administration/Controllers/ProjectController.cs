namespace IssueTrackingSystem2.Web.Areas.Administration.Controllers
{
    using IssueTrackingSystem2.Services.Data.ApplicationUsers;
    using IssueTrackingSystem2.Services.Data.Projects;
    using IssueTrackingSystem2.Services.Mapping;
    using IssueTrackingSystem2.Services.Models;
    using IssueTrackingSystem2.Web.InputModels.Project;
    using IssueTrackingSystem2.Web.ViewModels;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ProjectController : AdministrationController
    {
        private readonly IProjectService projectService;
        private readonly IApplicationUserService applicationUserService;

        public ProjectController(IProjectService projectService, IApplicationUserService applicationUserService)
        {
            this.projectService = projectService;
            this.applicationUserService = applicationUserService;
        }

        // GET: Project/Create
        [HttpGet]
        public ActionResult Create()
        {
            var users = this.applicationUserService.GetAllApplicationUsers();
            var usersSelectList = new SelectList(
                items: users,
                dataValueField: nameof(ApplicationUserServiceModel.Id),
                dataTextField: nameof(ApplicationUserServiceModel.UserName));

            this.ViewData["Users"] = usersSelectList;

            return this.View();
        }

        // POST: Project/Create
        [HttpPost]
        public async Task<ActionResult> Create(ProjectCreateInputModel inputModel)
        {
            try
            {
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
                    routeName: "default",
                    routeValues: new
                    {
                        controller = "Project",
                        action = "Details",
                        id = projectServiceModelResult.Id,
                    });
            }
            catch
            {
                return this.View(inputModel);
            }
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