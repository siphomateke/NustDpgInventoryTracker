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
    /// Interaction logic for ShopDetailsPage.xaml
    /// </summary>
    public partial class ShopDetailsPage : Page
    {
        public int ShopId { get; set; }
        public Shop Shop { get; set; }
        public ShopDetailsPage(int shopId)
        {
            InitializeComponent();
            this.ShopId = shopId;
            LoadDetails();
        }

        public async void LoadDetails()
        {
            this.busyIndicator.IsBusy = true;
            try
            {
                DataAccess db = new DataAccess();
                this.Shop = await db.GetShopDetails(ShopId);
                if (this.Shop != null)
                {
                    this._mainWrapper.DataContext = this.Shop;
                }
                else
                {
                    MessageBox.Show("Failed to find shop details. The shop might have been deleted.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            finally
            {
                this.busyIndicator.IsBusy = false;
            }
        }
    }
}
