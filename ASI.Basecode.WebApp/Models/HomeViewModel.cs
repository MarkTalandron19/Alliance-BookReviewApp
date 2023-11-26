using ASI.Basecode.Data.Models;
using System.Collections.Generic;

namespace ASI.Basecode.WebApp.Models
{
    public class HomeViewModel
    {
        public HomeViewModel(List<Book> NewlyReleasedBooks, List<BookAverageRating> TopRatedBooks) 
        {
            this.TopRatedBooks = TopRatedBooks;
            this.NewlyReleasedBooks = NewlyReleasedBooks;
            
        }

        public List<Book> NewlyReleasedBooks { get; set; }
        public List<BookAverageRating> TopRatedBooks { get; set; }
    }
}
