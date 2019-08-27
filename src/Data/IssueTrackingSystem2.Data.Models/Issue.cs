namespace IssueTrackingSystem2.Data.Models
{
    using IssueTrackingSystem2.Data.Common.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Issue : BaseDeletableModel<string>
    {
        public Issue()
        {
            this.Labels = new List<IssueLabel>();
            this.Comments = new List<Comment>();
        }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string IssueKey { get; set; }

        public int StatusId { get; set; }

        [Required]
        public virtual Status Status { get; set; }

        [Required]
        public string Priority { get; set; }

        public DateTime DueDate { get; set; }

        public string MilestoneId { get; set; }

        [Required]
        public virtual Milestone Milestone { get; set; }

        public string AssigneeId { get; set; }

        [Required]
        public virtual ApplicationUser Assignee { get; set; }

        public virtual ICollection<IssueLabel> Labels { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}