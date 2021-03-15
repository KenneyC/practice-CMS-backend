using System.Linq;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using practice_CMS_backend.CMS;
namespace practice_CMS_backend.Authentication
{
    public class UserAccessDataProvider : IUserDataAccessProvider
    {
        private readonly UserManager<User> _userManager;

        public UserAccessDataProvider(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Response> Register(RegisterModel registerModel)
        {
            User UserFound = await _userManager.FindByNameAsync(registerModel.Username);

            if (UserFound != null) return new Response
            {
                Status = "Error",
                Message = "An account already exists with the user name entered"
            };

            User newUser = new User()
            {
                UserName = registerModel.Username,
                Email = registerModel.Email
            };

            IdentityResult result = await _userManager.CreateAsync(newUser, registerModel.Password);

            if (!result.Succeeded) return new Response
            {
                Status = "Error",
                Message = "Error occured when creating an account"
            };

            return new Response
            {
                Status = "Success",
                Message = "User created successfully!"
            };
        }

        public async Task<bool> Login(LoginModel login)
        {
            User userFound = await _userManager.FindByNameAsync(login.Username);

            if (userFound != null && await _userManager.CheckPasswordAsync(userFound, login.Password))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}