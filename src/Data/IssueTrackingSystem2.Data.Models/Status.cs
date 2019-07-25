namespace IssueTrackingSystem2.Data.Models
{
    using IssueTrackingSystem2.Data.Common.Models;
    using System.ComponentModel.DataAnnotations;

    public class Status : BaseDeletableModel<int>
    {
        [Required]
        public string Name { get; set; }
    }
}