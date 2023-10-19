using ASI.Basecode.Data.Models;
using ASI.Basecode.Services.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASI.Basecode.Services.Interfaces
{
    public interface IReviewService
    {
        IQueryable<Review> GetReviews();
        Task<Review> GetReviewsById(string reviewId);
        IQueryable<Review> GetBookReview(string bookId);
        void AddReview(ReviewViewModel model);
        void UpdateReview(ReviewViewModel update);
        void DeleteReview(string reviewId);
    }
}
