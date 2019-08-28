namespace IssueTrackingSystem2.Web.InputModels.Milestone
{
    using AutoMapper;
    using IssueTrackingSystem2.Common.Enums;
    using IssueTrackingSystem2.Common.Infrastructure.Constants;
    using IssueTrackingSystem2.Common.Infrastructure.Extensions;
    using IssueTrackingSystem2.Services.Mapping;
    using IssueTrackingSystem2.Services.Models;
    using IssueTrackingSystem2.Web.Infrastructure.Attributes;
    using IssueTrackingSystem2.Web.InputModels.Project;
    using IssueTrackingSystem2.Web.ViewModels.Status;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class MilestoneUpdateInputModel 
        : IValidatableObject, IMapFrom<MilestoneServiceModel>, IMapTo<MilestoneServiceModel>, IHaveCustomMappings
    {
        public string Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "Completion Date")]
        public DateTime CompletionDate { get; set; }

        [Required]
        [EnumValidation(typeof(MilestoneStatuses))]
        [Display(Name = "Status Name")]
        public string StatusName { get; set; }

        public ProjectConciseInputModel Project { get; set; }

        public IEnumerable<ValidationResult> Validate(System.ComponentModel.DataAnnotations.ValidationContext validationContext)
        {
            //if (this.CompletionDate < DateTime.UtcNow)
            //{
            //    yield return new ValidationResult(string.Format(
            //        format: MessagesConstants.DateTimeEarlierThanNow,
            //        arg0: nameof(this.CompletionDate).SplitStringByCapitalLetters()));
            //}

            if (this.StartDate > this.CompletionDate)
            {
                yield return new ValidationResult(string.Format(
                    format: MessagesConstants.StartDateLaterThanEndDate,
                    arg0: nameof(this.StartDate).SplitStringByCapitalLetters(),
                    arg1: nameof(this.CompletionDate).SplitStringByCapitalLetters()));
            }

            if ((this.StartDate <= DateTime.UtcNow || this.CompletionDate <= DateTime.UtcNow) 
                    && this.StatusName == MilestoneStatuses.NotStarted.ToString())
            {
                yield return new ValidationResult(string.Format(
                    format: MessagesConstants.InvalidMilestoneStatus,
                    arg0: $"{nameof(this.StartDate).SplitStringByCapitalLetters()} or {nameof(this.CompletionDate).SplitStringByCapitalLetters()} earlier or equal than now",
                    arg1: MilestoneStatuses.NotStarted.ToString()));
            }

            if ((this.StartDate > DateTime.UtcNow || this.CompletionDate < DateTime.UtcNow)
                    && this.StatusName == MilestoneStatuses.Started.ToString())
            {
                yield return new ValidationResult(string.Format(
                    format: MessagesConstants.InvalidMilestoneStatus,
                    arg0: $"{nameof(this.StartDate).SplitStringByCapitalLetters()} later than now and {nameof(this.CompletionDate).SplitStringByCapitalLetters()} earlier than now",
                    arg1: MilestoneStatuses.Started.ToString()));
            }

            if ((this.StartDate >= DateTime.UtcNow || this.CompletionDate >= DateTime.UtcNow)
                    && this.StatusName == MilestoneStatuses.Overdued.ToString())
            {
                yield return new ValidationResult(string.Format(
                    format: MessagesConstants.InvalidMilestoneStatus,
                    arg0: $"{nameof(this.StartDate).SplitStringByCapitalLetters()} or {nameof(this.CompletionDate).SplitStringByCapitalLetters()} later or equal than now",
                    arg1: MilestoneStatuses.Overdued.ToString()));
            }

            if (this.StartDate >= DateTime.UtcNow && this.StatusName == MilestoneStatuses.Completed.ToString())
            {
                yield return new ValidationResult(string.Format(
                    format: MessagesConstants.InvalidMilestoneStatus,
                    arg0: $"{nameof(this.StartDate).SplitStringByCapitalLetters()} later or equal than now",
                    arg1: MilestoneStatuses.Completed.ToString()));
            }
        }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<MilestoneUpdateInputModel, MilestoneServiceModel>()
                .ForMember(dest => dest.Status, mapper => mapper.MapFrom(src => new StatusServiceModel()
                {
                    Name = src.StatusName,
                    Type = StatusTypes.Milestone.ToString()
                }));
        }
    }
}
