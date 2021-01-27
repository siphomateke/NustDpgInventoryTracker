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

namespace InventoryTrackerFrontend
{
    /// <summary>
    /// Interaction logic for ViewEquipmentForm.xaml
    /// </summary>
    public partial class ViewEquipmentForm : Page
    {
        List<Equipment> equipment = new List<Equipment>();

        public ViewEquipmentForm()
        {
            InitializeComponent();
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            DataAccess db = new DataAccess();
            equipment = db.GetEquipment();

            dataGrid.ItemsSource = equipment;
        }
    }
}
