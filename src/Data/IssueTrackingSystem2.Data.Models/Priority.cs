namespace IssueTrackingSystem2.Data.Models
{
    using IssueTrackingSystem2.Data.Common.Models;

    public class Priority : BaseDeletableModel<int>
    {
        public string Name { get; set; }
    }
}