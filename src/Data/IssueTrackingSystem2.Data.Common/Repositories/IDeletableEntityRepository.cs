namespace IssueTrackingSystem2.Data.Common.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using IssueTrackingSystem2.Data.Common.Models;

    public interface IDeletableEntityRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IDeletableEntity
    {
        IQueryable<TEntity> AllWithDeleted();

        IQueryable<TEntity> AllAsNoTrackingWithDeleted();

        Task<TEntity> GetByIdWithDeletedAsync(params object[] id);

        void HardDelete(TEntity entity);

        bool HardDeleteMultiple(IEnumerable<TEntity> entities);

        void Undelete(TEntity entity);
    }
}
