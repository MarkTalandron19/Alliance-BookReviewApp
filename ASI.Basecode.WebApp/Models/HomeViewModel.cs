using ASI.Basecode.Data.Models;
using System.Collections.Generic;

namespace ASI.Basecode.WebApp.Models
{
	public class HomeViewModel
	{
		public List<Book> NewlyReleasedBooks { get; set; }
		public List<Book> TopRatedBooks { get; set; }
	}
}
