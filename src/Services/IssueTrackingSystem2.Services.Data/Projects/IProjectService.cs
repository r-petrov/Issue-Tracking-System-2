namespace IssueTrackingSystem2.Services.Data.Projects
{
    using IssueTrackingSystem2.Services.Models;
    using System.Linq;

    public interface IProjectService
    {
        IQueryable<ProjectServiceModel> GetAll();
    }
}
