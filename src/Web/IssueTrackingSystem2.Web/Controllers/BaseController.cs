namespace IssueTrackingSystem2.Web.Controllers
{
    using IssueTrackingSystem2.Services.Data.ApplicationUsers;
    using IssueTrackingSystem2.Services.Data.Label;
    using IssueTrackingSystem2.Services.Data.Project;
    using IssueTrackingSystem2.Services.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;

    [Authorize]
    public class BaseController : Controller
    {
        private readonly IApplicationUserService applicationUserService;
        private readonly ILabelService labelService;

        public BaseController()
        {
        }

        public BaseController(IProjectService projectService)
        {
            this.ProjectService = projectService;
        }

        public BaseController(IApplicationUserService applicationUserService)
        {
            this.applicationUserService = applicationUserService;
        }

        public BaseController(IApplicationUserService applicationUserService, ILabelService labelService)
        {
            this.applicationUserService = applicationUserService;
            this.labelService = labelService;
        }

        protected IProjectService ProjectService { get; }

        protected SelectList GetDropdownUsers()
        {
            var users = this.applicationUserService.GetAllApplicationUsers();
            var usersSelectList = new SelectList(
                items: users,
                dataValueField: nameof(ApplicationUserServiceModel.Id),
                dataTextField: nameof(ApplicationUserServiceModel.UserName));

            return usersSelectList;
        }

        protected SelectList GetDropDownLabels()
        {
            var labels = this.labelService.All();
            var labelsSelectList = new SelectList(
                items: labels,
                dataValueField: nameof(LabelServiceModel.Id),
                dataTextField: nameof(LabelServiceModel.Title));

            return labelsSelectList;
        }
    }
}
