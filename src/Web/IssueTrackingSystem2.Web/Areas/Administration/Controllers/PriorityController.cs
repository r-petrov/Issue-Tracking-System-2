namespace IssueTrackingSystem2.Web.Areas.Administration.Controllers
{
    using IssueTrackingSystem2.Services.Data.Priority;
    using IssueTrackingSystem2.Services.Data.Project;
    using IssueTrackingSystem2.Services.Mapping;
    using IssueTrackingSystem2.Services.Models;
    using IssueTrackingSystem2.Web.Infrastructure.Constants;
    using IssueTrackingSystem2.Web.InputModels.Priority;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class PriorityController : AdministrationController
    {
        private readonly IPriorityService priorityService;

        public PriorityController(IPriorityService priorityService, IProjectService projectService)
            : base(projectService)
        {
            this.priorityService = priorityService;
        }

        // GET: Milestone/Create
        [HttpGet]
        public async Task<ActionResult> Create(string projectId)
        {
            var priorityInputModel = new PriorityInputModel()
            {
                ProjectId = projectId,
            };

            return this.View(priorityInputModel);
        }

        // POST: Milestone/Create
        [HttpPost]
        public async Task<ActionResult> Create(PriorityInputModel inputModel)
        {
            //try
            //{
                var priorityServiceModel = inputModel.To<PriorityServiceModel>();
                priorityServiceModel.Project = await this.ProjectService.ByIdAsync(inputModel.ProjectId);

                var priorityServiceModelResult = await this.priorityService.CreateAsync(priorityServiceModel);

                return this.RedirectToRoute(
                    routeName: ValuesConstants.DefaultRouteName,
                    routeValues: new
                    {
                        controller = ValuesConstants.ProjectControllerName,
                        action = ValuesConstants.DetailsActionName,
                        id = priorityServiceModelResult.ProjectId,
                    });
            //}
            //catch
            //{
            //    // TODO: Implement Exception logging in file in dropbox or some other file datastore
            //    return View();
            //}
        }
    }
}
