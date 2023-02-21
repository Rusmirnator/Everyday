using Everyday.Application.Common.Interfaces.Structures;
using Models = Everyday.Domain.Models;

namespace Everyday.Application.Manufacturer.Interfaces
{
    public interface IManufacturerService
    {
        public Task<IEnumerable<Models.Manufacturer>> GetManufacturersAsync();
        public Task<Models.Manufacturer?> GetManufacturerByIdAsync(int id);
        public Task<Models.Manufacturer?> GetManufacturerByNameAsync(string name);
        public Task<IOperationResult> CreateManufacturerAsync(Models.Manufacturer newManufacturer);
        public Task<IOperationResult> UpdateManufacturerAsync(Models.Manufacturer updatedManufacturer);
        public Task<IOperationResult> DeleteManufacturerAsync(int id);
        public Task<IOperationResult> DeleteManufacturerAsync(string name);
    }
}
