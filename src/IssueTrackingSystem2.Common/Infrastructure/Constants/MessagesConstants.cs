namespace IssueTrackingSystem2.Common.Infrastructure.Constants
{
    public class MessagesConstants
    {
        public const string StillHaveNotBeenAssignedTo = "{0} still have not been assigned to any {1}";
        public const string StillHaveNotBeenCreated = "No {0} have been created yet";
        public const string StillNotAddedItems = "There still are no any {0} added to {1}";
        public const string StillNotAssignedItems = "There still are no any {0} assigned to {1}";
        public const string NullItem = "There is no {0} with {1} value {2}";
        public const string NullOrEmptyArgument = "{0} should not be null or empty. Please, fill all required form fields.";
        public const string UnauthotizedForProjectLeaderAction = "Only the system Administrator and Project Leader have access to action {0}";
        public const string StartDateLaterThanEndDate = "{0} should not be later than {1}";
        public const string DateTimeEarlierThanNow = "{0} should not be earlier than now";
        public const string NotAmongTheValidValues = "{0} is not among the valid values of {1}";
        public const string InvalidMilestoneStatus = "Milestones with {0} could not have status {1}";
        public const string DeleteCommentsUsersLimitation = "Non-admin users can delete only their comments.";

        public const string ClickOnDesiredRow = "Click on desired {0} row to see its details";
    }
}
