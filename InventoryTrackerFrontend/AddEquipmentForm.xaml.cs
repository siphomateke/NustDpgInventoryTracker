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
    /// Interaction logic for AddEquipmentForm.xaml
    /// </summary>
    public partial class AddEquipmentForm : Page
    {
        public AddEquipmentForm()
        {
            InitializeComponent();
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

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadEquipmentConditions();
        }
    }
}
