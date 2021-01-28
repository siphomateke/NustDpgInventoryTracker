using InventoryTrackerFrontend.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryTrackerFrontend.ViewModels
{
    public class HomePageViewModel : BaseViewModel
    {
        public int SelectedEquipmentId { get; set; }
        public bool AnyEquipmentSelected { get; set; }
        public List<Equipment> Equipment { get; set; }
        public List<EquipmentChange> EquipmentChanges { get; set; }
        public List<BasicEquipment> EquipmentToBuy { get; set; }
        public bool AnyEquipmentChangeSelected { get; set; }
        public int SelectedEquipmentChangeId { get; set; }
    }
}
