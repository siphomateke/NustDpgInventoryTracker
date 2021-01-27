using InventoryTrackerFrontend.Models;
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
        List<Equipment> equipment = new List<Equipment>();

        public int SelectedEquipmentId { get; set; }

        public ViewEquipmentPage()
        {
            InitializeComponent();
            RefreshEquipment();
        }

        private async void RefreshEquipment()
        {
            try
            {
                this.busyIndicator.IsBusy = true;
                DataAccess db = new DataAccess();
                equipment = await db.GetEquipment();

                equipmentDataGrid.ItemsSource = equipment;
            }
            finally
            {
                this.busyIndicator.IsBusy = false;
            }
        }

        private void refreshButton_Click(object sender, RoutedEventArgs e)
        {
            RefreshEquipment();
        }

        private void equipmentDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                Equipment selectedEquipment = (Equipment)e.AddedItems[0];
                SelectedEquipmentId = selectedEquipment.EquipmentId;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ViewDetailsButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EquipmentDetailsPage(SelectedEquipmentId));
        }
    }
}
