namespace Everyday.Domain.Attributes
{
    /// <summary>
    /// Provides info for BindableBase to fetch marked method and create BindableAsyncCommand
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class AsyncCommandAttribute : CommandAttribute
    {
        public bool UseCommandManager { get; private set; }
        public bool AllowMultipleExecution { get; private set; }

        public AsyncCommandAttribute(bool useCommandManager = true, bool allowMultipleExecution = false) : base()
        {
            UseCommandManager = useCommandManager;
            AllowMultipleExecution = allowMultipleExecution;
        }
    }
}
