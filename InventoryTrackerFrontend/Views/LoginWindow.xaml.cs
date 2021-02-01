using InventoryTrackerFrontend.Common;
using InventoryTrackerFrontend.Models;
using InventoryTrackerFrontend.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();

            LoginWindowViewModel vm = (LoginWindowViewModel)this.DataContext;
            vm.PropertyChanged += (sender, args) =>
            {
                switch (args.PropertyName)
                {
                    case "UsernameIsFocused":
                        if (vm.UsernameIsFocused)
                            usernameTextBox.Focus();
                        break;
                    case "PasswordISFocused":
                        if (vm.PasswordIsFocused)
                            passwordTextBox.Focus();
                        break;
                    case "IsHidden":
                        if (vm.IsHidden)
                            this.Hide();
                        break;
                    case "DialogResult":
                        this.DialogResult = vm.DialogResult;
                        break;
                    default:
                        break;
                }
            };
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            {
                ((LoginWindowViewModel)this.DataContext).Password = ((PasswordBox)sender).Password;
            }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
