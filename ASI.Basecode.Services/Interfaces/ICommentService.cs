using ASI.Basecode.Data.Models;
using ASI.Basecode.Services.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASI.Basecode.Services.Interfaces
{
    public interface ICommentService
    {
        IQueryable<Comment> GetComments();
        Task<Comment> GetCommentById(string commentId);
        IQueryable<Comment> GetCommentsOfReview(string reviewId);
        void CreateComment(CommentViewModel model);
        void UpdateComment(CommentViewModel update);
        void DeleteComment(string commentId);
    }
}
