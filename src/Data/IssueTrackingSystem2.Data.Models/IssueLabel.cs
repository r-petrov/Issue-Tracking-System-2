namespace IssueTrackingSystem2.Data.Models
{
    public class IssueLabel
    {
        public string IssueId { get; set; }

        public virtual Issue Issue { get; set; }

        public string LabelId { get; set; }

        public virtual Label Label { get; set; }
    }
}
