using Everyday.Application.Common.Interfaces.Structures;
using Models = Everyday.Domain.Models;

namespace Everyday.Application.Consumable.Interfaces
{
    public interface IConsumableService
    {
        public Task<IEnumerable<Models.Consumable>> GetConsumablesAsync();
        public Task<Models.Consumable?> GetConsumableByItemId(int itemId);
        public Task<IOperationResult> CreateConsumableAsync(Models.Consumable newDatum);
        public Task<IOperationResult> UpdateConsumableAsync(Models.Consumable updatedDatum);
        public Task<IOperationResult> DeleteConsumableAsync(int id);
    }
}
