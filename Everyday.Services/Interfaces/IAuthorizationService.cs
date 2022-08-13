using Everyday.Core.Interfaces;
using Everyday.Core.Models;

namespace Everyday.Services.Interfaces
{
    public interface IAuthorizationService
    {
        public Task<IConveyOperationResult> AcquireCredentialsAsync(string? login = null, string? password = null);
        public JsonWebToken GetToken();
        public string GetEncodedToken();
    }
}
