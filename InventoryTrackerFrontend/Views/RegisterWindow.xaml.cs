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
using System.Windows.Shapes;

namespace InventoryTrackerFrontend.Views
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public User User { get; set; }
        public RegisterWindow()
        {
            InitializeComponent();
            User = new User();
            this._mainForm.DataContext = User;
            GetUserRolesAsync();
        }

        private async Task GetUserRolesAsync()
        {
            this.busyIndicator.IsBusy = true;
            try
            {
                DataAccess db = new DataAccess();
                List<UserRole> roles = await db.GetUserRoles();
                rolesComboBox.ItemsSource = roles;
                rolesComboBox.IsEnabled = true;
            }
            catch (Exception ex)
            {
                rolesComboBox.IsEnabled = false;
                MessageBox.Show(ex.Message, "Error loading roles", MessageBoxButton.OK, MessageBoxImage.Error);
                throw;
            }
            finally
            {
                this.busyIndicator.IsBusy = false;
            }
        }

        private async void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            this.busyIndicator.IsBusy = true;
            try
            {
                DataAccess db = new DataAccess();
                User.RoleId = (int)rolesComboBox.SelectedValue;
                User.Password = passwordBox.Password;
                await db.RegisterUser(User);
                this.Hide();
                MessageBox.Show("Registered new user successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error registereding new user", MessageBoxButton.OK, MessageBoxImage.Error);
                throw;
            }
            finally
            {
                this.busyIndicator.IsBusy = false;
            }
        }
    }
}
