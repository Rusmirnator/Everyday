using Everyday.Application.Common.Interfaces.Structures;
using Models = Everyday.Domain.Models;

namespace Everyday.Application.Item.Interfaces
{
    public interface IItemService
    {
        public Task<IEnumerable<Models.Item>> GetItemsAsync();
        public Task<Models.Item?> GetItemByIdAsync(int id);
        public Task<Models.Item?> GetItemByCodeAsync(string code);
        public Task<IOperationResult> CreateItemAsync(Models.Item newItem);
        public Task<IOperationResult> UpdateItemAsync(Models.Item updatedItem);
        public Task<IOperationResult> DeleteItemAsync(int id);
        public Task<IOperationResult> DeleteItemAsync(string code);
    }
}
