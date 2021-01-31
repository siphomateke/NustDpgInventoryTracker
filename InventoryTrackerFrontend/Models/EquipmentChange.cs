using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryTrackerFrontend.Models
{
    public class EquipmentChange : Equipment
    {
        public int EquipmentChangeId { get; set; }
        public int ChangedByUserId { get; set; }
        public string ChangedByUserUsername { get; set; }
        public int ChangeApprovedByUserId { get; set; }
        public string ChangeApprovedByUsername { get; set; }
        public bool ChangeApproved { get; set; }
        public DateTime ChangeDate { get; set; }
    }
}
