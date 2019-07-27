namespace IssueTrackingSystem2.Data.Models
{
    using IssueTrackingSystem2.Data.Common.Models;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Project : BaseDeletableModel<string>
    {
        public Project()
        {
            // TODO: Register Many to Many relationships in ApplicationDbContext
            this.Labels = new List<Label>();
            this.Milestones = new List<Milestone>();
            this.Priorities = new List<Priority>();
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

        [Required]
        public virtual ICollection<Milestone> Milestones { get; set; }

        [Required]
        public virtual ICollection<Priority> Priorities { get; set; }

        public virtual ICollection<Label> Labels { get; set; }
    }
}
