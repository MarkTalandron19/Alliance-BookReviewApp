using ASI.Basecode.Data.Models;
using ASI.Basecode.Services.ServiceModels;
using System.Collections.Generic;

namespace ASI.Basecode.WebApp.Models
{
	public class BookViewStorageModel
	{
		public BookViewModel ViewModel { get; set; }
		public List<Book> Books { get; set; }
	}
}
