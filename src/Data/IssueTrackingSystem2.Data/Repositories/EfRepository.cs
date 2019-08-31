namespace IssueTrackingSystem2.Data.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using IssueTrackingSystem2.Data.Common.Repositories;

    using Microsoft.EntityFrameworkCore;

    public class EfRepository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        public EfRepository(ApplicationDbContext context)
        {
            this.Context = context ?? throw new ArgumentNullException(nameof(context));
            this.DbSet = this.Context.Set<TEntity>();
        }

        protected DbSet<TEntity> DbSet { get; set; }

        protected ApplicationDbContext Context { get; set; }

        public virtual IQueryable<TEntity> All() => this.DbSet;

        public virtual IQueryable<TEntity> AllAsNoTracking() => this.DbSet.AsNoTracking();

        public virtual async Task<TEntity> ByIdAsync<T>(T id) => await this.DbSet.FindAsync(id);

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            await this.DbSet.AddAsync(entity);
            await this.SaveChangesAsync();

            return entity;
        }

        //public virtual async Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities)
        //{
        //    await this.DbSet.AddRangeAsync(entities);
        //    await this.SaveChangesAsync();

        //    return entities;
        //}

        //public virtual Task AddAsync(TEntity entity) => this.DbSet.AddAsync(entity);

        //public virtual void Update(TEntity entity)
        //{
        //    var entry = this.Context.Entry(entity);
        //    if (entry.State == EntityState.Detached)
        //    {
        //        this.DbSet.Attach(entity);
        //    }

        //    entry.State = EntityState.Modified;
        //}

        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            this.DbSet.Update(entity);
            await this.SaveChangesAsync();

            return entity;
        }

        public virtual void Delete(TEntity entity) => this.DbSet.Remove(entity);

        public Task<int> SaveChangesAsync() => this.Context.SaveChangesAsync();

        public void Dispose() => this.Context.Dispose();
    }
}
