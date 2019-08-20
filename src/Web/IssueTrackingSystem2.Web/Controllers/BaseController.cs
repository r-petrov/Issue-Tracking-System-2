namespace IssueTrackingSystem2.Web.Controllers
{
    using IssueTrackingSystem2.Services.Data.ApplicationUsers;
    using IssueTrackingSystem2.Services.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;

    [Authorize]
    public class BaseController : Controller
    {
        private readonly IApplicationUserService applicationUserService;

        public BaseController()
        {
        }

        public BaseController(IApplicationUserService applicationUserService)
        {
            this.applicationUserService = applicationUserService;
        }

        protected SelectList GetDropdownUsers()
        {
            var users = this.applicationUserService.GetAllApplicationUsers();
            var usersSelectList = new SelectList(
                items: users,
                dataValueField: nameof(ApplicationUserServiceModel.Id),
                dataTextField: nameof(ApplicationUserServiceModel.UserName));

            return usersSelectList;
        }
    }
}
