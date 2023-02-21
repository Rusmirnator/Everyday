using Everyday.Domain.Shared;
using System.ComponentModel.DataAnnotations;

namespace Everyday.Domain.Models
{
    public class Item : DataTransferObject
    {
        #region Fields & Properties
        public int? Id { get; set; }
        [Required]
        public string? Code { get; set; }
        [Required]
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double? Width { get; set; }
        public double? Height { get; set; }
        public double? Depth { get; set; }
        public double? Weight { get; set; }
        public double? Price { get; set; }
        public ItemDefinition? ItemDefinition { get; set; }
        public Manufacturer? Manufacturer { get; set; }
        #endregion
    }
}
