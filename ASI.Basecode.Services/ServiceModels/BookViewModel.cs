using ASI.Basecode.Data.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ASI.Basecode.Services.ServiceModels
{
    public class BookViewModel
    {
        [JsonPropertyName("bookId")]
        [Required(ErrorMessage = "Book Id is required.")]
        public string bookId { get; set; }

        [JsonPropertyName("title")]
        [Required(ErrorMessage = "Book title is required.")]
        public string title { get; set; }

        [JsonPropertyName("synopsis")]
        [Required(ErrorMessage = "Book synopsis is required.")]
        public string synopsis { get; set; }

        [JsonPropertyName("author")]
        [Required(ErrorMessage = "Book author is required.")]
        public string author { get; set; }

        [JsonPropertyName("pubYear")]
        [Required(ErrorMessage = "Publishing year is required.")]
        public string pubYear { get; set; }

        [JsonPropertyName("publisher")]
        [Required(ErrorMessage = "Publisher is required.")]
        public string publisher { get; set; }

        [JsonPropertyName("isbn")]
        [Required(ErrorMessage = "Book ISBN is required.")]
        public string isbn { get; set; }

        [JsonPropertyName("language")]
        [Required(ErrorMessage = "Book language is required.")]
        public string language { get; set; }

        [JsonPropertyName("genre")]
        [Required(ErrorMessage = "Genre is required.")]
        public string genre { get; set; }

        [JsonPropertyName("image")]
        public IFormFile image { get; set; }

        public string ExistingImage { get; set; }

    }
}
