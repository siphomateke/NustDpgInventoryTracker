using InventoryTrackerFrontend.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryTrackerFrontend.Models
{
    public class Equipment : BaseViewModel
    {
        public int EquipmentId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public string LocationInHome { get; set; }
        public bool Lost { get; set; }
        public int Age { get; set; }
        public DateTime DateOfPurchase { get; set; }
        public string ReceiptImage { get; set; }
        public string WarrantyExpiryDate { get; set; }
        public string WarrantyImage { get; set; }
        public int ConditionId { get; set; }
        public string Condition { get; set; }
        public int Price { get; set; }
        public string PriceWithCurrency
        {
            get
            {
                return $"N${Price}";
            }
        }

        public int ShopId { get; set; }
        public string Shop { get; set; }
        public string ShopTown { get; set; }
        public string ShopCountry { get; set; }

        public string ShopDetails
        {
            get
            {
                return $"{Shop}, {ShopTown}, {ShopCountry}";
            }
        }
        public List<Shop> Shops { get; set; }

        public List<EquipmentPrices> Prices { get; set; }

        public List<EquipmentCategory> Categories { get; set; }

        public string CategoryString
        {
            get
            {
                if (Categories != null)
                {
                    return String.Join(", ", Categories.Select(c => c.Name));
                }
                else
                {
                    return "";
                }
            }
        }
    }
}
