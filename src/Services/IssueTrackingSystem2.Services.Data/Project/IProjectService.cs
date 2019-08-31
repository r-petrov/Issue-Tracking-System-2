namespace IssueTrackingSystem2.Services.Data.Project
{
    using IssueTrackingSystem2.Services.Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IProjectService
    {
        IEnumerable<ProjectServiceModel> All();
        //IQueryable<ProjectServiceModel> All();

        Task<ProjectServiceModel> ByIdAsync(string id);

        Task<ProjectServiceModel> CreateAsync(ProjectServiceModel projectServiceModel);

        Task<ProjectServiceModel> UpdateAsync(ProjectServiceModel projectServiceModel);

        Task<ProjectLabelServiceModel> AddLabelAsync(ProjectLabelServiceModel projectLabelServiceModel);
    }
}
