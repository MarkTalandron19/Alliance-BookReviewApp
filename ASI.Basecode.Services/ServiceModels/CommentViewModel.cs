using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ASI.Basecode.Services.ServiceModels
{
    public class CommentViewModel
    {
        [JsonPropertyName("commentId")]
        [Required(ErrorMessage = "Comment Id is required.")]
        public string commentId { get; set; }

        [JsonPropertyName("commenterFirstName")]
        [Required(ErrorMessage = "Commenter first name is required.")]
        public string commenterFirstName { get; set; }

        [JsonPropertyName("commenterLastName")]
        [Required(ErrorMessage = "Commenter last name is required.")]
        public string commenterLastName { get; set; }

        [JsonPropertyName("comment")]
        [Required(ErrorMessage = "Comment is required.")]
        public string comment { get; set; }

        [JsonPropertyName("reviewId")]
        [Required(ErrorMessage = "Review Id is required.")]
        public string reviewId { get; set; }
    }
}
