using Everyday.Core.Interfaces;
using Everyday.Core.Models;

namespace Everyday.Services.Interfaces
{
    public interface IConsumableService
    {
        public Task<IEnumerable<Consumable>> GetConsumablesAsync();
        public Task<Consumable?> GetConsumableByItemId(int itemId);
        public Task<IConveyOperationResult> CreateConsumableAsync(Consumable newDatum);
        public Task<IConveyOperationResult> UpdateConsumableAsync(Consumable updatedDatum);
        public Task<IConveyOperationResult> DeleteConsumableAsync(int id);
    }
}
