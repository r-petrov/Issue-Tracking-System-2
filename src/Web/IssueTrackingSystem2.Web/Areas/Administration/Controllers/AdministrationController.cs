namespace IssueTrackingSystem2.Web.Areas.Administration.Controllers
{
    using IssueTrackingSystem2.Common;
    using IssueTrackingSystem2.Services.Data.ApplicationUsers;
    using IssueTrackingSystem2.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area(nameof(Administration))]
    public class AdministrationController : BaseController
    {
        public AdministrationController()
        {
        }

        public AdministrationController(IApplicationUserService applicationUserService) : base(applicationUserService)
        {
        }
    }
}
