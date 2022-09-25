using Everyday.Core.Attributes;
using Everyday.Core.Models;
using Everyday.GUI.Base;
using Everyday.GUI.Base.Interfaces;
using Everyday.Services.Interfaces;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Everyday.GUI.Pages.ViewModels
{
    public class ItemListViewModel : BaseViewModel
    {
        #region Fields & Properties
        private readonly IItemService itemService;

        public IAsyncCommand OpenScannerCommand { get; set; }
        public IAsyncCommand AlterItemCommand { get; set; }
        public IAsyncCommand CreateItemCommand { get; set; }
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
                    (AlterItemCommand as BindableAsyncCommand).RaiseCanExecuteChanged();
                }
            }
        }

        #endregion

        #region CTOR
        public ItemListViewModel(IItemService itemService)
        {
            OpenScannerCommand = new BindableAsyncCommand(async () => await OpenScannerAsync());
            AlterItemCommand = new BindableAsyncCommand(async () => await OpenItemEditorAsync(false), () => CanAlterItem());
            CreateItemCommand = new BindableAsyncCommand(async () => await OpenItemEditorAsync(true));
            InitCommand = new BindableAsyncCommand(async () => await GetItemsAsync());
            RefreshCommand = new BindableAsyncCommand(async () => await RefreshAsync());
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

        [AsyncCommand]
        public static async Task OpenScannerAsync()
        {
            await GoToPageAsync("Error");
        }

        [AsyncCommand]
        public async Task OpenItemEditorAsync(bool createNew)
        {
            IsWaitIndicatorVisible = true;

            if (!createNew)
            {
                Send(nameof(ItemEditorViewModel), nameof(SelectedItem), SelectedItem);
            }

            await GoToPageAsync("ItemEditor");

            IsWaitIndicatorVisible = false;
        }

        [AsyncCommand]
        public async Task RefreshAsync()
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
