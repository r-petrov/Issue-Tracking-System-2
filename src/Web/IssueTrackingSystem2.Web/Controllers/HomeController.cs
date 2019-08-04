namespace IssueTrackingSystem2.Web.Controllers
{
    using IssueTrackingSystem2.Services.Data.Projects;
    using IssueTrackingSystem2.Services.Mapping;
    using IssueTrackingSystem2.Web.ViewModels.Projects;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;

    public class HomeController : BaseController
    {
        private readonly IProjectService projectService;

        public HomeController(IProjectService projectService)
        {
            this.projectService = projectService;
        }

        public IActionResult Index()
        {
            return this.View();
        }


        public IActionResult Privacy()
        {
            return this.View();
        }

        public IActionResult Dashboard()
        {
            var projects = this.projectService.GetAll().To<DashboardProjectViewModel>().ToList();

            return this.View(projects);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => this.View();
    }
}
