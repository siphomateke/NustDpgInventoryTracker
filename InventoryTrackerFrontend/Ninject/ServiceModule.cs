using InventoryTrackerFrontend.Services;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace InventoryTrackerFrontend.Ninject
{
    public class ServiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IMessageBoxService>().To<MessageBoxService>();
        }
    }
}
