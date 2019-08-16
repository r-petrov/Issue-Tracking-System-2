namespace IssueTrackingSystem2.Web.Infrastructure.ViewComponents
{
    using IssueTrackingSystem2.Services.Data.ApplicationUsers;
    using IssueTrackingSystem2.Services.Mapping;
    using IssueTrackingSystem2.Services.Models;
    using IssueTrackingSystem2.Web.ViewModels;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class UserListViewComponent : ViewComponent
    {
        private readonly IApplicationUserService applicationUserService;

        public UserListViewComponent(IApplicationUserService applicationUserService)
        {
            this.applicationUserService = applicationUserService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var usersServiceModel = this.applicationUserService.GetAllApplicationUsers();
            var usersViewModel = usersServiceModel.To<ApplicationUserViewModel>();

            //var usersSelectList = new SelectList(
            //    items: usersViewModel,
            //    dataValueField: nameof(ApplicationUserViewModel.Id),
            //    dataTextField: nameof(ApplicationUserViewModel.UserName));

            //return this.View(usersSelectList);

            return this.View(usersViewModel);
        }
    }
}
