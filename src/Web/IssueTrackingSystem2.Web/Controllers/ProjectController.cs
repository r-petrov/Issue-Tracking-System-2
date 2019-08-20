namespace IssueTrackingSystem2.Web.Controllers
{
    using IssueTrackingSystem2.Services.Data.ApplicationUsers;
    using IssueTrackingSystem2.Services.Data.Projects;
    using IssueTrackingSystem2.Services.Mapping;
    using IssueTrackingSystem2.Services.Models;
    using IssueTrackingSystem2.Web.Infrastructure.Constants;
    using IssueTrackingSystem2.Web.Infrastructure.Filters;
    using IssueTrackingSystem2.Web.InputModels.Project;
    using IssueTrackingSystem2.Web.ViewModels.Project;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class ProjectController : BaseController
    {
        private readonly IProjectService projectService;

        public ProjectController(IProjectService projectService, IApplicationUserService applicationUserService)
            : base(applicationUserService)
        {
            this.projectService = projectService;
        }

        // GET: Project/Details/5
        [HttpGet]
        public async Task<ActionResult> Details(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return this.BadRequest(string.Format(format: MessagesConstants.NullOrEmptyArgument, arg0: nameof(id)));
            }

            var project = await this.projectService.GetByIdAsync(id);
            var projectViewModel = project.To<ProjectDetailsViewModel>();

            return this.View(projectViewModel);
        }

        [HttpGet]
        [ProjectLeaderFilter]
        public async Task<ActionResult> Update(string id, string leaderId = null)
        {
            if (string.IsNullOrEmpty(id))
            {
                return this.BadRequest(string.Format(
                    format: MessagesConstants.NullOrEmptyArgument, 
                    arg0: nameof(id)));
            }

            var project = await this.projectService.GetByIdAsync(id);
            var projectEditInputModel = project.To<ProjectUpdateInputModel>();

            var usersSelectList = this.GetDropdownUsers();
            this.ViewData["Users"] = usersSelectList;

            return this.View(projectEditInputModel);
        }

        [HttpPost]
        [ProjectLeaderFilter]
        public async Task<ActionResult> Update(ProjectUpdateInputModel inputModel, string leaderId = null)
        {
            if (inputModel == null)
            {
                return this.BadRequest(string.Format(format: MessagesConstants.NullOrEmptyArgument, arg0: nameof(inputModel)));
            }

            if (!this.ModelState.IsValid)
            {
                var usersSelectList = this.GetDropdownUsers();
                this.ViewData["Users"] = usersSelectList;

                return this.View(inputModel);
            }

            var serviceModel = inputModel.To<ProjectServiceModel>();
            var updatedProjectServiceModel = this.projectService.Update(serviceModel);
            var updatedInputModel = updatedProjectServiceModel.To<ProjectUpdateInputModel>();

            return this.RedirectToAction(
                controllerName: "Project",
                actionName: nameof(this.Details),
                routeValues: new { id = updatedInputModel.Id });
        }
    }
}