﻿using ASI.Basecode.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASI.Basecode.Data.Interfaces
{
    public interface IReviewRepository
    {
        IQueryable<Review> GetReviews();
        Task<Review> GetReviewsById(string reviewId);
        IQueryable<Review> GetBookReview(string bookId);
        void AddReview(Review review);
        void UpdateReview(Review update);
        void DeleteReview(string reviewId);
    }
}
