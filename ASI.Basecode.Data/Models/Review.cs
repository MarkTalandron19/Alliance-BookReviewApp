using System;

namespace ASI.Basecode.Data.Models
{
    public partial class Review
    {
        public string reviewId { get; set; }
        public string reviewerFirstName { get; set; }
        public string reviewerLastName { get; set; }
        public string reviewerEmail { get; set; }
        public string content { get; set; } 
        public int rating { get; set; }
        public string bookId { get; set; }
        public DateTime dateReviewed { get; set; }
    }
}
