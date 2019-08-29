namespace IssueTrackingSystem2.Data.Models
{
    using IssueTrackingSystem2.Data.Common.Models;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Comment : BaseDeletableModel<string>
    {
        [Required]
        public string Text { get; set; }

        public string IssueId { get; set; }

        public virtual Issue Issue { get; set; }

        public string CreatorId { get; set; }

        [ForeignKey(nameof(CreatorId))]
        public virtual ApplicationUser CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}