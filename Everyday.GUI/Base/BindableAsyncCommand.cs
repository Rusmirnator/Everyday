using Everyday.GUI.Base.Interfaces;
using Everyday.GUI.Utilities;

namespace Everyday.GUI.Base
{
    public class BindableAsyncCommand : BindableCommandBase, IAsyncCommand
    {
        #region Fields & Properties
        private readonly Func<Task> executeAsync;
        private readonly Action<Exception> executeOnException;
        private readonly bool continueOnCapturedContext;
        #endregion

        #region CTOR
        /// <summary>
        /// Initializes a new instance of the BindableAsyncCommand class.
        /// </summary>
        /// <param name="executeAsync">A delegate to be invoked asynchronously by BindableAsyncCommand.</param>
        /// <param name="canExecute">A delegate resolving BindableAsyncCommand execute possibility.</param>
        /// <param name="onException">A delegate to be invoked by BindableAsyncCommand on in-task exception occurrance.</param>
        /// <param name="continueOnCapturedContext">Configures an awaiter used to await BindableAsyncCommand's task.</param>
        public BindableAsyncCommand(Func<Task> executeAsync, Func<bool> canExecute = null, Action<Exception> onException = null, bool continueOnCapturedContext = true)
        {
            this.executeAsync = executeAsync;
            this.canExecute = canExecute ?? (() => true);
            executeOnException = onException;
            this.continueOnCapturedContext = continueOnCapturedContext;
        }
        #endregion

        #region Public API
        /// <summary>
        /// Executes the Command as a Task
        /// </summary>
        /// <returns>The executed Task</returns>
        public Task ExecuteAsync()
        {
            return executeAsync();
        }

        public override void Execute(object parameter)
        {
            ExecuteAsync()
                .FireAndForget(continueOnCapturedContext, executeOnException);
        }
        #endregion
    }

    public class BindableAsyncCommand<T> : BindableCommandBase, IAsyncCommand<T>
    {
        #region Fields & Properties
        private readonly Func<T, Task> executeAsync;
        private new readonly Func<T, bool> canExecute;
        private readonly Action<Exception> executeOnException;
        private readonly bool continueOnCapturedContext;
        #endregion

        #region CTOR
        /// <summary>
        /// Initializes a new instance of the BindableAsyncCommand class.
        /// </summary>
        /// <param name="executeAsync">A delegate to be invoked asynchronously by BindableAsyncCommand.</param>
        /// <param name="canExecute">A delegate resolving BindableAsyncCommand execute possibility.</param>
        /// <param name="onException">A delegate to be invoked by BindableAsyncCommand on in-task exception occurrance.</param>
        /// <param name="continueOnCapturedContext">Configures an awaiter used to await BindableAsyncCommand's task.</param>
        public BindableAsyncCommand(Func<T, Task> executeAsync, Func<T, bool> canExecute = null, Action<Exception> onException = null, bool continueOnCapturedContext = true)
        {
            this.executeAsync = executeAsync;
            this.canExecute = canExecute ?? ((_) => true);
            executeOnException = onException;
            this.continueOnCapturedContext = continueOnCapturedContext;
        }
        #endregion

        #region Public API
        public Task ExecuteAsync(T parameter)
        {
            return executeAsync.Invoke(parameter);
        }

        public override void Execute(object parameter)
        {
            if (parameter is T validParameter)
            {
                ExecuteAsync(validParameter)
                    .FireAndForget(continueOnCapturedContext, executeOnException);
            }
            else if (parameter is null && !typeof(T).IsValueType)
            {
                ExecuteAsync((T)parameter)
                    .FireAndForget(continueOnCapturedContext, executeOnException);
            }
            else
            {
                throw new ArgumentException($"Expected command parameter type {typeof(T).Name} differs from one provided {parameter.GetType()}");
            }
        }

        public override bool CanExecute(object parameter)
        {
            return canExecute((T)parameter);
        }
        #endregion
    }
}
