using IssueTrackingSystem2.Services.Mapping;
using IssueTrackingSystem2.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IssueTrackingSystem2.Web.ViewModels.Comment
{
    public class CommentDetailsViewModel : IMapFrom<CommentServiceModel>
    {
        public string Id { get; set; }

        public string Text { get; set; }

        public ApplicationUserViewModel CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
