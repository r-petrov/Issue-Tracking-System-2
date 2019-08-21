namespace IssueTrackingSystem2.Web.Areas.Administration.Controllers
{
    using IssueTrackingSystem2.Services.Data;
    using IssueTrackingSystem2.Web.Areas.Administration.ViewModels.Dashboard;
    using Microsoft.AspNetCore.Mvc;

    public class DashboardController : AdministrationController
    {
        private readonly ISettingsService settingsService;

        public DashboardController(ISettingsService settingsService)
        {
            this.settingsService = settingsService;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel { SettingsCount = this.settingsService.GetCount(), };
            return this.View(viewModel);
        }
    }
}
