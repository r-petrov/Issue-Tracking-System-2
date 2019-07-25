namespace IssueTrackingSystem2.Data.Models
{
    using IssueTrackingSystem2.Data.Common.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Milestone : BaseDeletableModel<string>
    {
        public Milestone()
        {
            // TODO: Check List or HashSet is more appropriate
            this.Issues = new List<Issue>();
        }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime ScheduledStartDate { get; set; }

        public DateTime ScheduledCompletionDate { get; set; }

        public DateTime ActualStartDate { get; set; }

        public DateTime ActualCompletionDate { get; set; }

        public int StatusId { get; set; }

        [Required]
        public Status Status { get; set; }

        public string ProjectId { get; set; }

        [Required]
        public Project Project { get; set; }

        public virtual ICollection<Issue> Issues { get; set; }
    }
}
