using Everyday.Application.Common.Interfaces.Services;
using Everyday.Application.Common.Interfaces.Structures;
using System.Net;
using System.Reflection;

namespace Everyday.Infrastructure.Common.DataAccess
{
    public class HttpService : IHttpService, IOptions<HttpClientConfiguration>
    {
        #region Fields & Properties
        public HttpClient Client { get; }
        public HttpClientConfiguration? Configuration { get; set; }
        #endregion

        #region CTOR
        public HttpService()
        {
            Client = new HttpClient();
            ConfigureHttpClient();
        }
        #endregion

        #region Public API
        public async Task<HttpResponseMessage> GetAsync(string endpoint)
        {
            try
            {
                return await Client.SendAsync(CreateRequest(HttpMethod.Get, endpoint).PrepareHeaders());
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Application encountered a connection problem", ex);
            }
        }

        public async Task<HttpResponseMessage> PostAsync<T>(string endpoint, T requestModel) where T : class
        {
            HttpResponseMessage? res = null;

            try
            {
                res = await Client!.SendAsync(CreateRequest(HttpMethod.Post, endpoint).PrepareHeaders().PrepareContent(requestModel));
            }
            catch (Exception ex)
            {
                ThrowErrorMessage(ex.Message, ex?.InnerException?.Message);
            }

            return res;
        }

        public async Task<HttpResponseMessage> PutAsync<T>(string endpoint, T requestModel) where T : class
        {
            HttpResponseMessage? res = null;

            try
            {
                res = await Client!.SendAsync(CreateRequest(HttpMethod.Put, endpoint).PrepareHeaders().PrepareContent(requestModel));
            }
            catch (Exception ex)
            {
                ThrowErrorMessage(ex.Message, ex?.InnerException?.Message);
            }

            return res;
        }

        public async Task<HttpResponseMessage> PatchAsync<T>(string endpoint, T requestModel) where T : class
        {
            HttpResponseMessage? res = null;

            try
            {
                res = await Client!.SendAsync(CreateRequest(HttpMethod.Patch, endpoint).PrepareHeaders().PrepareContent(requestModel));
            }
            catch (Exception ex)
            {
                ThrowErrorMessage(ex.Message, ex?.InnerException?.Message);
            }

            return res;
        }

        public async Task<HttpResponseMessage> DeleteAsync(string endpoint)
        {
            HttpResponseMessage? res = null;

            try
            {
                res = await Client!.SendAsync(CreateRequest(HttpMethod.Delete, endpoint).PrepareHeaders());
            }
            catch (Exception ex)
            {
                ThrowErrorMessage(ex.Message, ex?.InnerException?.Message);
            }

            return res;
        }
        #endregion

        #region Private API
        private void ConfigureHttpClient()
        {
            if (Configuration is not null)
            {
                Client.Timeout = Configuration.Timeout;
                Client.BaseAddress = Configuration.BaseAddress;
            }
        }

        private HttpRequestMessage CreateRequest(HttpMethod method, string endpoint)
        {
            return new HttpRequestMessage(method, Configuration?.BaseAddress?.AbsoluteUri + endpoint);
        }

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
    }
}
