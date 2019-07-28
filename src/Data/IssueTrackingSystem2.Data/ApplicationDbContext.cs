namespace IssueTrackingSystem2.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;

    using IssueTrackingSystem2.Data.Common.Models;
    using IssueTrackingSystem2.Data.Models;

    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        private static readonly MethodInfo SetIsDeletedQueryFilterMethod =
            typeof(ApplicationDbContext).GetMethod(
                nameof(SetIsDeletedQueryFilter),
                BindingFlags.NonPublic | BindingFlags.Static);

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Setting> Settings { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<ProjectLabel> ProjectLabels { get; set; }

        public DbSet<Milestone> Milestones { get; set; }

        public DbSet<Issue> Issues { get; set; }

        public DbSet<IssueLabel> IssueLabels { get; set; }

        public DbSet<Label> Labels { get; set; }

        public DbSet<Priority> Priorities { get; set; }

        public DbSet<Status> Statuses { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public override int SaveChanges() => this.SaveChanges(true);

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            this.ApplyAuditInfoRules();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
            this.SaveChangesAsync(true, cancellationToken);

        public override Task<int> SaveChangesAsync(
            bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            this.ApplyAuditInfoRules();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Needed for Identity models configuration
            base.OnModelCreating(builder);

            ConfigureUserIdentityRelations(builder);
            ConfigureIssueLabelRelations(builder);
            ConfigureProjectLabelRelations(builder);

            EntityIndexesConfiguration.Configure(builder);

            var entityTypes = builder.Model.GetEntityTypes().ToList();

            // Set global query filter for not deleted entities only
            SetGlobalQueryFilterForNotDeletedEntities(builder, entityTypes);

            // Disable cascade delete
            DisableCascadeDelete(entityTypes);
        }

        private static void ConfigureUserIdentityRelations(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>()
                .HasMany(e => e.Claims)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ApplicationUser>()
                .HasMany(e => e.Logins)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ApplicationUser>()
                .HasMany(e => e.Roles)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }

        private static void ConfigureProjectLabelRelations(ModelBuilder builder)
        {
            builder.Entity<ProjectLabel>().HasKey(pl => new { pl.ProjectId, pl.LabelId });
        }

        private static void ConfigureIssueLabelRelations(ModelBuilder builder)
        {
            //builder.Entity<IssueLabel>()
            //    .HasOne<Issue>(il => il.Issue)
            //    .WithMany(i => i.Labels)
            //    .HasForeignKey(e => e.IssueId)
            //    .OnDelete(DeleteBehavior.Restrict);

            //builder.Entity<IssueLabel>()
            //    .HasOne<Label>(il => il.Label)
            //    .WithMany(l => l.Issues)
            //    .HasForeignKey(e => e.LabelId)
            //    .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<IssueLabel>().HasKey(il => new { il.IssueId, il.LabelId });
        }

        private static void SetIsDeletedQueryFilter<T>(ModelBuilder builder)
            where T : class, IDeletableEntity
        {
            builder.Entity<T>().HasQueryFilter(e => !e.IsDeleted);
        }

        private void ApplyAuditInfoRules()
        {
            var changedEntries = this.ChangeTracker
                .Entries()
                .Where(e =>
                    e.Entity is IAuditInfo &&
                    (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in changedEntries)
            {
                var entity = (IAuditInfo)entry.Entity;
                if (entry.State == EntityState.Added && entity.CreatedOn == default)
                {
                    entity.CreatedOn = DateTime.UtcNow;
                }
                else
                {
                    entity.ModifiedOn = DateTime.UtcNow;
                }
            }
        }

        private static void SetGlobalQueryFilterForNotDeletedEntities(
            ModelBuilder builder,
            List<IMutableEntityType> entityTypes)
        {
            var deletableEntityTypes = entityTypes
                            .Where(et => et.ClrType != null && typeof(IDeletableEntity).IsAssignableFrom(et.ClrType));
            foreach (var deletableEntityType in deletableEntityTypes)
            {
                var method = SetIsDeletedQueryFilterMethod.MakeGenericMethod(deletableEntityType.ClrType);
                method.Invoke(null, new object[] { builder });
            }
        }

        private static void DisableCascadeDelete(List<IMutableEntityType> entityTypes)
        {
            var foreignKeys = entityTypes
                            .SelectMany(e => e.GetForeignKeys().Where(f => f.DeleteBehavior == DeleteBehavior.Cascade));
            foreach (var foreignKey in foreignKeys)
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}
