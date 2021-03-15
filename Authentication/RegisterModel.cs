using System.ComponentModel.DataAnnotations;

namespace practice_CMS_backend.Authentication
{
    public class RegisterModel
    {

        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        public string Password { get; set; }
    }
}