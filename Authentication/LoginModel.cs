using System.ComponentModel.DataAnnotations;

namespace practice_CMS_backend.Authentication
{
    public class LoginModel
    {

        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        public string Password { get; set; }
    }
}