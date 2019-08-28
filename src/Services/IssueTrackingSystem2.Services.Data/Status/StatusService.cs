namespace IssueTrackingSystem2.Services.Data.Status
{
    using IssueTrackingSystem2.Common.Enums;
    using IssueTrackingSystem2.Common.Infrastructure.Constants;
    using IssueTrackingSystem2.Common.Infrastructure.Extensions;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public class StatusService : IStatusService
    {
        public SelectList GetAvailableMilestoneStatuses(string currentStatus)
        {
            //if (!Enum.IsDefined(enumType: typeof(MilestoneStatuses), value: currentStatus))
            //{
            //    throw new Exception(string.Format(
            //        format: MessagesConstants.NotAmongTheValidValues,
            //        arg0: currentStatus,
            //        arg1: typeof(MilestoneStatuses).Name));
            //}

            //var methodName = $"GetAvailableMilestone{currentStatus}Statuses";
            //var methodInfo = this.GetType().GetMethod(
            //    name: methodName,
            //    bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic);

            var currentMethod = this.GetCurrentMethod(
                                    statusType: typeof(MilestoneStatuses),
                                    statusPrefix: GlobalConstants.Milestone,
                                    currentStatus: currentStatus);

            var availableStatuses = (SelectList)currentMethod.Invoke(obj: this, parameters: null);

            return availableStatuses;
        }

        private SelectList GetAvailableMilestoneNotStartedStatuses()
        {
            IList<string> availableStatuses = new List<string>();
            availableStatuses.AddRange(
                    MilestoneStatuses.NotStarted.ToString(),
                    MilestoneStatuses.Started.ToString());

            var statusesSelectList = new SelectList(items: availableStatuses);

            return statusesSelectList;
        }

        private SelectList GetAvailableMilestoneStartedStatuses()
        {
            IList<string> availableStatuses = new List<string>();
            availableStatuses.AddRange(
                    MilestoneStatuses.Started.ToString(),
                    MilestoneStatuses.Overdued.ToString(),
                    MilestoneStatuses.Completed.ToString());

            var statusesSelectList = new SelectList(items: availableStatuses);

            return statusesSelectList;
        }

        private SelectList GetAvailableMilestoneOverduedStatuses()
        {
            IList<string> availableStatuses = new List<string>();
            availableStatuses.AddRange(
                    MilestoneStatuses.Overdued.ToString(),
                    MilestoneStatuses.Completed.ToString());

            var statusesSelectList = new SelectList(items: availableStatuses);

            return statusesSelectList;
        }

        private SelectList GetAvailableMilestoneCompletedStatuses()
        {
            IList<string> availableStatuses = new List<string>();
            availableStatuses.Add(MilestoneStatuses.Completed.ToString());

            var statusesSelectList = new SelectList(items: availableStatuses);

            return statusesSelectList;
        }

        public SelectList GetAvailableIssueStatuses(string currentStatus)
        {
            var currentMethod = this.GetCurrentMethod(
                                   statusType: typeof(IssueStatuses),
                                   statusPrefix: GlobalConstants.Issue,
                                   currentStatus: currentStatus);

            var availableStatuses = (SelectList)currentMethod.Invoke(obj: this, parameters: null);

            return availableStatuses;
        }

        private SelectList GetAvailableIssueOpenStatuses()
        {
            IList<string> availableStatuses = new List<string>();
            availableStatuses.AddRange(
                    IssueStatuses.Open.ToString(),
                    IssueStatuses.InProgress.ToString(),
                    IssueStatuses.Resolved.ToString(),
                    IssueStatuses.Closed.ToString());

            var statusesSelectList = new SelectList(items: availableStatuses);

            return statusesSelectList;
        }

        private SelectList GetAvailableIssueInProgressStatuses()
        {
            IList<string> availableStatuses = new List<string>();
            availableStatuses.AddRange(
                    IssueStatuses.InProgress.ToString(),
                    IssueStatuses.Open.ToString(),
                    IssueStatuses.Resolved.ToString(),
                    IssueStatuses.Closed.ToString());

            var statusesSelectList = new SelectList(items: availableStatuses);

            return statusesSelectList;
        }

        private SelectList GetAvailableIssueResolvedStatuses()
        {
            IList<string> availableStatuses = new List<string>();
            availableStatuses.AddRange(
                    IssueStatuses.Resolved.ToString(),
                    IssueStatuses.Reopened.ToString(),
                    IssueStatuses.Closed.ToString());

            var statusesSelectList = new SelectList(items: availableStatuses);

            return statusesSelectList;
        }

        private SelectList GetAvailableIssueReopenedStatuses()
        {
            IList<string> availableStatuses = new List<string>();
            availableStatuses.AddRange(
                    IssueStatuses.Reopened.ToString(),
                    IssueStatuses.InProgress.ToString(),
                    IssueStatuses.Resolved.ToString(),
                    IssueStatuses.Closed.ToString());

            var statusesSelectList = new SelectList(items: availableStatuses);

            return statusesSelectList;
        }

        private SelectList GetAvailableIssueClosedStatuses()
        {
            IList<string> availableStatuses = new List<string>();
            availableStatuses.AddRange(
                    IssueStatuses.Closed.ToString(),
                    IssueStatuses.Reopened.ToString());

            var statusesSelectList = new SelectList(items: availableStatuses);

            return statusesSelectList;
        }

        private MethodInfo GetCurrentMethod(Type statusType, string statusPrefix, string currentStatus)
        {
            if (!Enum.IsDefined(enumType: statusType, value: currentStatus))
            {
                throw new Exception(string.Format(
                    format: MessagesConstants.NotAmongTheValidValues,
                    arg0: currentStatus,
                    arg1: statusType.Name));
            }

            var methodName = $"GetAvailable{statusPrefix}{currentStatus}Statuses";
            var methodInfo = this.GetType().GetMethod(
                name: methodName,
                bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic);

            return methodInfo;
        }
    }
}
