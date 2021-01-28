using InventoryTrackerFrontend.Models;
using InventoryTrackerFrontend.ViewModels;
using InventoryTrackerFrontend.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InventoryTrackerFrontend
{
    /// <summary>
    /// Interaction logic for AddEquipmentForm.xaml
    /// </summary>
    public partial class EditEquipmentForm : Page
    {
        public EditEquipmentFormViewModel ViewModel
        {
            get
            {
                return (EditEquipmentFormViewModel)this.DataContext;
            }
        }
        private void Init()
        {
            InitializeComponent();
            this._mainForm.DataContext = ViewModel.Equipment;
            ViewModel.AllCategories = new List<EquipmentCategory>();
            this.CategoryComboBox.DataContext = ViewModel;
            //ViewModel = new EditEquipmentFormViewModel();
            //this.DataContext = ViewModel;
        }
        private async void PostInit()
        {
            await RefreshEquipmentDetails();
            RefreshCategories();
        }
        public EditEquipmentForm(int equipmentId)
        {
            this.Init();
            ViewModel.IsNew = false;
            ViewModel.EquipmentId = equipmentId;
            PostInit();
        }
        public EditEquipmentForm()
        {
            this.Init();
            ViewModel.IsNew = true;
            ViewModel.Equipment = new Equipment();
            this._mainForm.DataContext = ViewModel.Equipment;
            RefreshCategories();
        }

        public async Task RefreshEquipmentDetails()
        {
            if (ViewModel.EquipmentId != null)
            {
                DataAccess db = new DataAccess();
                ViewModel.Equipment = await db.GetEquipmentDetails((int)ViewModel.EquipmentId);
                this._mainForm.DataContext = ViewModel.Equipment;
            }
        }

        public async void RefreshCategories()
        {
            this.busyIndicator.IsBusy = true;
            try
            {
                DataAccess db = new DataAccess();
                Console.WriteLine(ViewModel.Equipment.Categories);
                ViewModel.AllCategories = await db.GetCategories();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                this.busyIndicator.IsBusy = false;
            }
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            this.busyIndicator.IsBusy = true;
            try
            {
                DataAccess db = new DataAccess();
                if (ViewModel.IsNew)
                    await db.AddEquipment(ViewModel.Equipment);
                else
                    await db.RequestEquipmentChange(ViewModel.Equipment);
                this.busyIndicator.IsBusy = false;

                this.NavigationService.GoBack();
                // FIXME: Refresh data

                if (ViewModel.IsNew)
                    MessageBox.Show("Successfully added new equipment", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                else
                    MessageBox.Show("Successfully requested for change to equipment", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                this.busyIndicator.IsBusy = false;
            }
        }

        private async void AddCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            this.busyIndicator.IsBusy = true;
            try
            {
                NewCategoryDialog dialog = new NewCategoryDialog();
                if (dialog.ShowDialog() == true)
                {
                    DataAccess db = new DataAccess();
                    await db.AddCategory(dialog.ViewModel.Name, dialog.ViewModel.Description);
                    RefreshCategories();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                this.busyIndicator.IsBusy = false;
            }
        }
    }
}
