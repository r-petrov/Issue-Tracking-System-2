﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using IssueTrackingSystem2.Data.Common.Repositories;
using IssueTrackingSystem2.Data.Models;
using IssueTrackingSystem2.Services.Models;

namespace IssueTrackingSystem2.Services.Data.ApplicationUsers
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> efDeletableEntityRepository;

        public ApplicationUserService(IDeletableEntityRepository<ApplicationUser> efDeletableEntityRepository)
        {
            this.efDeletableEntityRepository = efDeletableEntityRepository;
        }

        public async Task<ApplicationUserServiceModel> ById(string id)
        {
            var applicationUser = await this.efDeletableEntityRepository.ByIdAsync(id);
            var applicationUserServiceModel = Mapper.Map<ApplicationUserServiceModel>(applicationUser);

            return applicationUserServiceModel;
        }

        public IEnumerable<ApplicationUserServiceModel> GetAllApplicationUsers()
        {
            var applicationUsers = this.efDeletableEntityRepository.All().ToList();
            var users = Mapper.Map<IEnumerable<ApplicationUserServiceModel>>(applicationUsers);

            return users;
        }
    }
}
