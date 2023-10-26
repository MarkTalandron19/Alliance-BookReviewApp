using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASI.Basecode.Data.Models
{
    public partial class BookGenres
    {
        public int Id { get; set; }
        public string bookId { get; set; }
        public string genreId { get; set; }
    }
}
