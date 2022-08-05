using System.Windows.Input;

namespace Everyday.GUI.Base
{
    public class EventToCommand : Behavior<View>
    {
        public string EventName { get; set; }
        public string CommandName { get; set; }
        public ICommand Command { get; set; }
        public object CommandParameter { get; set; }
        public bool PassEventArgsToCommand { get; set; }

        protected override void OnAttachedTo(View bindable)
        {
            AssociatedType
                .GetEvent(EventName)
                    .AddEventHandler(bindable, FireEvent);

            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(View bindable)
        {
            AssociatedType
                .GetEvent(EventName)
                    .RemoveEventHandler(bindable, FireEvent);

            base.OnDetachingFrom(bindable);
        }

        private void FireEvent(object sender, EventArgs e)
        {
            Command.Execute(PassEventArgsToCommand ? e : CommandParameter);
        }
    }
}
