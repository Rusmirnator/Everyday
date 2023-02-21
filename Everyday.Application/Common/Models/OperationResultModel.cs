using Everyday.Application.Common.Interfaces.Structures;

namespace Everyday.Application.Common.Models
{
    public class OperationResultModel : IOperationResult
    {
        public bool IsSuccessful { get; }
        public string Message { get; }

        public OperationResultModel(bool isSuccessful, string message)
        {
            IsSuccessful = isSuccessful;
            Message = message;
        }
    }
}