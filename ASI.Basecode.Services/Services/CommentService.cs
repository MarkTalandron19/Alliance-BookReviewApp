using ASI.Basecode.Data.Interfaces;
using ASI.Basecode.Data.Models;
using ASI.Basecode.Services.Interfaces;
using ASI.Basecode.Services.ServiceModels;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ASI.Basecode.Services.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _repository;
        private readonly IMapper _mapper;

        public CommentService(ICommentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public void CreateComment(CommentViewModel model)
        {
            var comment = new Comment();
            if (!_repository.CommentExists(model.commentId))
            {
                _mapper.Map(model, comment);
                comment.reviewId = model.commentId;
                comment.commenterLastName = model.commenterLastName;
                comment.commenterFirstName = model.commenterFirstName;
                comment.dateCommented = DateTime.Now;
                comment.reviewId = model.reviewId;
                _repository.CreateComment(comment);
            }
        }

        public void DeleteComment(string commentId)
        {
            if (_repository.CommentExists(commentId))
            {
                _repository.DeleteComment(commentId);
            }
        }

        public async Task<Comment> GetCommentById(string commentId)
        {
            return await _repository.GetCommentById(commentId);
        }

        public IQueryable<Comment> GetComments()
        {
            return _repository.GetComments();
        }

        public IQueryable<Comment> GetCommentsOfReview(string reviewId)
        {
            return _repository.GetCommentsOfReview(reviewId);
        }

        public void UpdateComment(CommentViewModel update)
        {
            var comment = new Comment();
            if (_repository.CommentExists(update.commentId))
            {
                _mapper.Map(update, comment);
                _repository.UpdateComment(comment);
            }
        }
    }
}
