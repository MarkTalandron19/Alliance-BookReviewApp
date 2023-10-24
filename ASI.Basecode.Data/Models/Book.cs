using System;

namespace ASI.Basecode.Data.Models
{
    public partial class Book
    {   
        public string bookId { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public DateTime pubYear { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedTime { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedTime { get; set; }
    }
}
