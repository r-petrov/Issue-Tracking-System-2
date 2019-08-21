namespace IssueTrackingSystem2.Services.Data.Project
{
    using IssueTrackingSystem2.Services.Models;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IProjectService
    {
        IQueryable<ProjectServiceModel> GetAll();

        Task<ProjectServiceModel> GetByIdAsync(string id);

        Task<ProjectServiceModel> CreateAsync(ProjectServiceModel projectServiceModel);

        Task<ProjectServiceModel> UpdateAsync(ProjectServiceModel projectServiceModel);
    }
}
