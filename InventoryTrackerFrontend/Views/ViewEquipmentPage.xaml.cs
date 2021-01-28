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
    /// Interaction logic for ViewEquipmentForm.xaml
    /// </summary>
    public partial class ViewEquipmentPage : Page
    {
        public HomePageViewModel ViewModel { get; set; }

        public ViewEquipmentPage()
        {
            InitializeComponent();
            ViewModel = new HomePageViewModel();
            this.DataContext = ViewModel;
            RefreshEquipment();
        }

        private async void RefreshEquipment()
        {
            try
            {
                busyIndicator.IsBusy = true;
                DataAccess db = new DataAccess();
                ViewModel.Equipment = await db.GetEquipment();
                ViewModel.EquipmentChanges = await db.GetEquipmentChanges();
                ViewModel.EquipmentToBuy = await db.GetAllEquipmentToBuy();
            }
            finally
            {
                busyIndicator.IsBusy = false;
            }
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            RefreshEquipment();
        }

        private void equipmentDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                Equipment selectedEquipment = (Equipment)e.AddedItems[0];
                ViewModel.SelectedEquipmentId = selectedEquipment.EquipmentId;
            }
        }

        private void ViewDetailsButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EquipmentDetailsPage(ViewModel.SelectedEquipmentId));
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EditEquipmentForm(ViewModel.SelectedEquipmentId));
        }

        private void EquipmentChangeDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                EquipmentChange selectedEquipmentChange = (EquipmentChange)e.AddedItems[0];
                ViewModel.SelectedEquipmentChangeId = selectedEquipmentChange.EquipmentChangeId;
                ViewModel.AnyEquipmentChangeSelected = true;
            }
            else
            {
                ViewModel.AnyEquipmentChangeSelected = false;
            }
        }

        private async void DiscardChangeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                busyIndicator.IsBusy = true;
                DataAccess db = new DataAccess();
                await db.UndoEquipmentChange(ViewModel.SelectedEquipmentChangeId);
                RefreshEquipment();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                busyIndicator.IsBusy = false;
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EditEquipmentForm());
        }
    }
}
