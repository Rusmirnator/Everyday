namespace Everyday.Application.Common.Interfaces.Structures
{
    public interface IOperationResult
    {
        public bool IsSuccessful { get; }
        public string Message { get; }
    }
}
