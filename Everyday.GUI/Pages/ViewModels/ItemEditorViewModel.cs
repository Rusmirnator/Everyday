﻿using Everyday.Application.Consumable.Interfaces;
using Everyday.Application.Item.Interfaces;
using Everyday.Application.Manufacturer.Interfaces;
using Everyday.Domain.Attributes;
using Everyday.Domain.Dictionaries;
using Everyday.Domain.Models;
using Everyday.Domain.Shared;
using Everyday.GUI.Base;
using Everyday.GUI.Utilities;
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
        private bool isExistingItemAltered;

        public ICommand SaveCommand { get; set; }
        public ICommand SelectManufacturerCommand { get; set; }
        public ICommand SelectItemCategoryCommand { get; set; }
        public ICommand SelectWeightMeasureUnitCommand { get; set; }
        public ICommand SelectDimensionsMeasureUnitCommand { get; set; }

        public Item AlteredItem
        {
            get { return GetValue<Item>(); }
            set
            {
                _ = SetValue(value);
            }
        }
        public ObservableCollection<BindableEnum> ItemCategories
        {
            get { return GetValue<ObservableCollection<BindableEnum>>(); }
            set { _ = SetValue(value); }
        }

        public ObservableCollection<BindableEnum> MeasureUnits
        {
            get { return GetValue<ObservableCollection<BindableEnum>>(); }
            set { _ = SetValue(value); }
        }

        public BindableEnum SelectedItemCategory
        {
            get { return GetValue<BindableEnum>(); }
            set
            {
                if (SetValue(value))
                {
                    (SaveCommand as BindableAsyncCommand).RaiseCanExecuteChanged();
                }
            }
        }

        public BindableEnum SelectedWeightMeasureUnit
        {
            get { return GetValue<BindableEnum>(); }
            set
            {
                if (SetValue(value))
                {
                    (SaveCommand as BindableAsyncCommand).RaiseCanExecuteChanged();
                }
            }
        }

        public BindableEnum SelectedDimensionsMeasureUnit
        {
            get { return GetValue<BindableEnum>(); }
            set
            {
                if (SetValue(value))
                {
                    (SaveCommand as BindableAsyncCommand).RaiseCanExecuteChanged();
                }
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
            set
            {
                if (SetValue(value))
                {
                    (SaveCommand as BindableAsyncCommand).RaiseCanExecuteChanged();
                }
            }
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

        public bool IsConsumableEditorGroupVisible
        {
            get { return GetValue<bool>(); }
            set { _ = SetValue(value); }
        }

        public bool IsChemicalEditorGroupVisible
        {
            get { return GetValue<bool>(); }
            set { _ = SetValue(value); }
        }

        public bool IsContainerEditorGroupVisible
        {
            get { return GetValue<bool>(); }
            set { _ = SetValue(value); }
        }

        public bool IsManufacturerDataEditLocked
        {
            get { return GetValue<bool>(); }
            set { _ = SetValue(value); }
        }

        [Consumable(typeof(Item))]
        public string Code
        {
            get { return GetValue<string>(); }
            set { _ = SetValue(value); }
        }

        [Consumable(typeof(Item))]
        public string Name
        {
            get { return GetValue<string>(); }
            set { _ = SetValue(value); }
        }

        [Consumable(typeof(Item))]
        public string Description
        {
            get { return GetValue<string>(); }
            set { _ = SetValue(value); }
        }

        [Consumable(typeof(Item))]
        public double? Width
        {
            get { return GetValue<double?>(); }
            set { _ = SetValue(value); }
        }

        [Consumable(typeof(Item))]
        public double? Height
        {
            get { return GetValue<double?>(); }
            set { _ = SetValue(value); }
        }

        [Consumable(typeof(Item))]
        public double? Depth
        {
            get { return GetValue<double?>(); }
            set { _ = SetValue(value); }
        }

        [Consumable(typeof(Item))]
        public double? Price
        {
            get { return GetValue<double?>(); }
            set { _ = SetValue(value); }
        }

        [Consumable(typeof(Item))]
        public double? Weight
        {
            get { return GetValue<double?>(); }
            set { _ = SetValue(value); }
        }

        [Consumable(typeof(Manufacturer), path: "Name")]
        public string ManufacturerName
        {
            get { return GetValue<string>(); }
            set
            {
                if (SetValue(value))
                {
                    (SaveCommand as BindableAsyncCommand).RaiseCanExecuteChanged();
                }
            }
        }

        [Consumable(typeof(Manufacturer), path: "Description")]
        public string ManufacturerDescription
        {
            get { return GetValue<string>(); }
            set
            {
                if (SetValue(value))
                {
                    (SaveCommand as BindableAsyncCommand).RaiseCanExecuteChanged();
                }
            }
        }

        [Consumable(typeof(Consumable))]
        public double? Protein
        {
            get { return GetValue<double?>(); }
            set { _ = SetValue(value); }
        }

        [Consumable(typeof(Consumable))]
        public double? Carbohydrates
        {
            get { return GetValue<double?>(); }
            set { _ = SetValue(value); }
        }

        [Consumable(typeof(Consumable))]
        public double? Sugars
        {
            get { return GetValue<double?>(); }
            set { _ = SetValue(value); }
        }

        [Consumable(typeof(Consumable))]
        public double? Fat
        {
            get { return GetValue<double?>(); }
            set { _ = SetValue(value); }
        }

        [Consumable(typeof(Consumable))]
        public double? SaturatedFat
        {
            get { return GetValue<double?>(); }
            set { _ = SetValue(value); }
        }

        [Consumable(typeof(Consumable))]
        public double? Fiber
        {
            get { return GetValue<double?>(); }
            set { _ = SetValue(value); }
        }

        [Consumable(typeof(Consumable))]
        public double? Salt
        {
            get { return GetValue<double?>(); }
            set { _ = SetValue(value); }
        }

        [Consumable(typeof(Consumable))]
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

        public MeasureUnit WeightMeasureUnit
        {
            get { return GetValue<MeasureUnit>(); }
            set { _ = SetValue(value); }
        }

        public MeasureUnit DimensionsMeasureUnit
        {
            get { return GetValue<MeasureUnit>(); }
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
        [AsyncCommand]
        public async Task InitAsync()
        {
            IsWaitIndicatorVisible = true;

            GetParametersFromNetwork();

            await RefreshAsync();

            InitializeEditors();

            IsWaitIndicatorVisible = false;
        }

        [AsyncCommand]
        public async Task RefreshAsync()
        {
            IsWaitIndicatorVisible = true;

            ItemCategories = new(typeof(ItemCateoryType).ToEnumerable());
            MeasureUnits = new(typeof(MeasureUnit).ToEnumerable());

            Manufacturers = new(await manufacturerService.GetManufacturersAsync());
            Manufacturers.Add(new Manufacturer());


            if (AlteredItem is not null)
            {
                AlteredConsumable = await consumableService.GetConsumableByItemId(AlteredItem.Id.GetValueOrDefault());
            }

            IsWaitIndicatorVisible = false;
        }

        [AsyncCommand]
        public async Task SaveAsync()
        {
            IsWaitIndicatorVisible = true;

            Item alteredItem = new();
            alteredItem.Consume<ItemEditorViewModel, Item>(this);

            if (!string.IsNullOrEmpty(string.Concat(ManufacturerName, ManufacturerDescription)) && !IsManufacturerDataEditLocked)
            {
                alteredItem.Manufacturer = new();
                alteredItem.Manufacturer.Consume<ItemEditorViewModel, Manufacturer>(this);
            }

            if (!string.IsNullOrEmpty(SelectedManufacturer?.Name))
            {
                alteredItem.Manufacturer = SelectedManufacturer;
            }

            alteredItem.ItemDefinition = new ItemDefinition
            {
                DimensionsMeasureUnitId = (int)DimensionsMeasureUnit,
                WeightMeasureUnitId = (int)WeightMeasureUnit,
                ItemCategoryTypeId = (int)Category
            };

            //IConveyOperationResult response = await ExecuteItemDataChangesAsync(alteredItem);

            //if (response.StatusCode != 0)
            //{
            //    IsWaitIndicatorVisible = false;

            //    await AnnounceAsync("Error", response.Message, "Ok");
            //    return;
            //}

            //Consumable alteredConsumable = new();
            //alteredConsumable.Consume<ItemEditorViewModel, Consumable>(this);

            //Item ownerItem = await itemService.GetItemByCodeAsync(Code);
            //alteredConsumable.ItemId = ownerItem.Id;

            //response = await ExecuteConsumableDataChangesAsync(alteredConsumable);

            //if (response.StatusCode != 0)
            //{
            //    IsWaitIndicatorVisible = false;

            //    await AnnounceAsync("Error", response.Message, "Ok");

            //    return;
            //}

            //IsWaitIndicatorVisible = false;

            //await AnnounceAsync("Success", response.Message, "Ok");

            CleanUp();

            await GoToPageAsync("ItemList");
        }

        [Command]
        public void SelectManufacturer()
        {
            if (SelectedManufacturer is null || SelectedManufacturer is not null && SelectedManufacturer.Id == 0)
            {
                IsManufacturerDataEditLocked = false;

                ManufacturerName = string.Empty;
                ManufacturerDescription = string.Empty;
                return;
            }

            ManufacturerName = SelectedManufacturer.Name;
            ManufacturerDescription = SelectedManufacturer.Description;

            IsManufacturerDataEditLocked = true;
        }

        [Command]
        public void SelectItemCategory()
        {
            if (SelectedItemCategory is null)
            {
                return;
            }

            Category = SelectedItemCategory.ToEnum<ItemCateoryType>();

            ResolveEditorGroupsVisibility();
        }

        [Command]
        public void SelectWeightMeasureUnit()
        {
            if (SelectedWeightMeasureUnit is null)
            {
                return;
            }

            WeightMeasureUnit = SelectedWeightMeasureUnit.ToEnum<MeasureUnit>();
        }

        [Command]
        public void SelectDimensionsMeasureUnit()
        {
            if (SelectedDimensionsMeasureUnit is null)
            {
                return;
            }

            DimensionsMeasureUnit = SelectedDimensionsMeasureUnit.ToEnum<MeasureUnit>();
        }

        [Command]
        public void CleanUp()
        {
            AlteredConsumable = null;
            AlteredItem = null;
            isExistingItemAltered = false;

            Code = null;
            Name = null;
            Description = null;
            Width = null;
            Height = null;
            Depth = null;
            Weight = null;
            Price = null;
            ManufacturerName = null;
            ManufacturerDescription = null;
            Energy = null;
            Protein = null;
            Carbohydrates = null;
            Sugars = null;
            Fat = null;
            SaturatedFat = null;
            Fiber = null;
            Salt = null;

            Category = default;
            WeightMeasureUnit = default;
            DimensionsMeasureUnit = default;
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

            AlteredItem.Feed<ItemEditorViewModel, Item>(this);
            AlteredItem?.Manufacturer?.Feed<ItemEditorViewModel, Manufacturer>(this);
            AlteredConsumable?.Feed<ItemEditorViewModel, Consumable>(this);

            SelectedItemCategory = ItemCategories.FirstOrDefault(c => c.Value == AlteredItem.ItemDefinition.ItemCategoryTypeId);
            SelectedWeightMeasureUnit = MeasureUnits.FirstOrDefault(u => u.Value == AlteredItem.ItemDefinition.WeightMeasureUnitId);
            SelectedDimensionsMeasureUnit = MeasureUnits.FirstOrDefault(u => u.Value == AlteredItem.ItemDefinition.DimensionsMeasureUnitId);
            SelectedManufacturer = Manufacturers.FirstOrDefault(m => m.Id == AlteredItem?.Manufacturer?.Id);
        }

        private void InitializeCommands()
        {
            InitCommand = new BindableAsyncCommand(async () => await InitAsync());
            RefreshCommand = new BindableAsyncCommand(async () => await RefreshAsync());
            CleanUpCommand = new BindableCommand(() => CleanUp());
            SaveCommand = new BindableAsyncCommand(async () => await SaveAsync(), () => CanSave());
            SelectManufacturerCommand = new BindableCommand(() => SelectManufacturer());
            SelectItemCategoryCommand = new BindableCommand(() => SelectItemCategory());
            SelectWeightMeasureUnitCommand = new BindableCommand(() => SelectWeightMeasureUnit());
            SelectDimensionsMeasureUnitCommand = new BindableCommand(() => SelectDimensionsMeasureUnit());
        }
        #endregion

        #region BehaviorResolvers
        //private async Task<IConveyOperationResult> ExecuteItemDataChangesAsync(Item alteredItem)
        //{
        //    if (isExistingItemAltered)
        //    {
        //        return await itemService.UpdateItemAsync(alteredItem);
        //    }

        //    return await itemService.CreateItemAsync(alteredItem);
        //}

        //private async Task<IConveyOperationResult> ExecuteConsumableDataChangesAsync(Consumable alteredConsumable)
        //{
        //    if (isExistingItemAltered)
        //    {
        //        return await consumableService.UpdateConsumableAsync(alteredConsumable);
        //    }

        //    return await consumableService.CreateConsumableAsync(alteredConsumable);
        //}

        private void ResolveEditorGroupsVisibility()
        {
            IsConsumableEditorGroupVisible = Category == ItemCateoryType.Consumable;
            IsChemicalEditorGroupVisible = Category == ItemCateoryType.Chemical;
            IsContainerEditorGroupVisible = Category == ItemCateoryType.Container;
        }
        #endregion

        #region DataFlow
        private void GetParametersFromNetwork()
        {
            AlteredItem = Receive<Item>("SelectedItem", nameof(ItemEditorViewModel));

            if (AlteredItem is not null)
            {
                isExistingItemAltered = true;
            }
        }
        #endregion

        #endregion

        #region CanExecute
        public bool CanSave()
        {
            return !string.IsNullOrEmpty(ManufacturerName)
                && !string.IsNullOrEmpty(ManufacturerDescription)
                && SelectedItemCategory is not null
                && SelectedDimensionsMeasureUnit is not null
                && SelectedWeightMeasureUnit is not null;
        }
        #endregion
    }
}
