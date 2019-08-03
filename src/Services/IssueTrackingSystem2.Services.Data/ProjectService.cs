namespace IssueTrackingSystem2.Services.Data
{
    using IssueTrackingSystem2.Data.Common.Repositories;
    using IssueTrackingSystem2.Data.Models;
    using System.Collections.Generic;

    public class ProjectService
    {
        private readonly IDeletableEntityRepository<Project> efDeletableEntityRepository;

        public ProjectService(IDeletableEntityRepository<Project> efDeletableEntityRepository)
        {
            this.efDeletableEntityRepository = efDeletableEntityRepository;
        }

        public IEnumerable<Project> GetAll()
        {
            var projects = this.efDeletableEntityRepository.All();

            return projects;
        }
    }
}
