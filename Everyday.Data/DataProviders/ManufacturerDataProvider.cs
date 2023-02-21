using Everyday.Data.Interfaces;
using Everyday.Data.Utilities;
using Everyday.Domain.Interfaces;
using Everyday.Domain.Models;
using Everyday.Domain.Shared;

namespace Everyday.Data.DataProviders
{
    public class ManufacturerDataProvider : IDataProvider<Manufacturer>
    {
        #region Fields & Properties
        private readonly IHttpClientService http;
        #endregion

        #region CTOR
        public ManufacturerDataProvider(IHttpClientService http)
        {
            this.http = http;
        }
        #endregion

        #region READ
        public async Task<IEnumerable<Manufacturer>?> GetAllAsync()
        {
            return await http.Create($"Manufacturers/manufacturers").GetCallToListAsync<Manufacturer>();
        }

        public async Task<Manufacturer?> GetByIdAsync(int id)
        {
            return await http.Create($"Manufacturers/{id}/manufacturer").GetCallToObjectAsync<Manufacturer>();
        }

        public async Task<Manufacturer?> GetByMemberAsync<TSource, TMember>(Func<TSource, TMember> memberSelector)
        {
            return await http.Create($"Manufacturers/manufacturer{memberSelector.ToQueryString()}").GetCallToObjectAsync<Manufacturer>();
        }
        #endregion

        #region CREATE
        public async Task<IConveyOperationResult> CreateAsync(Manufacturer newDatum)
        {
            IConveyOperationResult? res = null;

            HttpResponseMessage? response = await http.Create($"Manufacturers/manufacturer").PostCallAsync(newDatum);

            if (response is not null)
            {
                res = await response!.DeserializeContent<OperationResult>();
            }

            return res ?? new OperationResult();
        }
        #endregion

        #region UPDATE
        public async Task<IConveyOperationResult> UpdateAsync(Manufacturer updatedDatum)
        {
            IConveyOperationResult? res = null;

            HttpResponseMessage? response = await http.Create($"Manufacturers/manufacturer").PutCallAsync(updatedDatum);

            if (response is not null)
            {
                res = await response.DeserializeContent<OperationResult>();
            }

            return res ?? new OperationResult();
        }
        #endregion

        #region DELETE
        public async Task<IConveyOperationResult> DeleteByIdAsync(int id)
        {
            IConveyOperationResult? res = null;

            HttpResponseMessage? response = await http.Create($"Manufacturers/{id}/manufacturer").DeleteCallAsync();

            if (response is not null)
            {
                res = await response.DeserializeContent<OperationResult>();
            }

            return res ?? new OperationResult();
        }

        public async Task<IConveyOperationResult> DeleteByMemberAsync<TSource, TMember>(Func<TSource, TMember> memberSelector)
        {
            IConveyOperationResult? res = null;

            HttpResponseMessage? response = await http.Create($"Manufacturers/manufacturer{memberSelector.ToQueryString()}").DeleteCallAsync();

            if (response is not null)
            {
                res = await response.DeserializeContent<OperationResult>();
            }

            return res ?? new OperationResult();
        }
        #endregion
    }
}
