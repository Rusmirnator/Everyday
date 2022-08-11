using Everyday.Core.Models;
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

        public ObservableCollection<Item> Items
        {
            get { return GetValue(ref items); }
            set { _ = SetValue(ref items, value); }
        }

        public Item SelectedItem
        {
            get { return GetValue(ref selectedItem); }
            set { _ = SetValue(ref selectedItem, value); }
        }

        #endregion

        #region CTOR
        public PurchasesViewModel()
        {
            OpenScannerCommand = new Command(async () => await OpenScannerAsync());
            AlterItemCommand = new Command(async () => await OpenItemEditorAsync());
            InitCommand = new Command(async () => await GetItemsAsync());
        }
        #endregion

        #region Commands
        private static async Task GetItemsAsync()
        {
            await Task.CompletedTask;
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
    }
}
