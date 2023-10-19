using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ASI.Basecode.Services.ServiceModels
{
    public class ReviewViewModel
    {
        [JsonPropertyName("reviewId")]
        [Required(ErrorMessage = "Review Id is required.")]
        public string reviewId { get; set; }
        [JsonPropertyName("reviewerFirstName")]
        [Required(ErrorMessage = "Reviewer first name is required.")]
        public string reviewerFirstName { get; set; }
        [JsonPropertyName("reviewerLastName")]
        [Required(ErrorMessage = "Reviewer last name is required.")]
        public string reviewerLastName { get; set; }
        [JsonPropertyName("reviewerEmail")]
        [Required(ErrorMessage = "Reviewer email is required.")]
        public string reviewerEmail { get; set; }
        [JsonPropertyName("content")]
        [Required(ErrorMessage = "Review content is required.")]
        public string content { get; set; }
        [JsonPropertyName("rating")]
        [Required(ErrorMessage = "Rating is content"), Range(0, 5)]
        public int rating { get; set; }
        [JsonPropertyName("bookId")]
        [Required(ErrorMessage = "Book Id is required.")]
        public string bookId { get; set; }
        [JsonPropertyName("dateReviewed")]
        [Required(ErrorMessage = "Review date is required.")]
        public DateTime dateReviewed { get; set; }
    }
}
