using System.Text.Json.Serialization;

namespace Everyday.Core.Interfaces
{
    public interface IConveyOperationResult
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public object? Result { get; set; }
    }
}
