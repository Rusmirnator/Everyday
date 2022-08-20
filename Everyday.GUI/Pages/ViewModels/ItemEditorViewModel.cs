using Everyday.Core.Models;
using Everyday.GUI.Base;
using Everyday.Services.Interfaces;

namespace Everyday.GUI.Pages.ViewModels
{
    public class ItemEditorViewModel : BaseViewModel
    {

        #region Fields & Properties         
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
        #endregion

        #region CTOR
        public ItemEditorViewModel() : base()
        {
            InitializeCommands();
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
        }
        #endregion

        private async Task InitAsync()
        {
            GetParametersFromNetwork();
            InitializeEditors();

            await  Task.CompletedTask;
        }

        private void GetParametersFromNetwork()
        {
            AlteredItem = Receive<Item>("SelectedItem", nameof(ItemEditorViewModel));
        }
        #endregion
    }
}
