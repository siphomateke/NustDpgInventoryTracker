using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryTrackerFrontend.Services
{
    public interface INavigationPage
    {
        Type PageType { get; set; }
    }
}
