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

namespace InventoryTrackerFrontend.UserControls
{
    /// <summary>
    /// Interaction logic for EquipmentConditionDropdown.xaml
    /// </summary>
    public partial class EquipmentConditionDropdown : UserControl
    {
        public bool IsLoading { get; set; }
        public EquipmentConditionDropdown()
        {
            InitializeComponent();
        }

        // FIXME: Add selected prop

        private async void LoadEquipmentConditions()
        {
            this.IsLoading = true;
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
                this.IsLoading = false;
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadEquipmentConditions();
        }
    }
}
