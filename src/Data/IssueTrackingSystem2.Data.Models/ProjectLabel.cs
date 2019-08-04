namespace IssueTrackingSystem2.Data.Models
{
    public class ProjectLabel
    {
        public string ProjectId { get; set; }

        public virtual Project Project { get; set; }

        public string LabelId { get; set; }

        public virtual Label Label { get; set; }
    }
}
