namespace IssueTrackingSystem2.Services.Data.Project
{
    using IssueTrackingSystem2.Common.Infrastructure.Constants;
    using IssueTrackingSystem2.Common.Infrastructure.Extensions;
    using IssueTrackingSystem2.Data.Common.Repositories;
    using IssueTrackingSystem2.Data.Models;
    using IssueTrackingSystem2.Data.Repositories;
    using IssueTrackingSystem2.Services.Mapping;
    using IssueTrackingSystem2.Services.Models;
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ProjectService : IProjectService
    {
        private readonly IDeletableEntityRepository<Project> repository;
        private readonly IRepository<ProjectLabel> projectLabelRepository;
        private readonly Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> userManager;

        public ProjectService(IDeletableEntityRepository<Project> repository, IRepository<ProjectLabel> projectLabelRepository, UserManager<ApplicationUser> userManager)
        {
            this.repository = repository;
            this.projectLabelRepository = projectLabelRepository;
            this.userManager = userManager;
        }

        public IEnumerable<ProjectServiceModel> All()
        {
            var projects = this.repository.All().OrderByDescending(project => project.CreatedOn).ToList();
            var projectServiceModels = projects.To<ProjectServiceModel>();

            return projectServiceModels;
        }

        public async Task<ProjectServiceModel> ByIdAsync(string id)
        {
            var project = await this.repository.ByIdAsync(id);
            if (project == null)
            {
                throw new Exception(string.Format(
                    format: MessagesConstants.NullItem,
                    arg0: nameof(project),
                    arg1: nameof(id),
                    arg2: id));
            }

            var projectResult = project.To<ProjectServiceModel>();

            return projectResult;
        }

        public async Task<ProjectServiceModel> CreateAsync(ProjectServiceModel projectServiceModel)
        {
            Project project = projectServiceModel.To<Project>();
            if (project == null)
            {
                throw new Exception(string.Format(
                   format: MessagesConstants.NullItem,
                   arg0: GlobalConstants.Project,
                   arg1: nameof(ProjectServiceModel.Id),
                   arg2: projectServiceModel.Id));
            }

            project.ProjectKey = projectServiceModel.Name.ApendStringCapitalLetters();
            
            var projectResult = await this.repository.AddAsync(project);

            var projectServiceModelResult = projectResult.To<ProjectServiceModel>();

            return projectServiceModelResult;
        }

        public async Task<ProjectServiceModel> UpdateAsync(ProjectServiceModel projectServiceModel)
        {
            var project = await this.repository.ByIdAsync(projectServiceModel.Id);
            if (project == null)
            {
                throw new Exception(string.Format(
                   format: MessagesConstants.NullItem,
                   arg0: GlobalConstants.Project,
                   arg1: nameof(ProjectServiceModel.Id),
                   arg2: projectServiceModel.Id));
            }

            project.Name = projectServiceModel.Name;
            project.Description = projectServiceModel.Description;
            project.ProjectKey = projectServiceModel.Name.ApendStringCapitalLetters();
            project.LeaderId = projectServiceModel.LeaderId;
            project.Leader = await this.userManager.FindByIdAsync(projectServiceModel.LeaderId);

            var updatedProject = await this.repository.UpdateAsync(project);
            var updatedProjectServiceModel = updatedProject.To<ProjectServiceModel>();

            return updatedProjectServiceModel;
        }

        public async Task<ProjectLabelServiceModel> AddLabelAsync(ProjectLabelServiceModel projectLabelServiceModel)
        {
            var label = projectLabelServiceModel.To<ProjectLabel>();
            var labelResult = await this.projectLabelRepository.AddAsync(label);
            var projectLabelServiceModelsResult = new ProjectLabelServiceModel()
            {
                ProjectId = labelResult.ProjectId,
                Project = labelResult.Project.To<ProjectServiceModel>(),
                LabelId = labelResult.LabelId,
                Label = labelResult.Label.To<LabelServiceModel>(),
            };
            
            //var projectLabelServiceModelsResult = labelResult.To<ProjectLabelServiceModel>();

            return projectLabelServiceModelsResult;
        }
    }
}
