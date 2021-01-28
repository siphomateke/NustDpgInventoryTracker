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
            ViewModel.AllCategories = new List<EquipmentCategory>();
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
            LoadEquipmentConditions();
        }
        public EditEquipmentForm()
        {
            this.Init();
            ViewModel.IsNew = true;
            ViewModel.Equipment = new Equipment();
            RefreshCategories();
            LoadEquipmentConditions();
        }

        private async void LoadEquipmentConditions()
        {
            this.busyIndicator.IsBusy = true;
            try
            {
                DataAccess db = new DataAccess();
                List<EquipmentCondition> conditions = await db.GetEquipmentConditions();
                conditionComboBox.ItemsSource = conditions.Select(c => c.Name);
                conditionComboBox.SelectedIndex = 0;
                conditionComboBox.IsEnabled = true;
            }
            catch (Exception ex)
            {
                conditionComboBox.IsEnabled = false;
                MessageBox.Show(ex.Message, "Error loading conditions", MessageBoxButton.OK, MessageBoxImage.Error);
                throw;
            }
            finally
            {
                this.busyIndicator.IsBusy = false;
            }
        }

        private async Task RefreshEquipmentDetails()
        {
            if (ViewModel.EquipmentId != null)
            {
                DataAccess db = new DataAccess();
                ViewModel.Equipment = await db.GetEquipmentDetails((int)ViewModel.EquipmentId);
                // ViewModel.CategoryIds = ViewModel.Equipment.Categories.Select(c => c.CategoryId).ToList();
            }
        }

        private async void RefreshCategories()
        {
            this.busyIndicator.IsBusy = true;
            try
            {
                DataAccess db = new DataAccess();
                ViewModel.AllCategories = await db.GetCategories();
                // ViewModel.CategoryIds = new List<int>();
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

        private async void RefreshShops()
        {
            this.busyIndicator.IsBusy = true;
            try
            {
                DataAccess db = new DataAccess();
                ViewModel.AllShops = await db.GetAllShops();
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
                    ViewModel.EquipmentId = await db.AddEquipment(ViewModel.Equipment);
                else
                    // Handle price information
                    await db.RequestEquipmentChange(ViewModel.Equipment);

                // Update categories
                await Task.WhenAll(ViewModel.Equipment.Categories.Select((c) => db.AddCategoryToEquipment(c.CategoryId, (int)ViewModel.EquipmentId)));

                // FIXME: Add shops

                // FIXME: Add prices

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

        private void AddPriceButton_Click(object sender, RoutedEventArgs e)
        {
            PricingDataGrid.Items.Add(new EquipmentPrices());
        }
    }
}
