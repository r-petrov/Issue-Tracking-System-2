namespace IssueTrackingSystem2.Services.Data.Projects
{
    using IssueTrackingSystem2.Services.Models;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IProjectService
    {
        IQueryable<ProjectServiceModel> GetAll();

        Task<bool> CreateAsync(ProjectServiceModel project);
    }
}
