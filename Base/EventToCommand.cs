using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Windows.Input;

namespace Everyday.GUI.Base
{
    public class EventToCommand : BindableBehavior<View>
    {
        #region Fields & Properties
        private Delegate handler;
        private EventInfo eventInfo;

        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(EventToCommand), null);

        public static readonly BindableProperty EventNameProperty =
            BindableProperty.Create(nameof(EventName), typeof(string), typeof(EventToCommand), null);

        public static readonly BindableProperty CommandParameterProperty =
            BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(EventToCommand), null);

        public string EventName
        {
            get { return (string)GetValue(EventNameProperty); }
            set { SetValue(EventNameProperty, value); }
        }
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }
        public object CommandParameter
        {
            get { return GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }
        public bool PassEventArgsToCommand { get; set; }
        #endregion

        #region EventHandlers
        protected override void OnAttachedTo(View bindable)
        {
            base.OnAttachedTo(bindable);

            eventInfo = AssociatedObject?.GetType()?.GetRuntimeEvent(EventName);

            if (eventInfo is null)
            {
                throw new ArgumentException($"EventToCommand: Event {EventName} doesn't exist on attached type {AssociatedObject.GetType()}");
            }

            AddEventHandler(eventInfo, AssociatedObject, OnEventFired);
        }

        protected override void OnDetachingFrom(View bindable)
        {
            if (handler is not null)
            {
                eventInfo.RemoveEventHandler(AssociatedObject, handler);
            }

            base.OnDetachingFrom(bindable);
        }
        #endregion

        #region Private API
        private void OnEventFired(object sender, EventArgs e)
        {
            object parameter = PassEventArgsToCommand ? e : CommandParameter;

            if (Command is not null && Command.CanExecute(parameter))
            {
                Command.Execute(parameter);
            }
        }

        private void AddEventHandler(EventInfo eventInfo, object item, Action<object, EventArgs> action)
        {
            var eventParameters = eventInfo.EventHandlerType
                .GetRuntimeMethods().First(m => m.Name == "Invoke")
                    .GetParameters()
                        .Select(p => Expression.Parameter(p.ParameterType))
                            .ToArray();

            var actionInvoke = action.GetType()
                                        .GetRuntimeMethods()
                                            .FirstOrDefault(m => m.Name == "Invoke");

            handler = Expression.Lambda
                (
                        eventInfo.EventHandlerType,
                        Expression.Call(Expression.Constant(action), actionInvoke, eventParameters[0], eventParameters[1]),
                        eventParameters
                ).Compile();

            eventInfo.AddEventHandler(item, handler);
        }
        #endregion
    }
}
