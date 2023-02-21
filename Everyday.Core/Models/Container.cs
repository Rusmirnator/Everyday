using Everyday.Domain.Shared;

namespace Everyday.Domain.Models
{
    public class Container : DataTransferObject
    {
        #region Fields & Properties
        public int Id { get; set; }
        public int TrashTypeId { get; set; }
        public bool IsReusable { get; set; }
        #endregion
    }
}
