using InventoryTrackerFrontend.Common;
using InventoryTrackerFrontend.Models;
using InventoryTrackerFrontend.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace InventoryTrackerFrontend.ViewModels
{
    public class LoginWindowViewModel : BaseViewModel
    {
        public bool IsLoading { get; set; }
        public bool IsHidden { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool UsernameIsFocused { get; set; }
        public bool PasswordIsFocused { get; set; }
        public bool DialogResult { get; set; }

        public CommandHandler LoginCommand { get; set; }
        public CommandHandler ExitCommand { get; set; }
        public IMessageBoxService _messageBox;
        public LoginWindowViewModel(IMessageBoxService messageBoxService)
        {
            LoginCommand = new CommandHandler(() => Login());
            // ExitCommand = new CommandHandler(() => Exit());
            _messageBox = messageBoxService;
        }

        private async Task Login()
        {
            if (Username == "")
            {
                _messageBox.Show("Please enter a username", "Error", MessageType.Error);
                UsernameIsFocused = true;
                return;
            }
            if (Password == "")
            {
                _messageBox.Show("Please enter a password", "Error", MessageType.Error);
                PasswordIsFocused = true;
                return;
            }

            try
            {
                IsLoading = true;
                User loggedInUser = await UserManager.Login(Username, Password);
                IsLoading = false;

                bool loginSuccess = loggedInUser != null;
                if (loginSuccess)
                {
                    _messageBox.Show($"Successfully logged in as {loggedInUser.FirstName} {loggedInUser.LastName} ({loggedInUser.Username})", "Login successful", MessageType.Information);
                    DialogResult = true;
                    IsHidden = true;
                }
                else
                {
                    DialogResult = false;
                    _messageBox.Show("Invalid username or password", "Error", MessageType.Error);
                    // FIXME: Clear password
                }
            }
            catch (Exception ex)
            {
                _messageBox.Show(ex.Message, "Error", MessageType.Error);
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}
