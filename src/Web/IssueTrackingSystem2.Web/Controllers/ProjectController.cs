namespace IssueTrackingSystem2.Web.Controllers
{
    using IssueTrackingSystem2.Common;
    using IssueTrackingSystem2.Common.Infrastructure.Constants;
    using IssueTrackingSystem2.Services.Data.ApplicationUsers;
    using IssueTrackingSystem2.Services.Data.Project;
    using IssueTrackingSystem2.Services.Mapping;
    using IssueTrackingSystem2.Services.Models;
    using IssueTrackingSystem2.Web.Infrastructure.Constants;
    using IssueTrackingSystem2.Web.Infrastructure.Filters;
    using IssueTrackingSystem2.Web.InputModels.Project;
    using IssueTrackingSystem2.Web.ViewModels.Project;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class ProjectController : BaseController
    {
        private readonly IProjectService projectService;

        public ProjectController(IProjectService projectService, IApplicationUserService applicationUserService)
                : base(applicationUserService)
        {
            this.projectService = projectService;
        }

        public ActionResult List()
        {
            // TODO: Add Pagination
            // TODO: Add filters, i.e. Project name
            var projectListServiceModels = this.projectService.All().ToList();
            var projectListViewModels = projectListServiceModels
                    .To<ProjectListViewModel>()
                    .OrderByDescending(project => project.CreatedOn);

            return this.View(projectListViewModels);
        }

        // GET: Project/Details/5
        [HttpGet]
        public async Task<ActionResult> Details(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return this.BadRequest(string.Format(format: MessagesConstants.NullOrEmptyArgument, arg0: nameof(id)));
            }

            var project = await this.projectService.ByIdAsync(id);
            var projectViewModel = project.To<ProjectDetailsViewModel>();

            return this.View(projectViewModel);
        }

        [HttpGet]
        [ProjectLeaderFilter]
        public async Task<ActionResult> Update(string id, string leaderId)
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

        [HttpPost]
        [ProjectLeaderFilter]
        public async Task<ActionResult> Update(ProjectUpdateInputModel projectUpdateInputModel, string leaderId)
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
    }
}