namespace Everyday.GUI.Base
{
    public class BindableBehavior<T> : Behavior<T> where T : BindableObject
    {
        #region Fields & Properties
        public T AssociatedObject { get; private set; }
        #endregion

        #region EventHandlers
        protected override void OnAttachedTo(T bindable)
        {
            base.OnAttachedTo(bindable);

            AssociatedObject = bindable;

            if (bindable.BindingContext != null)
            {
                BindingContext = bindable.BindingContext;
            }

            bindable.BindingContextChanged += OnBindingContextChanged;
        }
        protected override void OnDetachingFrom(T bindable)
        {
            bindable.BindingContextChanged -= OnBindingContextChanged;
        }

        private void OnBindingContextChanged(object sender, EventArgs e)
        {
            OnBindingContextChanged();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            BindingContext = AssociatedObject.BindingContext;
        }
        #endregion
    }
}
