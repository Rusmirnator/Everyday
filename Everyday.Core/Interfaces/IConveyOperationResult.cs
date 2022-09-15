using System.Text.Json.Serialization;

namespace Everyday.Core.Interfaces
{
    public interface IConveyOperationResult
    {
        [JsonIgnore]
        public int StatusCode { get; set; }
        [JsonIgnore]
        public string? Message { get; set; }
        [JsonIgnore]
        public object? Result { get; set; }
    }
}
