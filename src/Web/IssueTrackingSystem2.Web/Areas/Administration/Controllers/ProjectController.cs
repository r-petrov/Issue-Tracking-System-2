namespace IssueTrackingSystem2.Web.Areas.Administration.Controllers
{
    using IssueTrackingSystem2.Services.Data.ApplicationUsers;
    using IssueTrackingSystem2.Services.Data.Projects;
    using IssueTrackingSystem2.Services.Mapping;
    using IssueTrackingSystem2.Services.Models;
    using IssueTrackingSystem2.Web.InputModels.Project;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
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
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProjectCreateInputModel inputModel)
        {
            try
            {
                var projectServiceModel = inputModel.To<ProjectServiceModel>();
                var result = await this.projectService.CreateAsync(projectServiceModel);

                return this.RedirectToRoute("~/Project/Details");
            }
            catch
            {
                return View(inputModel);
            }
        }
    }
}