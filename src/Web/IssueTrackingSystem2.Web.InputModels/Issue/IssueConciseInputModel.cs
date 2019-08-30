using IssueTrackingSystem2.Services.Mapping;
using IssueTrackingSystem2.Services.Models;
using IssueTrackingSystem2.Web.InputModels.Milestone;
using System;
using System.Collections.Generic;
using System.Text;

namespace IssueTrackingSystem2.Web.InputModels.Issue
{
    public class IssueConciseInputModel : IMapTo<IssueServiceModel>, IMapFrom<IssueServiceModel>
    {
        public string Id { get; set; }

        public string AssigneeId { get; set; }

        public MilestoneConciseInputModel Milestone { get; set; }
    }
}
