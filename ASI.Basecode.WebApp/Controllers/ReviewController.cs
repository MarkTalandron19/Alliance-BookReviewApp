using ASI.Basecode.Data.Models;
using ASI.Basecode.Services.Interfaces;
using ASI.Basecode.Services.ServiceModels;
using ASI.Basecode.Services.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using System.Net;

namespace ASI.Basecode.WebApp.Controllers
{
    [Route("reviews")]
    public class ReviewController : Controller
    {
        private readonly IReviewService _reviewService;
        private readonly IMapper _mapper;

        public ReviewController(IReviewService reviewService, IMapper mapper)
        {
            _reviewService = reviewService;
            _mapper = mapper;
        }

        [HttpPost("add")]
        public IActionResult AddReview(int bookId, ReviewViewModel review)
        {
            review.reviewId = Guid.NewGuid().ToString();
            review.dateReviewed = DateTime.Now;
            _reviewService.AddReview(review);
            return RedirectToAction("BookDetail", "Books", new { bookId });
        }

        [HttpGet("get")]
        public IActionResult GetReviews()
        {
            var reviews = _reviewService.GetReviews();
            return Ok(reviews);
        }

        [HttpGet("get/{reviewId}")]
        public async Task<IActionResult> GetReviewById(string reviewId)
        {
            var review = await _reviewService.GetReviewsById(reviewId);
            
            if(review == null)
            {
                return NoContent();
            }

            return Ok(review);
        }

        [HttpPut("update")]
        public IActionResult UpdateReview(ReviewViewModel review)
        {
           _reviewService.UpdateReview(review);
           return NoContent();
        }

        [HttpDelete("delete/{reviewId}")]
        public IActionResult DeleteReview(string reviewId)
        {
            _reviewService.DeleteReview(reviewId);
            return NoContent();
        }
    }
}
