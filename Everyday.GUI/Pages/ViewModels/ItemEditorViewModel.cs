using Everyday.Core.Dictionaries;
using Everyday.Core.Interfaces;
using Everyday.Core.Models;
using Everyday.GUI.Base;
using Everyday.Services.Interfaces;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Everyday.GUI.Pages.ViewModels
{
    public class ItemEditorViewModel : BaseViewModel
    {
        #region Fields & Properties         
        private readonly IItemService itemService;
        private readonly IManufacturerService manufacturerService;
        private readonly IConsumableService consumableService;

        public ICommand SaveCommand { get; set; }
        public ICommand SelectManufacturerCommand { get; set; }

        public Item AlteredItem
        {
            get { return GetValue<Item>(); }
            set
            {
                _ = SetValue(value);
            }
        }

        public ObservableCollection<Manufacturer> Manufacturers
        {
            get { return GetValue<ObservableCollection<Manufacturer>>(); }
            set { _ = SetValue(value); }
        }

        public Manufacturer SelectedManufacturer
        {
            get { return GetValue<Manufacturer>(); }
            set { _ = SetValue(value); }
        }

        public Consumable AlteredConsumable
        {
            get { return GetValue<Consumable>(); }
            set { _ = SetValue(value); }
        }

        public bool IsWaitIndicatorVisible
        {
            get { return GetValue<bool>(); }
            set { _ = SetValue(value); }
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

        public double? Protein
        {
            get { return GetValue<double?>(); }
            set { _ = SetValue(value); }
        }

        public double? Carbohydrates
        {
            get { return GetValue<double?>(); }
            set { _ = SetValue(value); }
        }

        public double? Sugars
        {
            get { return GetValue<double?>(); }
            set { _ = SetValue(value); }
        }

        public double? Fat
        {
            get { return GetValue<double?>(); }
            set { _ = SetValue(value); }
        }

        public double? SaturatedFat
        {
            get { return GetValue<double?>(); }
            set { _ = SetValue(value); }
        }

        public double? Fiber
        {
            get { return GetValue<double?>(); }
            set { _ = SetValue(value); }
        }

        public double? Salt
        {
            get { return GetValue<double?>(); }
            set { _ = SetValue(value); }
        }

        public double? Energy
        {
            get { return GetValue<double?>(); }
            set { _ = SetValue(value); }
        }

        public ItemCateoryType Category
        {
            get { return GetValue<ItemCateoryType>(); }
            set { _ = SetValue(value); }
        }
        #endregion

        #region CTOR
        public ItemEditorViewModel(IItemService itemService, IManufacturerService manufacturerService, IConsumableService consumableService) : base()
        {
            InitializeCommands();
            this.itemService = itemService;
            this.manufacturerService = manufacturerService;
            this.consumableService = consumableService;
        }
        #endregion

        #region Commands
        private async Task InitAsync()
        {
            GetParametersFromNetwork();

            await RefreshAsync();

            InitializeEditors();
        }

        private async Task RefreshAsync()
        {
            IsWaitIndicatorVisible = true;

            Manufacturers = new(await manufacturerService.GetManufacturersAsync().ConfigureAwait(false));

            if (AlteredItem is not null)
            {
                AlteredConsumable = await consumableService.GetConsumableByItemId(AlteredItem.Id.GetValueOrDefault());
            }

            IsWaitIndicatorVisible = false;
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
                return;
            }

            Consumable consumable = new Consumable
            {
                Energy = Energy,
                Protein = Protein,
                Carbohydrates = Carbohydrates,
                Sugars = Sugars,
                Fat = Fat,
                SaturatedFat = SaturatedFat,
                Fiber = Fiber,
                Salt = Salt,
            };

            response = await consumableService.CreateConsumableAsync(consumable);

            if (response.StatusCode != 0)
            {
                await AnnounceAsync("Error", response.Message, "Ok");
                return;
            }
        }

        private void SelectManufacturer()
        {
            if (SelectedManufacturer is null)
            {
                return;
            }

            ManufacturerName = SelectedManufacturer.Name;
            ManufacturerDescription = SelectedManufacturer.Description;
        }

        private void CleanUp()
        {
            AlteredConsumable = null;
            AlteredItem = null;
        }
        #endregion

        #region PrivateAPI

        #region Initializers
        private void InitializeEditors()
        {
            if (AlteredItem is null)
            {
                return;
            }

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

            if (AlteredConsumable is null)
            {
                return;
            }

            Energy = AlteredConsumable.Energy;
            Protein = AlteredConsumable.Protein;
            Carbohydrates = AlteredConsumable.Carbohydrates;
            Sugars = AlteredConsumable.Sugars;
            Fat = AlteredConsumable.Fat;
            SaturatedFat = AlteredConsumable.SaturatedFat;
            Fiber = AlteredConsumable.Fiber;
            Salt = AlteredConsumable.Salt;
        }

        private void InitializeCommands()
        {
            InitCommand = new Command(async () => await InitAsync());
            RefreshCommand = new Command(async () => await RefreshAsync());
            CleanUpCommand = new Command(() => CleanUp());
            SaveCommand = new Command(async () => await SaveAsync(), () => CanSave());
            SelectManufacturerCommand = new Command(() => SelectManufacturer());
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
