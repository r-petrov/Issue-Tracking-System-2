namespace IssueTrackingSystem2.Data.Models
{
    using IssueTrackingSystem2.Data.Common.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Label : BaseDeletableModel<string>
    {
        public Label()
        {
            this.Issues = new List<IssueLabel>();
        }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public string Color { get; set; }

        public string CreatorId { get; set; }

        [Required]
        [ForeignKey(nameof(CreatorId))]
        public ApplicationUser CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        public string ProjectId { get; set; }

        [Required]
        public virtual Project Project { get; set; }

        public virtual ICollection<IssueLabel> Issues { get; set; }
    }
}