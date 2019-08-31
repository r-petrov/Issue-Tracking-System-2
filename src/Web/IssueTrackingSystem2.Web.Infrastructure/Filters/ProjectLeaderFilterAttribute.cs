namespace IssueTrackingSystem2.Web.Infrastructure.Filters
{
    using IssueTrackingSystem2.Common.Infrastructure.Constants;
    using Microsoft.AspNetCore.Mvc.Filters;
    using System;
    using System.Security.Claims;

    public class ProjectLeaderFilterAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.User.IsInRole(GlobalConstants.AdministratorRoleName))
            {
                return;
            }

            var currentUserId = context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var actionArguments = context.ActionArguments;
            if (actionArguments.ContainsKey(GlobalConstants.LeaderId))
            {
                var leaderIdArgument = (string)actionArguments[GlobalConstants.LeaderId];
                if (currentUserId == leaderIdArgument)
                {
                    return;
                }
            }

            throw new Exception(string.Format(
                    format: MessagesConstants.UnauthotizedForProjectLeaderAction,
                    arg0: context.ActionDescriptor.DisplayName));
        }

        //public void OnActionExecuting(ActionExecutingContext context)
        //{
        //    var actionArguments = context.ActionArguments;
        //    if (!actionArguments.ContainsKey(GlobalConstants.LeaderId))
        //    {
        //        throw new Exception(string.Format(
        //            format: MessagesConstants.NullOrEmptyArgument,
        //            arg0: GlobalConstants.LeaderId));
        //    }

        //    var leaderIdArgument = (string)actionArguments[GlobalConstants.LeaderId];
        //    var currentUserId = context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        //    if (!context.HttpContext.User.IsInRole(GlobalConstants.AdministratorRoleName)
        //            && currentUserId != leaderIdArgument)
        //    {
        //        throw new Exception(string.Format(
        //            format: MessagesConstants.UnauthotizedForProjectLeaderAction,
        //            arg0: context.ActionDescriptor.DisplayName));
        //    }
        //}
    }
}
