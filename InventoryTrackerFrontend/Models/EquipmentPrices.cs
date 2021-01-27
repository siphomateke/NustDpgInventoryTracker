using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryTrackerFrontend.Models
{
    public class EquipmentPrices
    {
        public int EquipmentId { get; set; }
        public int Price { get; set; }
        public string PriceWithCurrency
        {
            get
            {
                return $"N${Price}";
            }
        }
        public DateTime DatePriceChecked { get; set; }
        public Shop Shop { get; set; }

        public string ShopName
        {
            get
            {
                return Shop.BasicDetails;
            }
        }
    }
}
