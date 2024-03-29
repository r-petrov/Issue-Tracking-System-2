﻿namespace IssueTrackingSystem2.Services.Data.Comment
{
    using IssueTrackingSystem2.Data.Common.Repositories;
    using IssueTrackingSystem2.Data.Models;
    using IssueTrackingSystem2.Services.Mapping;
    using IssueTrackingSystem2.Services.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CommentService : ICommentService
    {
        private readonly IDeletableEntityRepository<Comment> repository;

        public CommentService(IDeletableEntityRepository<Comment> repository)
        {
            this.repository = repository;
        }

        public IEnumerable<CommentServiceModel> All(string issueId)
        {
            var comments = this.repository
                .All()
                .Where(comment => comment.IssueId == issueId)
                .OrderByDescending(comment => comment.CreatedAt)
                .ToList();

            var commentServiceModels = comments.To<CommentServiceModel>();

            return commentServiceModels;
        }

        public async Task<CommentServiceModel> ByIdAsync(string id)
        {
            var comment = await this.repository.ByIdAsync(id);
            var commentServiceModel = comment.To<CommentServiceModel>();

            return commentServiceModel;
        }

        //public IQueryable<CommentServiceModel> All(string issueId)
        //{
        //    var comments = this.repository.All().Where(comment => comment.IssueId == issueId);
        //    var commentServiceModels = comments.To<CommentServiceModel>();

        //    return commentServiceModels;
        //}

        public async Task<CommentServiceModel> CreateAsync(CommentServiceModel commentServiceModel)
        {
            var comment = commentServiceModel.To<Comment>();
            var commentResult = await this.repository.AddAsync(comment);
            var commentServiceModelResult = commentResult.To<CommentServiceModel>();

            return commentServiceModelResult;
        }

        public async Task<CommentServiceModel> RemoveSafeAsync(string id)
        {
            var commentServiceModel = await this.repository.ByIdAsync(id);
            var comment = commentServiceModel.To<Comment>();
            var commentResult = await this.repository.DeleteAsync(comment);
            var commentServiceModelResult = commentResult.To<CommentServiceModel>();

            return commentServiceModelResult;
        }
    }
}
