using Everyday.Core.Models;
using Everyday.GUI.Base;
using Everyday.Services.Interfaces;

namespace Everyday.GUI.Pages.ViewModels
{
    public class ItemEditorViewModel : BaseViewModel
    {
        private readonly IItemService itemService;
        #region Fields & Properties
        public Item AlteredItem
        {
            get { return GetValue<Item>(); }
            set
            {
                _ = SetValue(value);
            }
        }
        #endregion

        #region CTOR
        public ItemEditorViewModel(IItemService itemService)
        {
            InitCommand = new Command(() => GetAlteredItem());
            this.itemService = itemService;
        }
        #endregion

        private void GetAlteredItem()
        {
            AlteredItem = Receive<Item>("SelectedItem", nameof(ItemEditorViewModel));
            RaisePropertyChanged(nameof(AlteredItem));
        }
    }
}
