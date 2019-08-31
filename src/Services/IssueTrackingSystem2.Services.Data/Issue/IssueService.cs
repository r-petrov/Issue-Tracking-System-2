namespace IssueTrackingSystem2.Services.Data.Issue
{
    using IssueTrackingSystem2.Common.Infrastructure.Constants;
    using IssueTrackingSystem2.Common.Infrastructure.Extensions;
    using IssueTrackingSystem2.Data.Common.Repositories;
    using IssueTrackingSystem2.Data.Models;
    using IssueTrackingSystem2.Services.Mapping;
    using IssueTrackingSystem2.Services.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class IssueService : IIssueService
    {
        private readonly IDeletableEntityRepository<Issue> repository;

        public IssueService(IDeletableEntityRepository<Issue> repository)
        {
            this.repository = repository;
        }

        public IEnumerable<IssueServiceModel> All()
        {
            var issues = this.repository
                .All()
                .OrderByDescending(issue => issue.DueDate)
                .ToList();

            var issueListServiceModels = issues.To<IssueServiceModel>();

            return issueListServiceModels;
        }

        public IEnumerable<IssueServiceModel> AllByMilestoneId(string milestoneId)
        {
            var issues = this.repository
                .All()
                .Where(issue => issue.MilestoneId == milestoneId)
                .OrderByDescending(issue => issue.DueDate)
                .ToList();

            var issueListServiceModels = issues.To<IssueServiceModel>();

            return issueListServiceModels;
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
            if (issue == null)
            {
                throw new Exception(string.Format(
                    format: MessagesConstants.NullItem,
                    arg0: nameof(issue),
                    arg1: nameof(id),
                    arg2: id));
            }

            var issueServiceModel = issue.To<IssueServiceModel>();

            return issueServiceModel;
        }

        public async Task<IssueServiceModel> UpdateAsync(IssueServiceModel issueServiceModel)
        {
            var issue = await this.repository.ByIdAsync(issueServiceModel.Id);
            if (issue == null)
            {
                throw new Exception(string.Format(
                   format: MessagesConstants.NullItem,
                   arg0: GlobalConstants.Issue,
                   arg1: nameof(IssueServiceModel.Id),
                   arg2: issueServiceModel.Id));
            }

            issue.Title = issueServiceModel.Title;
            issue.Description = issueServiceModel.Description;
            issue.IssueKey = issueServiceModel.Title.ApendStringCapitalLetters();
            issue.DueDate = issueServiceModel.DueDate;
            issue.Priority = issueServiceModel.Priority;
            issue.Status.Name = issueServiceModel.Status.Name;
            issue.Status.Type = issueServiceModel.Status.Type;
            issue.AssigneeId = issueServiceModel.AssigneeId;

            var updatedIssue = await this.repository.UpdateAsync(issue);
            var updatedIssueServiceModel = updatedIssue.To<IssueServiceModel>();

            return updatedIssueServiceModel;
        }
    }
}
