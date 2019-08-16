namespace IssueTrackingSystem2.Web.InputModels.Project
{
    using AutoMapper;
    using IssueTrackingSystem2.Services.Mapping;
    using IssueTrackingSystem2.Services.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class ProjectCreateInputModel : IMapTo<ProjectServiceModel>, IHaveCustomMappings
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public virtual string LeaderId { get; set; }

        [Required]
        public virtual string Priorities { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ProjectCreateInputModel, ProjectServiceModel>()
                .ForMember(dest => dest.ProjectKey, mapper => mapper.MapFrom(src => this.GenerateProjectKey()))
                .ForMember(dest => dest.Priorities, mapper => mapper.MapFrom(src => this.GeneratePriorities()))
                .ForMember(dest => dest.CreatedOn, mapper => mapper.MapFrom(src => DateTime.UtcNow));
        }

        private string GenerateProjectKey()
        {
            var projectNameParts = this.Name.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var projectKey = new StringBuilder();
            foreach (var projectNamePart in projectNameParts)
            {
                projectKey.Append(projectNamePart[0]);
            }

            return projectKey.ToString();
        }

        private IEnumerable<PriorityServiceModel> GeneratePriorities()
        {
            var priorityNames = this.Priorities.Split(
                new char[] { ',', ';', ' ' },
                StringSplitOptions.RemoveEmptyEntries);

            foreach (var priorityName in priorityNames)
            {
                yield return new PriorityServiceModel()
                {
                    Name = priorityName
                };
            }
        }
    }
}
