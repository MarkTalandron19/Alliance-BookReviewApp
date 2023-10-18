using ASI.Basecode.Data.Models;
using ASI.Basecode.Services.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASI.Basecode.Services.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewService _reviewService;
        private readonly IMapper _mapper;

        public ReviewService(IReviewService reviewService, IMapper mapper)
        {
            _reviewService = reviewService;
            _mapper = mapper;
        }

        public void AddReview(Review review)
        {
            _reviewService.AddReview(review);
        }

        public void DeleteReview(string reviewId)
        {
            _reviewService.DeleteReview(reviewId);
        }

        public IQueryable<Review> GetBookReview(string bookId)
        {
            return _reviewService.GetBookReview(bookId);
        }

        public IQueryable<Review> GetReviews()
        {
            return _reviewService.GetReviews();
        }

        public Task<Review> GetReviewsById(string reviewId)
        {
            return _reviewService.GetReviewsById(reviewId);
        }

        public void UpdateReview(Review update)
        {
            _reviewService.UpdateReview(update);
        }
    }
}
