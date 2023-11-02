using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASI.Basecode.Data.Models
{
    public partial class Comment
    {
        public string commentId { get; set; }
        public string commenterFirstName { get; set; }
        public string commenterLastName { get; set; }
        public string comment { get; set; }
        public DateTime dateCommented { get; set; }
        public string reviewId { get; set; }
        public Review review { get; set; }
    }
}
