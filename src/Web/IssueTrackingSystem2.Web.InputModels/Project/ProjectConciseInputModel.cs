using IssueTrackingSystem2.Services.Mapping;
using IssueTrackingSystem2.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IssueTrackingSystem2.Web.InputModels.Project
{
    public class ProjectConciseInputModel : IMapFrom<ProjectServiceModel>, IMapTo<ProjectServiceModel>
    {
        public string Id { get; set; }

        public string LeaderId { get; set; }
    }
}
