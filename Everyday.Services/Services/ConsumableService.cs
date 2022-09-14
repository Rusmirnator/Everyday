using Everyday.Core.Interfaces;
using Everyday.Core.Models;
using Everyday.Data.Interfaces;
using Everyday.Services.Interfaces;

namespace Everyday.Services.Services
{
    public class ConsumableService : IConsumableService
    {
        #region Fields & Properties
        private readonly IDataProvider<Consumable> dataProvider;
        #endregion

        #region CTOR
        public ConsumableService(IDataProvider<Consumable> dataProvider)
        {
            this.dataProvider = dataProvider;
        }
        #endregion

        #region READ
        public async Task<IEnumerable<Consumable>> GetConsumablesAsync()
        {
            return await dataProvider.GetAllAsync() ?? Enumerable.Empty<Consumable>();
        }

        public async Task<Consumable?> GetConsumableByItemId(int itemId)
        {
            return await dataProvider.GetByIdAsync(itemId);
        }
        #endregion

        #region CREATE
        public async Task<IConveyOperationResult> CreateConsumableAsync(Consumable newDatum)
        {
            return await dataProvider.CreateAsync(newDatum);
        }
        #endregion

        #region UPDATE
        public async Task<IConveyOperationResult> UpdateConsumableAsync(Consumable updatedDatum)
        {
            return await dataProvider.UpdateAsync(updatedDatum);
        }
        #endregion

        #region DELETE
        public async Task<IConveyOperationResult> DeleteConsumableAsync(int id)
        {
            return await dataProvider.DeleteByIdAsync(id);
        }
        #endregion
    }
}
