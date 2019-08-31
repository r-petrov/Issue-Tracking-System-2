using IssueTrackingSystem2.Data.Models;
using IssueTrackingSystem2.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace IssueTrackingSystem2.Services.Models
{
    public class ProjectLabelServiceModel : IMapTo<ProjectLabel>, IMapFrom<ProjectLabel>
    {
        public string ProjectId { get; set; }

        public ProjectServiceModel Project { get; set; }

        public string LabelId { get; set; }

        public LabelServiceModel Label { get; set; }
    }
}
