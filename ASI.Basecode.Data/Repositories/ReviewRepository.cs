using ASI.Basecode.Data.Interfaces;
using ASI.Basecode.Data.Models;
using Basecode.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASI.Basecode.Data.Repositories
{
    public class ReviewRepository : BaseRepository, IReviewRepository
    {
        public ReviewRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IQueryable<Review> GetReviews()
        {
            return this.GetDbSet<Review>();
        }
        public Task<Review> GetReviewsById(string reviewId)
        {
            var review = this.GetDbSet<Review>().Where(r => r.reviewId == reviewId).SingleOrDefaultAsync();

            return review;
        }

        public IQueryable<Review> GetBookReview(string bookId)
        {
            return this.GetDbSet<Review>().Where(r => r.bookId == bookId);
        }

        public void AddReview(Review review)
        {
            this.GetDbSet<Review>().Add(review);
            UnitOfWork.SaveChanges();
        }

        public void DeleteReview(string reviewId)
        {
            var review = this.GetDbSet<Review>().SingleOrDefault(r => r.reviewId == reviewId);

            if (review != null)
            {
                this.GetDbSet<Review>().Remove(review);
                UnitOfWork.SaveChanges();
            }

        }

        public void UpdateReview(Review update)
        {
            var review = this.GetDbSet<Review>().SingleOrDefault(r => r.reviewId == update.reviewId);

            if (review != null)
            {
                review.reviewerFirstName = update.reviewerFirstName;
                review.reviewerLastName = update.reviewerLastName;
                review.reviewerEmail = update.reviewerEmail;
                review.content = update.content;
                review.rating = update.rating;
                UnitOfWork.SaveChanges();
            }
        }
    } 
}
