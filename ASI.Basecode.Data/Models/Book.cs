using System;

namespace ASI.Basecode.Data.Models
{
    public partial class Book
    {   
        public string bookId { get; set; }
        public string title { get; set; }
        public string synopsis { get; set; }
        public DateTime pubYear { get; set; }
        public string publisher { get; set; }
        public string IBSN { get; set; }
        public string language { get; set; }    
        public string CreatedBy { get; set; }
        public DateTime CreatedTime { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedTime { get; set; }
    }
}
