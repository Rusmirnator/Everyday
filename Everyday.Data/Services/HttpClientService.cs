using Everyday.Data.Interfaces;
using Everyday.Data.Utilities;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Http.Headers;

namespace Everyday.Data.Services
{
    public class HttpClientService : IHttpClientService
    {
        #region Fields && properties
        private readonly string API_BASE_URI;
        private string? Endpoint;
        private readonly int Timeout;
        private static readonly HttpClient Client = new();
        private readonly IConfiguration configuration;
        #endregion

        #region  CTOR
        public HttpClientService(IConfiguration configuration)
        {
            API_BASE_URI = configuration["Https:API_BASE_URI"];
            Timeout = 900;
            this.configuration = configuration;
        }
        #endregion

        #region Authorization
        public void AuthorizeBearer(string accessToken)
        {
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }
        #endregion

        #region Requests
        public IHttpClientService Create(string endpoint)
        {
            Endpoint = endpoint;
            return this;
        }

        public async Task<HttpResponseMessage> GetCallAsync()
        {
            HttpResponseMessage res = new(HttpStatusCode.NoContent);

            ConfigureClient();

            try
            {
                return await Client.SendAsync(CreateRequest(HttpMethod.Get, API_BASE_URI + Endpoint).PrepareHeaders());
            }
            catch (Exception ex)
            {
                ThrowErrorMessage(ex.Message, ex?.InnerException?.Message);
            }

            return res;
        }

        public async Task<HttpResponseMessage> PostCallAsync<T>(T model) where T : class
        {
            HttpResponseMessage res = new(HttpStatusCode.NoContent);

            ConfigureClient();

            try
            {
                res = await Client.SendAsync(CreateRequest(HttpMethod.Post, API_BASE_URI + Endpoint).PrepareHeaders().PrepareContent(model));
            }
            catch (Exception ex)
            {
                ThrowErrorMessage(ex.Message, ex?.InnerException?.Message);
            }

            return res;
        }

        public async Task<HttpResponseMessage> PostCallAsync()
        {
            HttpResponseMessage res = new(HttpStatusCode.NoContent);

            ConfigureClient();

            try
            {
                res = await Client.SendAsync(CreateRequest(HttpMethod.Post, API_BASE_URI + Endpoint).PrepareHeaders());
            }
            catch (Exception ex)
            {
                ThrowErrorMessage(ex.Message, ex?.InnerException?.Message);
            }

            return res;
        }

        public async Task<HttpResponseMessage> PutCallAsync<T>(T model) where T : class
        {
            HttpResponseMessage res = new(HttpStatusCode.NoContent);

            ConfigureClient();

            try
            {
                res = await Client.SendAsync(CreateRequest(HttpMethod.Put, API_BASE_URI + Endpoint).PrepareHeaders().PrepareContent(model));
            }
            catch (Exception ex)
            {
                ThrowErrorMessage(ex.Message, ex?.InnerException?.Message);
            }

            return res;
        }

        public async Task<HttpResponseMessage> PutCallAsync()
        {
            HttpResponseMessage res = new(HttpStatusCode.NoContent);

            ConfigureClient();

            try
            {
                return await Client.SendAsync(CreateRequest(HttpMethod.Put, API_BASE_URI + Endpoint).PrepareHeaders());
            }
            catch (Exception ex)
            {
                ThrowErrorMessage(ex.Message, ex?.InnerException?.Message);
            }

            return res;
        }

        public async Task<HttpResponseMessage> DeleteCallAsync()
        {
            HttpResponseMessage res = new(HttpStatusCode.NoContent);

            ConfigureClient();

            try
            {
                res = await Client.SendAsync(CreateRequest(HttpMethod.Delete, API_BASE_URI + Endpoint).PrepareHeaders());
            }
            catch (Exception ex)
            {
                ThrowErrorMessage(ex.Message, ex?.InnerException?.Message);
            }

            return res;
        }

        private static HttpRequestMessage CreateRequest(HttpMethod method, string fullUri)
        {
            return new HttpRequestMessage(method, fullUri);
        }
        #endregion

        #region JSONConvert
        private static async Task<T?> JSONConvertToObject<T>(HttpResponseMessage? msg)
        {
            T? dto = default(T);

            if (msg is null)
            {
                return dto;
            }

            if (HandleUnsuccessfulResponse(msg))
            {
                return dto;
            }

            try
            {
                dto = await msg.DeserializeContent<T>();
            }
            catch (Exception ex)
            {
                ThrowErrorMessage(ex.Message, ex?.InnerException?.Message);
            }

            return dto;
        }

        private static async Task<List<T>?> JSONConvertToList<T>(HttpResponseMessage? msg)
        {
            List<T>? res = new();

            if (msg is null)
            {
                return res;
            }

            if (HandleUnsuccessfulResponse(msg))
            {
                return res;
            }

            try
            {
                res = await msg.DeserializeContent<List<T>>();
            }
            catch (Exception ex)
            {
                ThrowErrorMessage(ex.Message, ex?.InnerException?.Message);
            }

            return res;
        }
        #endregion

        #region ObjectMapping
        public async Task<List<T>?> GetCallToListAsync<T>()
        {
            return await JSONConvertToList<T>(await GetCallAsync());
        }

        public async Task<T?> GetCallToObjectAsync<T>()
        {
            return await JSONConvertToObject<T>(await GetCallAsync());
        }
        #endregion

        #region ErrorHandling
        private static bool HandleUnsuccessfulResponse(HttpResponseMessage msg)
        {
            switch (msg.StatusCode)
            {
                case HttpStatusCode.InternalServerError:

                    ThrowErrorMessage("Internal server error", msg.ReasonPhrase);
                    return true;

                case HttpStatusCode.NotImplemented:

                    ThrowErrorMessage("Not implemented", msg.ReasonPhrase);
                    return true;

                case HttpStatusCode.BadGateway:

                    ThrowErrorMessage("Bad gateway", msg.ReasonPhrase);
                    return true;

                case HttpStatusCode.ServiceUnavailable:

                    ThrowErrorMessage("Service unavailable", msg.ReasonPhrase);
                    return true;

                case HttpStatusCode.GatewayTimeout:

                    ThrowErrorMessage("Connection timeout", msg.ReasonPhrase);
                    return true;

                case HttpStatusCode.BadRequest:

                    ThrowErrorMessage("Bad request", msg.ReasonPhrase);
                    return true;

                case HttpStatusCode.Unauthorized:

                    ThrowErrorMessage("Unauthorized", msg.ReasonPhrase);
                    return true;

                case HttpStatusCode.Forbidden:

                    ThrowErrorMessage("Forbidden", msg.ReasonPhrase);
                    return true;

                case HttpStatusCode.NotFound:

                    ThrowErrorMessage("Not found", msg.ReasonPhrase);
                    return true;
            }

            return false;
        }
        private static void ThrowErrorMessage(string msg, string? innerException = "")
        {
            System.Diagnostics.Debug.WriteLine(msg);
        }
        #endregion

        #region Preconfiguration
        private void ConfigureClient()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            if (Client.Timeout == TimeSpan.Zero)
            {
                Client.Timeout = TimeSpan.FromSeconds(Timeout);
            }
        }
        #endregion
    }
}

