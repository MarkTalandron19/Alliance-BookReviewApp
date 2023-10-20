using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ASI.Basecode.Services.ServiceModels
{
    public class GenreViewModel
    {
        [JsonPropertyName("genreId")]
        [Required(ErrorMessage = "Genre Id is required.")]
        public string genreId { get; set; }
        [JsonPropertyName("genreName")]
        [Required(ErrorMessage = "Genre name is required.")]
        public string genreName { get; set; }
        [JsonPropertyName("description")]
        [Required(ErrorMessage = "Genre description is required.")]
        public string description { get; set; }
    }
}
