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

        private ObservableCollection<Item> items;
        private Item selectedItem;
        private readonly IItemDataProvider dataProvider;

        private string searchTerm;

        public string SearchTerm
        {
            get { return GetValue(ref searchTerm); }
            set { SetValue(ref searchTerm, value); }
        }

        public ObservableCollection<Item> Items
        {
            get { return GetValue(ref items); }
            set { _ = SetValue(ref items, value); }
        }

        public Item SelectedItem
        {
            get { return GetValue(ref selectedItem); }
            set
            {
                if (SetValue(ref selectedItem, value))
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
            this.dataProvider = dataProvider;
        }
        #endregion

        #region Commands
        private async Task GetItemsAsync()
        {
            Items = new(await dataProvider.GetItemsAsync());
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
