namespace IssueTrackingSystem2.Web.Infrastructure.Attributes
{
    using IssueTrackingSystem2.Common.Infrastructure.Constants;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class EnumValidationAttribute : ValidationAttribute
    {
        private readonly Type enumType;

        public EnumValidationAttribute(Type enumType)
        {
            this.enumType = enumType;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (!Enum.IsDefined(enumType: this.enumType, value: (string)value))
            {
                return new ValidationResult(string.Format(
                    format: MessagesConstants.NotAmongTheValidValues,
                    arg0: value,
                    arg1: this.enumType.Name));
            }

            return ValidationResult.Success;
        }
    }
}
