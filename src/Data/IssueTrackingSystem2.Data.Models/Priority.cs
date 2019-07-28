namespace IssueTrackingSystem2.Data.Models
{
    using IssueTrackingSystem2.Data.Common.Models;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Priority : BaseDeletableModel<int>
    {
        [Required]
        public string Name { get; set; }

        public string ProjectId { get; set; }

        [Required]
        public Project Project { get; set; }
    }
}