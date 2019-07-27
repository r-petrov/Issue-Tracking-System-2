namespace IssueTrackingSystem2.Data.Models
{
    public class IssueLabel
    {
        public string IssueId { get; set; }

        public Issue Issue { get; set; }

        public string LabelId { get; set; }

        public Label Label { get; set; }
    }
}
