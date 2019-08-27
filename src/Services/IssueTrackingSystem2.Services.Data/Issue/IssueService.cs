namespace IssueTrackingSystem2.Services.Data.Issue
{
    using IssueTrackingSystem2.Common.Infrastructure.Extensions;
    using IssueTrackingSystem2.Data.Common.Repositories;
    using IssueTrackingSystem2.Data.Models;
    using IssueTrackingSystem2.Services.Mapping;
    using IssueTrackingSystem2.Services.Models;
    using System;
    using System.Threading.Tasks;

    public class IssueService : IIssueService
    {
        private readonly IDeletableEntityRepository<Issue> repository;

        public IssueService(IDeletableEntityRepository<Issue> repository)
        {
            this.repository = repository;
        }

        public async Task<IssueServiceModel> CreateAsync(IssueServiceModel issueServiceModel)
        {
            var issue = issueServiceModel.To<Issue>();
            issue.IssueKey = issueServiceModel.Title.ApendStringCapitalLetters();
            var issueResult = await this.repository.AddAsync(issue);
            var issueServiceModelResult = issueResult.To<IssueServiceModel>();

            return issueServiceModelResult;
        }

        public async Task<IssueServiceModel> ByIdAsync(string id)
        {
            var issue = await this.repository.ByIdAsync(id);
            var issueServiceModel = issue.To<IssueServiceModel>();

            return issueServiceModel;
        }
    }
}
