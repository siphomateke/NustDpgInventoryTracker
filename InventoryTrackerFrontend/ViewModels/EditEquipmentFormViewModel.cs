using InventoryTrackerFrontend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryTrackerFrontend.ViewModels
{
    public class EditEquipmentFormViewModel : BaseViewModel
    {
        public bool IsNew { get; set; }
        public int? EquipmentId { get; set; }
        public Equipment Equipment { get; set; }
        public List<EquipmentCategory> AllCategories { get; set; }
    }
}
