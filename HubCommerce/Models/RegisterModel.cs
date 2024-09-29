using System.ComponentModel.DataAnnotations;

namespace HubCommerce.Models
{
    public class RegisterModel
    {
        [EmailAddress]
        [Required(ErrorMessage ="Please fill your email adress")]
        public string Email { get; set; } = string.Empty;


        [Required(ErrorMessage ="Please fill your password")]
        [MinLength(8,ErrorMessage ="Minimum 8 characters required")]
        [MaxLength(100,ErrorMessage = "Maximum 100 characters required")]
        public string Password { get; set; } = string.Empty;


        [Required(ErrorMessage ="Please fill your password")]
        [Compare("Password", ErrorMessage ="Passwords doesn't match")]
        public string PasswordConfirm {  get; set; } = string.Empty;
    }
}
