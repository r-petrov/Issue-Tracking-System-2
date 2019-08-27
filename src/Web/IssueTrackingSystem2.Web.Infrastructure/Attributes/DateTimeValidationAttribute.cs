namespace IssueTrackingSystem2.Web.Infrastructure.Attributes
{
    using IssueTrackingSystem2.Common.Infrastructure.Constants;
    using IssueTrackingSystem2.Web.Infrastructure.Constants;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class DateTimeValidationAttribute : ValidationAttribute
    {
        private readonly string valueName;

        public DateTimeValidationAttribute(string valueName)
        {
            this.valueName = valueName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult(string.Format(
                    format: MessagesConstants.NullOrEmptyArgument,
                    arg0: this.valueName));
            }

            var dateTimeValue = (DateTime)value;
            if (dateTimeValue < DateTime.UtcNow)
            {
                return new ValidationResult(string.Format(
                    format: MessagesConstants.DateTimeEarlierThanNow,
                    arg0: this.valueName));
            }

            return ValidationResult.Success;
        }
    }
}
