namespace IssueTrackingSystem2.Data.Models
{
    using IssueTrackingSystem2.Common;
    using IssueTrackingSystem2.Common.Infrastructure.Constants;
    using IssueTrackingSystem2.Common.Infrastructure.Extensions;
    using IssueTrackingSystem2.Data.Common.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Milestone : BaseDeletableModel<string>, IValidatableObject
    {
        public Milestone()
        {
            this.Issues = new List<Issue>();
        }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime CompletionDate { get; set; }

        //public DateTime? ActualStartDate { get; set; }

        //public DateTime? ActualCompletionDate { get; set; }

        public int StatusId { get; set; }

        [Required]
        public virtual Status Status { get; set; }

        public string ProjectId { get; set; }

        [Required]
        public virtual Project Project { get; set; }

        public virtual ICollection<Issue> Issues { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.StartDate < DateTime.UtcNow)
            {
                yield return new ValidationResult(string.Format(
                    format: MessagesConstants.DateTimeEarlierThanNow.SplitStringByCapitalLetters(),
                    arg0: nameof(this.StartDate)));
            }

            if (this.StartDate < DateTime.UtcNow)
            {
                yield return new ValidationResult(string.Format(
                    format: MessagesConstants.DateTimeEarlierThanNow.SplitStringByCapitalLetters(),
                    arg0: nameof(this.CompletionDate)));
            }

            if (this.StartDate > this.CompletionDate)
            {
                yield return new ValidationResult(string.Format(
                    format: MessagesConstants.StartDateLaterThanEndDate.SplitStringByCapitalLetters(),
                    arg0: nameof(this.StartDate),
                    arg1: nameof(this.CompletionDate)));
            }
        }
    }
}
