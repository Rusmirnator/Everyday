using Everyday.Core.Models;
using Everyday.Data.Interfaces;
using Everyday.GUI.Base;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Everyday.GUI.Pages.ViewModels
{
    public class PurchasesViewModel : BaseViewModel
    {
        #region Fields & Properties
        public ICommand OpenScannerCommand { get; set; }
        public ICommand AlterItemCommand { get; set; }

        private readonly IItemDataProvider dataProvider;

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
        public PurchasesViewModel(IItemDataProvider dataProvider)
        {
            OpenScannerCommand = new Command(async () => await OpenScannerAsync());
            AlterItemCommand = new Command(async () => await OpenItemEditorAsync(), () => CanAlterItem());
            InitCommand = new Command(async () => await GetItemsAsync());
            RefreshCommand = new Command(async () => await RefreshAsync());
            this.dataProvider = dataProvider;
        }
        #endregion

        #region Commands
        private async Task GetItemsAsync()
        {
            IsWaitIndicatorVisible = true;

            Items = new(await dataProvider.GetItemsAsync().ConfigureAwait(false));

            IsWaitIndicatorVisible = false;
        }
        private static async Task OpenScannerAsync()
        {
            await GoToPageAsync("Error");
        }

        private static async Task OpenItemEditorAsync()
        {
            //Pass selected item to EditorViewModel
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
