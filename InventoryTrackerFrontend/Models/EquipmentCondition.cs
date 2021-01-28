using InventoryTrackerFrontend.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryTrackerFrontend.Models
{
    class EquipmentCondition : BaseViewModel
    {
        public int ConditionId { get; set; }
        public string Name { get; set; }
    }
}
