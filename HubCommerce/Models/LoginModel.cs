using System.ComponentModel.DataAnnotations;

namespace HubCommerce.Models
{
    public class LoginModel
    {
        [EmailAddress]
        [Required(ErrorMessage = "Please fill your email adress")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please fill your password")]
        public string Password { get; set; } = string.Empty;
    }
}
