namespace IssueTrackingSystem2.Data.Models
{
    using IssueTrackingSystem2.Data.Common.Models;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Project : BaseDeletableModel<string>
    {
        public Project()
        {
            this.Labels = new List<Label>();
            this.Milestones = new List<Milestone>();
        }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string ProjectKey { get; set; }

        public string LeaderId { get; set; }

        [Required]
        public ApplicationUser Leader { get; set; }

        public Priority Priority { get; set; }

        public virtual ICollection<Milestone> Milestones { get; set; }

        public virtual ICollection<Label> Labels { get; set; }
    }
}
