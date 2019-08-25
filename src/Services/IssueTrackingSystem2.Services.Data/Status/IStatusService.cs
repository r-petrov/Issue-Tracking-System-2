namespace IssueTrackingSystem2.Services.Data.Status
{
    using IssueTrackingSystem2.Services.Models;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System;
    using System.Threading.Tasks;

    public interface IStatusService
    {
        SelectList GetAvailableMilestoneStatuses(string currentStatus);
    }
}
