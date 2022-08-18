using Everyday.Core.Interfaces;
using Everyday.Core.Models;

namespace Everyday.Services.Interfaces
{
    public interface IItemService
    {
        public Task<IEnumerable<Item>> GetItemsAsync();
        public Task<Item?> GetItemByIdAsync(int id);
        public Task<Item?> GetItemByCodeAsync(string code);
        public Task<IConveyOperationResult> CreateItemAsync(Item newItem);
        public Task<IConveyOperationResult> UpdateItemAsync(Item updatedItem);
        public Task<IConveyOperationResult> DeleteItemAsync(int id);
        public Task<IConveyOperationResult> DeleteItemAsync(string code);
    }
}
