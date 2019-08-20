namespace IssueTrackingSystem2.Services.Data.Projects
{
    using IssueTrackingSystem2.Data.Common.Repositories;
    using IssueTrackingSystem2.Data.Models;
    using IssueTrackingSystem2.Services.Mapping;
    using IssueTrackingSystem2.Services.Models;
    using System.Linq;
    using System.Threading.Tasks;

    public class ProjectService : IProjectService
    {
        private readonly IDeletableEntityRepository<Project> efDeletableEntityRepository;

        public ProjectService(IDeletableEntityRepository<Project> efDeletableEntityRepository)
        {
            this.efDeletableEntityRepository = efDeletableEntityRepository;
        }

        public IQueryable<ProjectServiceModel> GetAll()
        {
            var projects = this.efDeletableEntityRepository.All().To<ProjectServiceModel>();

            return projects;
        }

        public async Task<ProjectServiceModel> GetByIdAsync(string id)
        {
            var project = await this.efDeletableEntityRepository.ByIdAsync(id);
            var projectResult = project.To<ProjectServiceModel>();

            return projectResult;
        }

        public async Task<ProjectServiceModel> CreateAsync(ProjectServiceModel projectServiceModel)
        {
            Project project = projectServiceModel.To<Project>();
            var projectResult = await this.efDeletableEntityRepository.AddAsync(project);

            // var result = await this.efDeletableEntityRepository.SaveChangesAsync();

            var projectServiceModelResult = projectResult.To<ProjectServiceModel>();

            return projectServiceModelResult;
        }

        public ProjectServiceModel Update(ProjectServiceModel projectServiceModel)
        {
            var project = projectServiceModel.To<Project>();
            var updatedProject = this.efDeletableEntityRepository.Update(project);
            var updatedProjectServiceModel = updatedProject.To<ProjectServiceModel>();

            return updatedProjectServiceModel;
        }
    }
}
