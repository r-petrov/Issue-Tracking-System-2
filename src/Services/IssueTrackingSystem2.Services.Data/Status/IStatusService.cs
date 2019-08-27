namespace IssueTrackingSystem2.Services.Data.Status
{
    using Microsoft.AspNetCore.Mvc.Rendering;

    public interface IStatusService
    {
        SelectList GetAvailableMilestoneStatuses(string currentStatus);

        SelectList GetAvailableIssueStatuses(string currentStatus);
    }
}
