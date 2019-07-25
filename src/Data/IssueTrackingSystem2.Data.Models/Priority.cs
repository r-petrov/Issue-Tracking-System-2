namespace IssueTrackingSystem2.Data.Models
{
    using IssueTrackingSystem2.Data.Common.Models;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Priority : BaseDeletableModel<int>
    {
        public Priority()
        {
            // TODO: Check List or HashSet is more appropriate
            this.Projects = new List<Project>();
            this.Issues = new List<Issue>();
        }

        [Required]
        public string Name { get; set; }

        public ICollection<Project> Projects { get; set; }

        public ICollection<Issue> Issues { get; set; }
    }
}