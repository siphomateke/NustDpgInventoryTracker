using InventoryTrackerFrontend.ViewModels;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace InventoryTrackerFrontend.Ninject
{
    public class ServiceLocator
    {
        private readonly IKernel kernel;

        public ServiceLocator()
        {
            kernel = new StandardKernel(new ServiceModule());
        }

        public HomePageViewModel HomePageViewModel
        {
            get { return kernel.Get<HomePageViewModel>(); }
        }

        public WelcomePageViewModel WelcomePageViewModel
        {
            get { return kernel.Get<WelcomePageViewModel>(); }
        }

        public LoginWindowViewModel LoginWindowViewModel
        {
            get { return kernel.Get<LoginWindowViewModel>(); }
        }
    }
}
