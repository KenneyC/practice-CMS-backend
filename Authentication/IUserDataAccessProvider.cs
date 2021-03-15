using System.Threading.Tasks;
using practice_CMS_backend.CMS;

namespace practice_CMS_backend.Authentication
{
    public interface IUserDataAccessProvider
    {
        public Task<Response> Register(RegisterModel registerModel);

        public Task<bool> Login(LoginModel login);

    }
}