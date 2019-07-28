namespace IssueTrackingSystem2.Data.Models
{
    public class ProjectLabel
    {
        public string ProjectId { get; set; }

        public Project Project { get; set; }

        public string LabelId { get; set; }

        public Label Label { get; set; }
    }
}
