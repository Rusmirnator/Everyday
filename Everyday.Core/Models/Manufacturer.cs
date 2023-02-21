using Everyday.Domain.Shared;

namespace Everyday.Domain.Models
{
    public class Manufacturer : DataTransferObject
    {
        #region Fields & Properties
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        #endregion
    }
}
