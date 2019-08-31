namespace IssueTrackingSystem2.Web.ViewModels.Label
{
    using IssueTrackingSystem2.Web.ViewModels.Issue;
    using IssueTrackingSystem2.Web.ViewModels.Project;
    using System.Collections.Generic;

    public class LabelsViewModel
    {
        public ICollection<LabelListViewModel> Labels { get; set; }

        public virtual ICollection<ProjectConciseViewModel> Projects { get; set; }

        public virtual ICollection<IssueConciseViewModel> Issues { get; set; }
    }
}
