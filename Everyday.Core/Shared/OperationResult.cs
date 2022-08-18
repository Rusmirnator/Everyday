namespace Everyday.Core.Shared
{
    public class OperationResult : DataTransferObject
    {
        public OperationResult()
        {
            StatusCode = -1;
            Message = "Response was null!";
        }

        public OperationResult(HttpResponseMessage response)
        {
            StatusCode = response.IsSuccessStatusCode ? 0 : 1;
            Message = response.ReasonPhrase;
        }
    }
}
