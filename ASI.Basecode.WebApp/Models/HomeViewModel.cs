using ASI.Basecode.Data.Models;
using System;
using System.Collections.Generic;

namespace ASI.Basecode.WebApp.Models
{
    public class HomeViewModel
    {
        public HomeViewModel(List<Book> NewlyReleasedBooks, List<BookAverageRating> TopRatedBooks, List<Genre> Genres, List<int> Years) 
        {
            this.TopRatedBooks = TopRatedBooks;
            this.NewlyReleasedBooks = NewlyReleasedBooks;
            this.Genres = Genres;
            this.Years = Years;
        }

        public List<Book> NewlyReleasedBooks { get; set; }
        public List<BookAverageRating> TopRatedBooks { get; set; }

        public List<Genre> Genres { get; set; }

        public List<int> Years { get; set; }

        public List<Book> SearchResults { get; set; }
    }
}
