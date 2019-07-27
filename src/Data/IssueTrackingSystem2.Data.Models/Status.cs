namespace IssueTrackingSystem2.Data.Models
{
    using IssueTrackingSystem2.Data.Common.Models;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Status : BaseDeletableModel<int>
    {
        public Status()
        {
            this.Milestones = new List<Milestone>();
            this.Issues = new List<Issue>();
        }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Type { get; set; }

        public virtual ICollection<Milestone> Milestones { get; set; }

        public virtual ICollection<Issue> Issues { get; set; }
    }
}