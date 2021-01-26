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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Equipment> equipment = new List<Equipment>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            DataAccess db = new DataAccess();
            equipment = db.GetEquipment();

            dataGrid.ItemsSource = equipment;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoginWindow win = new LoginWindow();
            win.Show();
        }
    }
}
