namespace IssueTrackingSystem2.Web.Controllers
{
    using IssueTrackingSystem2.Services.Data.Projects;
    using IssueTrackingSystem2.Services.Mapping;
    using IssueTrackingSystem2.Web.ViewModels.Project;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;

    [Authorize]
    public class HomeController : BaseController
    {
        private readonly IProjectService projectService;

        public HomeController(IProjectService projectService)
        {
            this.projectService = projectService;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index()
        {
            return this.View();
        }

        [HttpGet]
        public IActionResult Privacy()
        {
            return this.View();
        }

        [HttpGet]
        public IActionResult Dashboard()
        {
            // TODO: Add Pagination
            // TODO: Add filters, i.e. Project name
            var projects = this.projectService
                .GetAll()
                .To<DashboardProjectViewModel>()
                .ToList()
                .OrderByDescending(project => project.CreatedOn);

            return this.View(projects);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => this.View();
    }
}
