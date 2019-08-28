namespace IssueTrackingSystem2.Web.Infrastructure.Filters
{
    using IssueTrackingSystem2.Common.Infrastructure.Constants;
    using Microsoft.AspNetCore.Mvc.Filters;
    using System;
    using System.Security.Claims;

    public class IssueAssigneeFilterAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var actionArguments = context.ActionArguments;
            if (!actionArguments.ContainsKey(GlobalConstants.LeaderId))
            {
                throw new Exception(string.Format(
                    format: MessagesConstants.NullOrEmptyArgument,
                    arg0: GlobalConstants.LeaderId));
            }

            if (!actionArguments.ContainsKey(GlobalConstants.AssigneeId))
            {
                throw new Exception(string.Format(
                    format: MessagesConstants.NullOrEmptyArgument,
                    arg0: GlobalConstants.AssigneeId));
            }

            var leaderIdArgument = (string)actionArguments[GlobalConstants.LeaderId];
            var assigneeIdArgument = (string)actionArguments[GlobalConstants.AssigneeId];
            var currentUserId = context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (!context.HttpContext.User.IsInRole(GlobalConstants.AdministratorRoleName)
                    && currentUserId != leaderIdArgument
                    && currentUserId != assigneeIdArgument)
            {
                throw new Exception(string.Format(
                    format: MessagesConstants.UnauthotizedForProjectLeaderAction,
                    arg0: context.ActionDescriptor.DisplayName));
            }
        }
    }
}
