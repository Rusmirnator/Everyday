using Everyday.Application.Common.Interfaces.Services;
using Everyday.Application.Common.Interfaces.Structures;
using System.Net;

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
            try
            {
                return await Client!.SendAsync(CreateRequest(HttpMethod.Post, endpoint).PrepareHeaders().PrepareContent(requestModel));
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Application encountered a connection problem", ex);
            }
        }

        public async Task<HttpResponseMessage> PutAsync<T>(string endpoint, T requestModel) where T : class
        {
            try
            {
                return await Client!.SendAsync(CreateRequest(HttpMethod.Put, endpoint).PrepareHeaders().PrepareContent(requestModel));
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Application encountered a connection problem", ex);
            }
        }

        public async Task<HttpResponseMessage> PatchAsync<T>(string endpoint, T requestModel) where T : class
        {
            try
            {
                return await Client!.SendAsync(CreateRequest(HttpMethod.Patch, endpoint).PrepareHeaders().PrepareContent(requestModel));
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Application encountered a connection problem", ex);
            }
        }

        public async Task<HttpResponseMessage> DeleteAsync(string endpoint)
        {
            try
            {
                return await Client!.SendAsync(CreateRequest(HttpMethod.Delete, endpoint).PrepareHeaders());
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Application encountered a connection problem", ex);
            }
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
        #endregion
    }
}
