namespace IssueTrackingSystem2.Services.Data.Projects
{
    using IssueTrackingSystem2.Data.Common.Repositories;
    using IssueTrackingSystem2.Data.Models;
    using IssueTrackingSystem2.Services.Mapping;
    using IssueTrackingSystem2.Services.Models;
    using System.Linq;

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
    }
}
