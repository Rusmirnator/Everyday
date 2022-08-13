using Everyday.Core.Interfaces;
using Everyday.Core.Models;
using Everyday.Data.Interfaces;
using Everyday.Data.Utilities;

namespace Everyday.Data.DataProviders
{
    public class UserDataProvider : IUserDataProvider
    {
        private readonly IHttpClientService httpClient;

        public UserDataProvider(IHttpClientService httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IConveyOperationResult> GetTokenAsync(string login, string pasword)
        {
            JsonWebToken authorizationResult;

            HttpResponseMessage? response =
                await httpClient
                        .Create($"Home/login?login={login}&password={pasword}")
                            .PostCallAsync();

            if (response is null)
            {
                authorizationResult = new(null)
                {
                    StatusCode = -1,
                    Message = "Response was null - this may indicate connection problem!"
                };

                return authorizationResult;
            }

            if (!response!.IsSuccessStatusCode)
            {
                authorizationResult = new(null)
                {
                    StatusCode = 1,
                    Message = await response!.ReadResponseContentAsync()
                };
                return authorizationResult;
            }

            authorizationResult = new(await response!.ReadResponseContentAsync())
            {
                StatusCode = 0,
                Message = "Successfuly authorized!"
            };

            httpClient.AuthorizeBearer(authorizationResult.EncodedToken!);

            return authorizationResult;
        }
    }
}
