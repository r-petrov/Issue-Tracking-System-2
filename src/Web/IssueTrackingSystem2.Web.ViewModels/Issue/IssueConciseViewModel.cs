﻿namespace IssueTrackingSystem2.Web.ViewModels.Issue
{
    using IssueTrackingSystem2.Services.Mapping;
    using IssueTrackingSystem2.Services.Models;


    public class IssueConciseViewModel : IMapFrom<IssueServiceModel>
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string AssigneeId { get; set; }
    }
}
