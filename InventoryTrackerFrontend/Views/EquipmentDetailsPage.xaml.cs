using InventoryTrackerFrontend.Common;
using InventoryTrackerFrontend.Models;
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

namespace InventoryTrackerFrontend.Views
{
    /// <summary>
    /// Interaction logic for EquipmentDetailsPage.xaml
    /// </summary>
    public partial class EquipmentDetailsPage : Page
    {
        public int EquipmentId { get; set; }
        public Equipment Equipment { get; set; }

        public EquipmentDetailsPage(int equipmentId)
        {
            InitializeComponent();
            this.EquipmentId = equipmentId;
            LoadDetails();
        }

        public async void LoadDetails()
        {
            this.busyIndicator.IsBusy = true;
            try
            {
                DataAccess db = new DataAccess();
                this.Equipment = await db.GetEquipmentDetails(EquipmentId);
                if (this.Equipment != null)
                {
                    this._equipmentDetailsWrapper.DataContext = this.Equipment;
                }
                else
                {
                    MessageBox.Show("Failed to find equipment details. The equipment might have been deleted.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            finally
            {
                this.busyIndicator.IsBusy = false;
            }
        }

        private void ViewShopButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ShopDetailsPage(Equipment.ShopId));
        }
    }
}
