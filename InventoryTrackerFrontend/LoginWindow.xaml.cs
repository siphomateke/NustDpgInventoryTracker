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

        private void loginButton_Click(object sender, RoutedEventArgs e)
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

            DataAccess db = new DataAccess();

            User loggedInUser = db.Login(usernameTextBox.Text, passwordBox.Password);

            bool loginSuccess = loggedInUser != null;
            if (loginSuccess)
            {
                // Save the logged in user
                MessageBox.Show(loggedInUser.FirstName + " " + loggedInUser.LastName);
                MessageBox.Show($"Successfully logged in as {loggedInUser.FirstName} {loggedInUser.LastName} ({loggedInUser.Username})", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid username or password", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                usernameTextBox.Clear();
                passwordBox.Clear();
            }
        }
    }
}
