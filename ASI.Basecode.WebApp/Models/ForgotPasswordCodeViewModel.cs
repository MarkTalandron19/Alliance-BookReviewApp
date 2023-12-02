using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ASI.Basecode.WebApp.Models
{
    public class ForgotPasswordCodeViewModel
    {
        [JsonPropertyName("code")]
        [Required(ErrorMessage = "Code is required.")]
        public string Code { get; set; }

        [JsonPropertyName("token")]
        [Required(ErrorMessage = "Token is required.")]
        public string Token { get; set; }

        public string Email { get; set; }
        public string OriginalToken { get; set; }
    }
}
