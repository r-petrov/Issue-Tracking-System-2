namespace IssueTrackingSystem2.Services.Models
{
    using AutoMapper;
    using IssueTrackingSystem2.Data.Models;
    using IssueTrackingSystem2.Services.Mapping;
    using Microsoft.AspNetCore.Identity;

    public class ApplicationRoleServiceModel : IMapFrom<ApplicationRole>
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}