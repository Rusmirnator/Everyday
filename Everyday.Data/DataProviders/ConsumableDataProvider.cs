using Everyday.Data.Interfaces;
using Everyday.Data.Utilities;
using Everyday.Domain.Interfaces;
using Everyday.Domain.Models;
using Everyday.Domain.Shared;

namespace Everyday.Data.DataProviders
{
    public class ConsumableDataProvider : IDataProvider<Consumable>
    {
        #region Fields & Properties
        private readonly IHttpClientService http;
        #endregion

        #region CTOR
        public ConsumableDataProvider(IHttpClientService http)
        {
            this.http = http;
        }
        #endregion

        #region READ
        public async Task<IEnumerable<Consumable>?> GetAllAsync()
        {
            return await http.Create($"Consumables/consumables").GetCallToListAsync<Consumable>();
        }

        public async Task<Consumable?> GetByIdAsync(int id)
        {
            return await http.Create($"Consumables/{id}/consumable").GetCallToObjectAsync<Consumable>();
        }

        public async Task<Consumable?> GetByMemberAsync<TSource, TMember>(Func<TSource, TMember> memberSelector)
        {
            return await http.Create($"Consumables/consumable{memberSelector.ToQueryString()}").GetCallToObjectAsync<Consumable>();
        }
        #endregion

        #region CREATE
        public async Task<IConveyOperationResult> CreateAsync(Consumable newDatum)
        {
            IConveyOperationResult? res = null;

            HttpResponseMessage? response = await http.Create($"Consumables/consumable").PostCallAsync(newDatum);

            if (response is not null)
            {
                res = await response!.DeserializeContent<DataTransferObject>();
            }

            return res ?? new OperationResult();
        }
        #endregion

        #region UPDATE
        public async Task<IConveyOperationResult> UpdateAsync(Consumable updatedDatum)
        {
            IConveyOperationResult? res = null;

            HttpResponseMessage? response = await http.Create($"Consumables/consumable").PutCallAsync(updatedDatum);

            if (response is not null)
            {
                res = await response!.DeserializeContent<DataTransferObject>();
            }

            return res ?? new OperationResult();
        }
        #endregion

        #region DELETE
        public async Task<IConveyOperationResult> DeleteByIdAsync(int id)
        {
            IConveyOperationResult? res = null;

            HttpResponseMessage? response = await http.Create($"Consumables/{id}/consumable").DeleteCallAsync();

            if (response is not null)
            {
                res = await response.DeserializeContent<DataTransferObject>();
            }

            return res ?? new OperationResult();
        }

        public async Task<IConveyOperationResult> DeleteByMemberAsync<TSource, TMember>(Func<TSource, TMember> memberSelector)
        {
            IConveyOperationResult? res = null;

            HttpResponseMessage? response = await http.Create($"Consumables/consumable{memberSelector.ToQueryString()}").DeleteCallAsync();

            if (response is not null)
            {
                res = await response.DeserializeContent<DataTransferObject>();
            }

            return res ?? new OperationResult();
        }
        #endregion
    }
}
