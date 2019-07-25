namespace IssueTrackingSystem2.Data.Models
{
    using IssueTrackingSystem2.Data.Common.Models;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Project : BaseDeletableModel<string>
    {
        public Project()
        {
            // TODO: Check List or HashSet is more appropriate
            // TODO: Register Many to Many relationships in ApplicationDbContext
            this.Labels = new List<Label>();

            //this.Labels = new List<ProjectLabel>();

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

        public virtual ICollection<Milestone> Milestones { get; set; }

        public int PriorityId { get; set; }

        [Required]
        public Priority Priority { get; set; }

        //public virtual ICollection<ProjectLabel> Labels { get; set; }

        public virtual ICollection<Label> Labels { get; set; }
    }
}
