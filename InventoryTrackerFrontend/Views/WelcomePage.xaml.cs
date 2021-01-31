using InventoryTrackerFrontend.Common;
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
    /// Interaction logic for WelcomePage.xaml
    /// </summary>
    public partial class WelcomePage : Page
    {
        public WelcomePage()
        {
            InitializeComponent();
            RefreshButtons();
        }

        private void RefreshButtons()
        {
            LoginButton.IsEnabled = !UserManager.LoggedIn;
            RegisterButton.IsEnabled = !UserManager.LoggedIn;
            LogoutButton.IsEnabled = UserManager.LoggedIn;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow win = new LoginWindow();
            win.ShowDialog();

            if (UserManager.LoggedIn)
            {
                NavigationService.Navigate(new HomePage());
            }
            RefreshButtons();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            if (!UserManager.LoggedIn)
            {
                MessageBox.Show("Only the owner can add new users. You have been redirected to the login form.");
                LoginWindow win = new LoginWindow();
                win.ShowDialog();
            }
            if (UserManager.LoggedIn)
            {
                RegisterWindow win = new RegisterWindow();
                win.ShowDialog();
            }
            RefreshButtons();
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            UserManager.Logout();
            RefreshButtons();
        }
    }
}
