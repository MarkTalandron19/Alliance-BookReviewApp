using ASI.Basecode.Data.Models;
using ASI.Basecode.Services.Interfaces;
using ASI.Basecode.Services.ServiceModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
        public IActionResult AddReview(ReviewViewModel review)
        {
            _reviewService.AddReview(review);
            return NoContent();
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
