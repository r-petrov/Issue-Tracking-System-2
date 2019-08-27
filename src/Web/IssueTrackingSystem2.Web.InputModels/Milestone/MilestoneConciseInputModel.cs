using IssueTrackingSystem2.Web.InputModels.Project;
using System;
using System.Collections.Generic;
using System.Text;

namespace IssueTrackingSystem2.Web.InputModels.Milestone
{
    public class MilestoneConciseInputModel
    {
        public string Id { get; set; }

        public string ProjectLeaderId { get; set; }
    }
}
