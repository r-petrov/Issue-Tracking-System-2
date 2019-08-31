namespace IssueTrackingSystem2.Web.ViewModels.Label
{
    using IssueTrackingSystem2.Services.Mapping;
    using IssueTrackingSystem2.Services.Models;
    using IssueTrackingSystem2.Web.ViewModels.Issue;
    using IssueTrackingSystem2.Web.ViewModels.Project;
    using System.Collections.Generic;

    public class LabelListViewModel : IMapFrom<LabelServiceModel>
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Color { get; set; }

        public ApplicationUserViewModel CreatedBy { get; set; }

        public virtual ICollection<ProjectConciseViewModel> Projects { get; set; }

        public virtual ICollection<IssueConciseViewModel> Issues { get; set; }
    }
}
