using InventoryTrackerFrontend.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryTrackerFrontend.Models
{
    public class UserRole : BaseViewModel
    {
        public int RoleId { get; set; }
        public string Name { get; set; }
    }
}
