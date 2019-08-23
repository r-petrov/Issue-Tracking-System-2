namespace IssueTrackingSystem2.Services.Data.Project
{
    using IssueTrackingSystem2.Common;
    using IssueTrackingSystem2.Data.Common.Repositories;
    using IssueTrackingSystem2.Data.Models;
    using IssueTrackingSystem2.Services.Mapping;
    using IssueTrackingSystem2.Services.Models;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class ProjectService : IProjectService
    {
        private readonly IDeletableEntityRepository<Project> repository;

        public ProjectService(IDeletableEntityRepository<Project> repository)
        {
            this.repository = repository;
        }

        public IQueryable<ProjectServiceModel> GetAll()
        {
            var projects = this.repository.All().To<ProjectServiceModel>();

            return projects;
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
            var projectResult = await this.repository.AddAsync(project);

            // var result = await this.efDeletableEntityRepository.SaveChangesAsync();

            var projectServiceModelResult = projectResult.To<ProjectServiceModel>();

            return projectServiceModelResult;
        }

        public async Task<ProjectServiceModel> UpdateAsync(ProjectServiceModel projectServiceModel)
        {
            var project = projectServiceModel.To<Project>();
            var updatedProject = await this.repository.UpdateAsync(project);
            var updatedProjectServiceModel = updatedProject.To<ProjectServiceModel>();

            return updatedProjectServiceModel;
        }
    }
}
