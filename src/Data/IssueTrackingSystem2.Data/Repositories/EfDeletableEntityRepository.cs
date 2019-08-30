namespace IssueTrackingSystem2.Data.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using IssueTrackingSystem2.Data.Common.Models;
    using IssueTrackingSystem2.Data.Common.Repositories;

    using Microsoft.EntityFrameworkCore;

    public class EfDeletableEntityRepository<TEntity> : EfRepository<TEntity>, IDeletableEntityRepository<TEntity>
        where TEntity : class, IDeletableEntity
    {
        public EfDeletableEntityRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public override IQueryable<TEntity> All() => base.All().Where(x => !x.IsDeleted);

        public override IQueryable<TEntity> AllAsNoTracking() => base.AllAsNoTracking().Where(x => !x.IsDeleted);

        public IQueryable<TEntity> AllWithDeleted() => base.All().IgnoreQueryFilters();

        public IQueryable<TEntity> AllAsNoTrackingWithDeleted() => base.AllAsNoTracking().IgnoreQueryFilters();

        public Task<TEntity> GetByIdWithDeletedAsync(params object[] id)
        {
            var byIdPredicate = EfExpressionHelper.BuildByIdPredicate<TEntity>(this.Context, id);

            return this.AllWithDeleted().FirstOrDefaultAsync(byIdPredicate);
        }

        public bool HardDeleteMultiple(IEnumerable<TEntity> entities)
        {
            this.DbSet.RemoveRange(entities);
            var result = this.SaveChangesAsync().GetAwaiter().GetResult();

            return result > 0;
        }

        public void HardDelete(TEntity entity) => base.Delete(entity);

        public void Undelete(TEntity entity)
        {
            entity.IsDeleted = false;
            entity.DeletedOn = null;

            this.UpdateAsync(entity).GetAwaiter().GetResult();
        }

        public override void Delete(TEntity entity)
        {
            entity.IsDeleted = true;
            entity.DeletedOn = DateTime.UtcNow;

            this.UpdateAsync(entity).GetAwaiter().GetResult();
        }

        public async Task<TEntity> DeleteAsync(TEntity entity)
        {
            entity.IsDeleted = true;
            entity.DeletedOn = DateTime.UtcNow;

            var result = await this.UpdateAsync(entity);

            return result;
        }
    }
}
