using ASI.Basecode.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASI.Basecode.Data.Interfaces
{
    public interface ICommentRepository
    {
        IQueryable<Comment> GetComments();
        Task<Comment> GetCommentById(string commentId);
        IQueryable<Comment> GetCommentsOfReview(string reviewId);
        void CreateComment(Comment comment);
        void UpdateComment(Comment update);
        void DeleteComment(string commentId);
        bool CommentExists(string commentId);
    }
}
