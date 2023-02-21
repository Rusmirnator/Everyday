using Everyday.Application.Common.Interfaces.Services;
using Everyday.Application.Common.Interfaces.Structures;

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
        public Task<HttpResponseMessage> DeleteAsync<T>()
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> GetAsync<T>()
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> PatchAsync<T>()
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> PostAsync<T>()
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> PutAsync<T>()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Private API
        private void ConfigureHttpClient()
        {
            Client.DefaultRequestHeaders.Clear();

            if (Configuration is not null)
            {
                Client.Timeout = Configuration.Timeout;
                Client.BaseAddress = Configuration.BaseAddress;
            }
        }
        #endregion
    }
}
