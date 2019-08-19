namespace IssueTrackingSystem2.Web.Controllers
{
    using IssueTrackingSystem2.Services.Data.Projects;
    using IssueTrackingSystem2.Services.Mapping;
    using IssueTrackingSystem2.Web.ViewModels.Project;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class ProjectController : Controller
    {
        private readonly IProjectService projectService;

        public ProjectController(IProjectService projectService)
        {
            this.projectService = projectService;
        }
        
        // GET: Project/Details/5
        [HttpGet]
        public async Task<ActionResult> Details(string id)
        {
            var project = await this.projectService.GetByIdAsync(id);
            var projectViewModel = project.To<ProjectDetailsViewModel>();

            return this.View(projectViewModel);
        }
    }
}