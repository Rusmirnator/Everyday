namespace Everyday.Application.Common.Interfaces.Services
{
    public interface IHttpService
    {
        public Task<HttpResponseMessage> GetAsync(string endpoint);
        public Task<HttpResponseMessage> PostAsync<T>(string endpoint, T requestModel) where T : class;
        public Task<HttpResponseMessage> PutAsync<T>(string endpoint, T requestModel) where T : class;
        public Task<HttpResponseMessage> PatchAsync<T>(string endpoint, T requestModel) where T : class;
        public Task<HttpResponseMessage> DeleteAsync(string endpoint);
    }
}