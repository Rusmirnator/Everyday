using Everyday.Application.Common.Models;
using Everyday.Domain.Models;

namespace Everyday.Application.Common.Interfaces.Services
{
    public interface IIdentityService
    {
        public Task<UserResponseModel> LoginAsync(LoginRequestModel loginRequest);
        public JsonWebToken GetToken();
        public string GetEncodedToken();
    }
}
