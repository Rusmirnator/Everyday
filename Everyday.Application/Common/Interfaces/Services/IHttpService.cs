using Everyday.Application.Common.Interfaces.Structures;

namespace Everyday.Application.Common.Interfaces.Services
{
    public interface IHttpService : IOptions
    {
        public Task<HttpResponseMessage> GetAsync<T>();
        public Task<HttpResponseMessage> PostAsync<T>();
        public Task<HttpResponseMessage> PutAsync<T>();
        public Task<HttpResponseMessage> PatchAsync<T>();
        public Task<HttpResponseMessage> DeleteAsync<T>();
    }
}
