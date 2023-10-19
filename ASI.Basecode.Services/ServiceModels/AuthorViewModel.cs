using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ASI.Basecode.Services.ServiceModels
{
    public class AuthorViewModel
    {
        [JsonPropertyName("authorId")]
        [Required(ErrorMessage = "Author Id is required.")]
        public string authorId { get; set; }
        [JsonPropertyName("authorFirstName")]
        [Required(ErrorMessage = "Author's first name is required.")]
        public string authorFirstName { get; set; }
        [JsonPropertyName("authorLastName")]
        [Required(ErrorMessage = "Author's last name is required")]
        public string authorLastName { get; set; }
    }
}
