using InventoryTrackerFrontend.Common;
using InventoryTrackerFrontend.ViewModels;
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
        WelcomePageViewModel vm;
        public WelcomePage()
        {
            InitializeComponent();
            vm = this.DataContext as WelcomePageViewModel;

            vm.LoginEvent += Login;
            vm.RegisterEvent += Register;
        }

        private void Login()
        {
            LoginWindow win = new LoginWindow();
            var result = win.ShowDialog();

            if (UserManager.LoggedIn)
            {
                NavigationService.Navigate(new HomePage());
            }
            vm.RefreshCommand.Execute();
        }

        private void Register()
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
            vm.RefreshCommand.Execute();
        }
    }
}
