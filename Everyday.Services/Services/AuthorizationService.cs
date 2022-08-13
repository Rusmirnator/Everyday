using Everyday.Core.Interfaces;
using Everyday.Core.Models;
using Everyday.Data.Interfaces;
using Everyday.Services.Interfaces;

namespace Everyday.Services.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private JsonWebToken? AcquiredToken;
        private readonly IUserDataProvider dataProvider;
        private string? CachedLogin { get; set; }
        private string? CachedPassword { get; set; }

        public AuthorizationService(IUserDataProvider dataProvider)
        {
            this.dataProvider = dataProvider;
            CachedLogin = string.Empty;
            CachedPassword = string.Empty;
        }

        public async Task<IConveyOperationResult> AcquireCredentialsAsync(string? login = null, string? password = null)
        {
            CachedLogin = login ?? CachedLogin;
            CachedPassword = password ?? CachedPassword;

            IConveyOperationResult res = await dataProvider.GetTokenAsync(CachedLogin!, CachedPassword!);

            AcquiredToken = (JsonWebToken?)res.Result;

            return res;
        }

        public JsonWebToken? GetToken()
        {
            return AcquiredToken;
        }

        public string? GetEncodedToken()
        {
            return AcquiredToken?.EncodedToken;
        }
    }
}
