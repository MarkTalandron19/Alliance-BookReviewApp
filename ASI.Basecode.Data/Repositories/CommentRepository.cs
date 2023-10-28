using ASI.Basecode.Data.Interfaces;
using ASI.Basecode.Data.Models;
using Basecode.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ASI.Basecode.Data.Repositories
{
    public class CommentRepository : BaseRepository, ICommentRepository
    {
        public CommentRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public bool CommentExists(string commentId)
        {
            return this.GetDbSet<Comment>().Any(x => x.commentId == commentId);
        }

        public void CreateComment(Comment comment)
        {
            this.GetDbSet<Comment>().Add(comment);
            UnitOfWork.SaveChanges();
        }

        public void DeleteComment(string commentId)
        {
            var comment = this.GetDbSet<Comment>().SingleOrDefault(c => c.commentId == commentId);

            if (comment != null)
            {
                this.GetDbSet<Comment>().Remove(comment);
                UnitOfWork.SaveChanges();
            }
        }

        public Task<Comment> GetCommentById(string commentId)
        {
            var comment = this.GetDbSet<Comment>().Where(c => c.commentId == commentId).SingleOrDefaultAsync();

            return comment;
        }

        public IQueryable<Comment> GetComments()
        {
            return this.GetDbSet<Comment>();
        }

        public IQueryable<Comment> GetCommentsOfReview(string reviewId)
        {
            var comments = this.GetDbSet<Comment>()
                .Where(c => c.reviewId == reviewId);

            return comments;
        }

        public void UpdateComment(Comment update)
        {
            var comment = this.GetDbSet<Comment>().SingleOrDefault(c => c.commentId == update.commentId);

            if (comment != null)
            {
                comment.commenterFirstName = update.commenterFirstName;
                comment.commenterLastName = update.commenterLastName;
                comment.comment = update.comment;
                comment.dateCommented = DateTime.Now;
                UnitOfWork.SaveChanges();
            }
        }
    }
}
