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

namespace InventoryTrackerFrontend.Views
{
    /// <summary>
    /// Interaction logic for ViewEquipmentForm.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        HomePageViewModel vm;

        public HomePage()
        {
            InitializeComponent();
            vm = (HomePageViewModel)DataContext;
        }

        private void ViewDetailsButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EquipmentDetailsPage(vm.SelectedEquipmentId));
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EditEquipmentForm(vm.SelectedEquipmentId));
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EditEquipmentForm());
        }

        private void equipmentDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            vm.EquipmentSelectionChangedCommand.Execute(e.AddedItems);
        }

        private void EquipmentChangeDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            vm.EquipmentChangeSelectionChangedCommand.Execute(e.AddedItems);
        }
    }
}
