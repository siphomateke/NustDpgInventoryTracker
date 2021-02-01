using InventoryTrackerFrontend.Common;
using InventoryTrackerFrontend.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryTrackerFrontend.ViewModels
{
    public class WelcomePageViewModel : BaseViewModel
    {
        public CommandHandler LoadedCommand { get; set; }
        public CommandHandler RefreshCommand { get; set; }
        public CommandHandler LoginCommand { get; set; }
        public CommandHandler LogoutCommand { get; set; }
        public CommandHandler RegisterCommand { get; set; }

        public bool CanLogin { get; set; }
        public bool CanRegister { get; set; }
        public bool CanLogout { get; set; }

        public WelcomePageViewModel()
        {
            LoadedCommand = new CommandHandler(() => RefreshButtons());
            RefreshCommand = new CommandHandler(() => RefreshButtons());
            LoginCommand = new CommandHandler(() => Login(), () => CanLogin);
            LogoutCommand = new CommandHandler(() => Logout(), () => CanLogout);
            RegisterCommand = new CommandHandler(() => Register(), () => CanRegister);
        }

        private void RefreshButtons()
        {
            CanLogin = !UserManager.LoggedIn;
            CanRegister = !UserManager.LoggedIn;
            CanLogout = UserManager.LoggedIn;
        }

        public delegate void BasicEvent();
        public event BasicEvent LoginEvent;
        public event BasicEvent RegisterEvent;

        private void Login()
        {
            LoginEvent?.Invoke();
        }

        private void Register()
        {
            RegisterEvent?.Invoke();
        }

        private void Logout()
        {
            UserManager.Logout();
            RefreshButtons();
        }
    }

}
