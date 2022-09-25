using System.Windows.Input;

namespace Everyday.GUI.Base
{
    /// <summary>
    /// Special type of ICommand designed to be created at runtime by BindableBase.
    /// </summary>
    public abstract class BindableCommandBase : ICommand
    {
        #region Events
        /// <summary>
        /// Occurs when changes occur that affect whether or not the command should execute
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add => weakEventManager.AddEventHandler(value);
            remove => weakEventManager.RemoveEventHandler(value);
        }
        #endregion

        #region Fields & Properties
        protected Action execute;
        protected Func<bool> canExecute;
        protected WeakEventManager weakEventManager = new();
        #endregion

        #region Public API
        public virtual bool CanExecute(object parameter)
        {
            return canExecute();
        }

        public virtual void Execute(object parameter)
        {
            execute();
        }

        /// <summary>
        /// Raises the CanExecuteChanged event.
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            weakEventManager.HandleEvent(this, EventArgs.Empty, nameof(CanExecuteChanged));
        }
        #endregion
    }
    /// <summary>
    /// Special type of ICommand designed to be created at runtime by BindableBase.
    /// </summary>
    public class BindableCommand : BindableCommandBase
    {
        #region CTOR
        /// <summary>
        /// Initializes new instance of BindableCommand class.
        /// </summary>
        /// <param name="execute">A delegate to be invoked by BindableCommand.</param>
        public BindableCommand(Action execute)
        {
            this.execute = execute;
            canExecute = () => true;
        }

        /// <summary>
        /// Initializes new instance of BindableCommand class.
        /// </summary>
        /// <param name="execute">A delegate to be invoked by BindableCommand.</param>
        /// <param name="canExecute">A delegate resolving BindableCommand execute possibility.</param>
        public BindableCommand(Action execute, Func<bool> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }
        #endregion
    }

    /// <summary>
    /// Special type of ICommand designed to be created at runtime by BindableBase.
    /// </summary>
    public class BindableCommand<T> : BindableCommandBase
    {
        #region Fields & Properties
        private new readonly Action<T> execute;
        private new readonly Func<T, bool> canExecute;
        #endregion

        #region CTOR
        /// <summary>
        /// Initializes new instance of BindableCommand class.
        /// </summary>
        /// <param name="execute">A delegate to be invoked by BindableCommand.</param>
        public BindableCommand(Action<T> execute)
        {
            this.execute = execute;
            canExecute = (_) => true;
        }

        /// <summary>
        /// Initializes new instance of BindableCommand class.
        /// </summary>
        /// <param name="execute">A delegate to be invoked by BindableCommand.</param>
        /// <param name="canExecute">A delegate resolving BindableCommand execute possibility.</param>
        public BindableCommand(Action<T> execute, Func<T, bool> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }
        #endregion

        #region Public API
        public override bool CanExecute(object parameter)
        {
            return canExecute((T)parameter);
        }

        public override void Execute(object parameter)
        {
            execute((T)parameter);
        }
        #endregion
    }
}
