using Everyday.Core.Interfaces;
using Everyday.Core.Models;

namespace Everyday.Services.Interfaces
{
    public interface IManufacturerService
    {
        public Task<IEnumerable<Manufacturer>> GetManufacturersAsync();
        public Task<Manufacturer?> GetManufacturerByIdAsync(int id);
        public Task<Manufacturer?> GetManufacturerByNameAsync(string name);
        public Task<IConveyOperationResult> CreateManufacturerAsync(Manufacturer newManufacturer);
        public Task<IConveyOperationResult> UpdateManufacturerAsync(Manufacturer updatedManufacturer);
        public Task<IConveyOperationResult> DeleteManufacturerAsync(int id);
        public Task<IConveyOperationResult> DeleteManufacturerAsync(string name);
    }
}
