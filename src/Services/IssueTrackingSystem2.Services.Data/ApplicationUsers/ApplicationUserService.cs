using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public IEnumerable<ApplicationUserServiceModel> GetAllApplicationUsers()
        {
            var applicationUsers = this.efDeletableEntityRepository.All().ToList();
            var users = Mapper.Map<IEnumerable<ApplicationUserServiceModel>>(applicationUsers);

            return users;
        }
    }
}
