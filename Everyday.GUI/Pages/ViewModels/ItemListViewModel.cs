using Everyday.Core.Models;
using Everyday.GUI.Base;
using Everyday.Services.Interfaces;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Everyday.GUI.Pages.ViewModels
{
    public class ItemListViewModel : BaseViewModel
    {
        #region Fields & Properties
        private readonly IItemService itemService;

        public ICommand OpenScannerCommand { get; set; }
        public ICommand AlterItemCommand { get; set; }
        public ICommand CreateItemCommand { get; set; }
        public string SearchTerm
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public ObservableCollection<Item> Items
        {
            get { return GetValue<ObservableCollection<Item>>(); }
            set { _ = SetValue(value); }
        }

        public bool IsWaitIndicatorVisible
        {
            get { return GetValue<bool>(); }
            set { _ = SetValue(value); }
        }

        public Item SelectedItem
        {
            get { return GetValue<Item>(); }
            set
            {
                if (SetValue(value))
                {
                    (AlterItemCommand as Command).ChangeCanExecute();
                }
            }
        }

        #endregion

        #region CTOR
        public ItemListViewModel(IItemService itemService)
        {
            OpenScannerCommand = new Command(async () => await OpenScannerAsync());
            AlterItemCommand = new Command(async () => await OpenItemEditorAsync(false), () => CanAlterItem());
            CreateItemCommand = new Command(async () => await OpenItemEditorAsync(true));
            InitCommand = new Command(async () => await GetItemsAsync());
            RefreshCommand = new Command(async () => await RefreshAsync());
            this.itemService = itemService;
        }
        #endregion

        #region Commands
        private async Task GetItemsAsync()
        {
            IsWaitIndicatorVisible = true;

            Items = new(await itemService.GetItemsAsync().ConfigureAwait(false));

            IsWaitIndicatorVisible = false;
        }
        private static async Task OpenScannerAsync()
        {
            await GoToPageAsync("Error");
        }

        private async Task OpenItemEditorAsync(bool createNew)
        {
            if (!createNew)
            {
                Send(nameof(ItemEditorViewModel), nameof(SelectedItem), SelectedItem);
            }

            await GoToPageAsync("ItemEditor");
        }

        private async Task RefreshAsync()
        {
            await GetItemsAsync();
        }
        #endregion

        #region Private API

        #endregion

        #region CanExecute
        public bool CanAlterItem()
        {
            return SelectedItem is not null;
        }
        #endregion
    }
}
