﻿using ASI.Basecode.Data.Models;
using System.Collections.Generic;

namespace ASI.Basecode.WebApp.Models
{
    public class HomeViewModel
    {
        public List<Genre> Genres { get; set; }

        public List<int> Years { get; set; }

        public List<Book> SearchResults { get; set; }
    }
}
