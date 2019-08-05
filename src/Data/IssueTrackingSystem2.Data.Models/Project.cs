namespace IssueTrackingSystem2.Data.Models
{
    using IssueTrackingSystem2.Data.Common.Models;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Project : BaseDeletableModel<string>
    {
        public Project()
        {
            this.Labels = new List<ProjectLabel>();
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
        public virtual ApplicationUser Leader { get; set; }

        public virtual ICollection<Milestone> Milestones { get; set; }

        [Required]
        public virtual ICollection<Priority> Priorities { get; set; }

        public virtual ICollection<ProjectLabel> Labels { get; set; }
    }
}
