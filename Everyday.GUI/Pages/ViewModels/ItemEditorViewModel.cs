using Everyday.Core.Dictionaries;
using Everyday.Core.Interfaces;
using Everyday.Core.Models;
using Everyday.GUI.Base;
using Everyday.Services.Interfaces;
using System.Windows.Input;

namespace Everyday.GUI.Pages.ViewModels
{
    public class ItemEditorViewModel : BaseViewModel
    {
        #region Fields & Properties         
        private readonly IItemService itemService;

        public ICommand SaveCommand { get; set; }
        public Item AlteredItem
        {
            get { return GetValue<Item>(); }
            set
            {
                _ = SetValue(value);
            }
        }

        public string Code
        {
            get { return GetValue<string>(); }
            set { _ = SetValue(value); }
        }

        public string Name
        {
            get { return GetValue<string>(); }
            set { _ = SetValue(value); }
        }

        public string Description
        {
            get { return GetValue<string>(); }
            set { _ = SetValue(value); }
        }

        public double? Width
        {
            get { return GetValue<double?>(); }
            set { _ = SetValue(value); }
        }

        public double? Height
        {
            get { return GetValue<double?>(); }
            set { _ = SetValue(value); }
        }

        public double? Depth
        {
            get { return GetValue<double?>(); }
            set { _ = SetValue(value); }
        }

        public double? Price
        {
            get { return GetValue<double?>(); }
            set { _ = SetValue(value); }
        }

        public double? Weight
        {
            get { return GetValue<double?>(); }
            set { _ = SetValue(value); }
        }

        public string ManufacturerName
        {
            get { return GetValue<string>(); }
            set { _ = SetValue(value); }
        }

        public string ManufacturerDescription
        {
            get { return GetValue<string>(); }
            set { _ = SetValue(value); }
        }

        public ItemCateoryType Category
        {
            get { return GetValue<ItemCateoryType>(); }
            set { _ = SetValue(value); }
        }
        #endregion

        #region CTOR
        public ItemEditorViewModel(IItemService itemService) : base()
        {
            InitializeCommands();
            this.itemService = itemService;
        }
        #endregion

        #region Commands
        private async Task InitAsync()
        {
            GetParametersFromNetwork();
            InitializeEditors();

            await Task.CompletedTask;
        }

        private async Task RefreshAsync()
        {
            await Task.CompletedTask;
        }

        private async Task SaveAsync()
        {
            string res = await DecideAsync("Choose item category", "Cancel", "Consumable", "Chemical", "Container");

            if (res.Equals("Cancel", StringComparison.Ordinal))
            {
                return;
            }

            Item alteredItem = new Item
            {
                Code = Code,
                Name = Name,
                Description = Description,
                Width = Width,
                Depth = Depth,
                Weight = Weight,
                Price = Price,
            };

            if (!string.IsNullOrEmpty(string.Concat(ManufacturerName, ManufacturerDescription)))
            {
                alteredItem.Manufacturer = new Manufacturer
                {
                    Name = ManufacturerName,
                    Description = ManufacturerDescription
                };
            }

            alteredItem.ItemDefinition = new ItemDefinition
            {
                DimensionsMeasureUnitId = 2,
                WeightMeasureUnitId = 1,
                ItemCategoryTypeId = 1
            };

            IConveyOperationResult response = await itemService.CreateItemAsync(alteredItem);

            if (response.StatusCode != 0)
            {
                await AnnounceAsync("Error", response.Message, "Ok");
            }

            await Task.CompletedTask;
        }
        #endregion

        #region PrivateAPI

        #region Initializers
        private void InitializeEditors()
        {
            Code = AlteredItem.Code;
            Name = AlteredItem.Name;
            Description = AlteredItem.Description;
            Width = AlteredItem.Width;
            Height = AlteredItem.Height;
            Depth = AlteredItem.Depth;
            Weight = AlteredItem.Weight;
            Price = AlteredItem.Price;
            ManufacturerName = AlteredItem?.Manufacturer?.Name;
            ManufacturerDescription = AlteredItem?.Manufacturer?.Description;
        }

        private void InitializeCommands()
        {
            InitCommand = new Command(async () => await InitAsync());
            RefreshCommand = new Command(async () => await RefreshAsync());
            SaveCommand = new Command(async () => await SaveAsync(), () => CanSave());
        }
        #endregion

        private void GetParametersFromNetwork()
        {
            AlteredItem = Receive<Item>("SelectedItem", nameof(ItemEditorViewModel));
        }
        #endregion

        #region CanExecute
        public bool CanSave()
        {
            return true;
        }
        #endregion
    }
}
