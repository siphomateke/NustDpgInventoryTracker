using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryTrackerFrontend.ViewModels
{
    public class NewCategoryDialogViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
