namespace IssueTrackingSystem2.Data.Common.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IRepository<TEntity> : IDisposable
        where TEntity : class
    {
        IQueryable<TEntity> All();

        IQueryable<TEntity> AllAsNoTracking();

        Task<TEntity> ByIdAsync<T>(T id);

        Task<TEntity> AddAsync(TEntity entity);


        //void Update(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entity);

        void Delete(TEntity entity);

        Task<int> SaveChangesAsync();
    }
}
