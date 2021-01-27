using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryTrackerFrontend.Models
{
    public class Shop
    {
        public int ShopId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Comments { get; set; }
        public string StreetAdress { get; set; }
        public string Town { get; set; }
        public string Country { get; set; }
        public string Location
        {
            get
            {
                return $"{StreetAdress}, {Town}, {Country}";
            }
        }
        public string BasicDetails
        {
            get
            {
                return $"{Name}, {StreetAdress}";
            }
        }
    }
}
