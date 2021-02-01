using InventoryTrackerFrontend.Common;
using InventoryTrackerFrontend.Models;
using InventoryTrackerFrontend.ViewModels;
using InventoryTrackerFrontend.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace InventoryTrackerFrontend.Views
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
        }
        private async void PostInit()
        {
            RefreshCategories(); // This must run before getting equipment details
            await RefreshEquipmentDetails();
        }
        public EditEquipmentForm(int equipmentId)
        {
            this.Init();
            ViewModel.IsNew = false;
            ViewModel.EquipmentId = equipmentId;
            PostInit();
            LoadEquipmentConditions();
            RefreshShops();
        }
        public EditEquipmentForm()
        {
            this.Init();
            ViewModel.IsNew = true;
            ViewModel.Equipment = new Equipment();
            RefreshCategories();
            LoadEquipmentConditions();
            RefreshShops();
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
                // CheckComboBox requires the selected items to be the actual objects from the AllCategories list.
                var selectedCategories = ViewModel.Equipment.Categories.Select(c => ViewModel.AllCategories.Find(c2 => c2.CategoryId == c.CategoryId));
                ViewModel.SelectedCategories = new ObservableCollection<EquipmentCategory>(selectedCategories);
            }
        }

        private async void RefreshCategories()
        {
            this.busyIndicator.IsBusy = true;
            try
            {
                DataAccess db = new DataAccess();
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
                await db.SetEquipmentCategories((int)ViewModel.EquipmentId, ViewModel.SelectedCategories.ToList());

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
