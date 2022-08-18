using Everyday.Core.Interfaces;

namespace Everyday.Core.Shared
{
    public class DataTransferObject : IConveyOperationResult
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public object? Result { get; set; }

        public DataTransferObject()
        {
            Result = this;
        }
    }
}
