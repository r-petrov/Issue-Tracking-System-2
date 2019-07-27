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
            // TODO: Register Many to Many relationships in ApplicationDbContext
            this.Labels = new List<IssueLabel>();
            this.Comments = new List<Comment>();
        }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public string IssueKey { get; set; }

        public int StatusId { get; set; }

        [Required]
        public Status Status { get; set; }

        public DateTime DueDate { get; set; }

        public int PriorityId { get; set; }

        [Required]
        public Priority Priority { get; set; }

        public string MilestoneId { get; set; }

        [Required]
        public Milestone Milestone { get; set; }

        public string AssigneeId { get; set; }

        [Required]
        public ApplicationUser Assignee { get; set; }

        public virtual ICollection<IssueLabel> Labels { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}