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

namespace InventoryTrackerFrontend
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private async void loginButton_Click(object sender, RoutedEventArgs e)
        {
            if (usernameTextBox.Text == "")
            {
                MessageBox.Show("Please enter a username", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                usernameTextBox.Focus();
                return;
            }
            if (passwordBox.Password == "")
            {
                MessageBox.Show("Please enter a password", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                passwordBox.Focus();
                return;
            }

            try
            {
                busyIndicator.IsBusy = true;
                User loggedInUser = await UserManager.Login(usernameTextBox.Text, passwordBox.Password);
                busyIndicator.IsBusy = false;

                bool loginSuccess = loggedInUser != null;
                if (loginSuccess)
                {
                    MessageBox.Show($"Successfully logged in as {loggedInUser.FirstName} {loggedInUser.LastName} ({loggedInUser.Username})", "Login successful", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid username or password", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    usernameTextBox.Clear();
                    passwordBox.Clear();
                }
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

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
