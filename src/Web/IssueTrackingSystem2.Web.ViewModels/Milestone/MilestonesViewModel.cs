namespace IssueTrackingSystem2.Web.ViewModels.Milestone
{
    using IssueTrackingSystem2.Web.ViewModels.Project;
    using System.Collections.Generic;

    public class MilestonesViewModel
    {
        public ICollection<MilestoneListViewModel> Milestones { get; set; }

        public ProjectConciseViewModel Project { get; set; }
    }
}
