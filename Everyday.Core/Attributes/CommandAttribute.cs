using System.Runtime.CompilerServices;

namespace Everyday.Core.Attributes
{
    /// <summary>
    /// Provides info for BindableBase to fetch marked method and create BindableCommand
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class CommandAttribute : Attribute
    {
        protected const string CAN_EXECUTE_PREFIX = "Can";
        public string? CommandName { get; private set; }
        public string? CanExecuteMethodName { get; private set; }

        /// <summary>
        /// Initializes new instance of CommandAttribute class.
        /// </summary>
        /// <param name="commandName">This parameter is auto implemented and should not be specified manually - if so, BindableBase will search for command matching parameter value.</param>
        /// <param name="canExecuteMethodName">If not specified, BindableBase will search for method with marked command's name preceeded with "Can".</param>
        public CommandAttribute([CallerMemberName] string? commandName = null, string? canExecuteMethodName = null)
        {
            CommandName = commandName;
            CanExecuteMethodName = canExecuteMethodName ?? string.Concat(CAN_EXECUTE_PREFIX, commandName);
        }
    }
}
