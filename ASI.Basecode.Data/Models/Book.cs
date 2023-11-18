using System;
using System.Collections.Generic;

namespace ASI.Basecode.Data.Models
{
   public struct BookAverageRating
    {
        public BookAverageRating(Book book, float AvgRating)
        {
            Book = book;
            AverageRating = AvgRating;
        }

        public Book Book { get; set; }
        public float AverageRating { get; set; }
    }

    public partial class Book
    {   
        public string bookId { get; set; }
        public string title { get; set; }
        public string synopsis { get; set; }
        public string author { get; set; }
        public string pubYear { get; set; }
        public string publisher { get; set; }
        public string isbn { get; set; }
        public string language { get; set; }
        public string image { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedTime { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedTime { get; set; }
        public ICollection<BookGenres> BookGenres { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}
