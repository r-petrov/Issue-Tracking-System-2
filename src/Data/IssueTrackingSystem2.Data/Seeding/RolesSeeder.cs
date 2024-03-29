﻿namespace IssueTrackingSystem2.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using IssueTrackingSystem2.Common;
    using IssueTrackingSystem2.Common.Infrastructure.Constants;
    using IssueTrackingSystem2.Data.Models;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    internal class RolesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            // Seed Roles if not created
            // dbContext.Database.EnsureCreated();
            await SeedRoleAsync(roleManager, GlobalConstants.AdministratorRoleName);
            await SeedRoleAsync(roleManager, GlobalConstants.UserRoleName);
            await SeedRoleAsync(roleManager, GlobalConstants.ProjectLeaderRoleName);
            await SeedRoleAsync(roleManager, GlobalConstants.IssueAssigneeRoleName);
        }

        private static async Task SeedRoleAsync(RoleManager<ApplicationRole> roleManager, string roleName)
        {
            var role = await roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                var result = await roleManager.CreateAsync(new ApplicationRole(roleName));
                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }
            }
        }
    }
}
