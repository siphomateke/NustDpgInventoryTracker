using InventoryTrackerFrontend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;
using System.Data;
using System.Windows;

namespace InventoryTrackerFrontend
{
    class DataAccess
    {
        public IDbConnection connect()
        {
            try
            {
                var con = new SqlConnection(Helper.CnnVal("InventoryTrackerDB"));
                return con;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                throw ex;
            }
        }

        public async Task<User> Login(string username, string password)
        {
            using (var con = connect())
            {
                return (await con.QueryAsync<User>("dbo.spUser_Login @Username, @Password", new { Username = username, Password = password })).ToList().FirstOrDefault();
            }
        }

        public async Task<List<EquipmentCondition>> GetEquipmentConditions()
        {
            using (var con = connect())
            {
                return (await con.QueryAsync<EquipmentCondition>("SELECT * FROM v_EquipmentCondition")).ToList();
            }
        }
        public async Task<List<Equipment>> GetEquipment()
        {
            using (var con = connect())
            {
                return (await con.QueryAsync<Equipment>("dbo.spEquipment_List @UserId", new { UserId = UserManager.LoggedInUser.UserId })).ToList();
            }
        }

        public async Task<List<Shop>> GetEquipmentAvailableShops(int equipmentId)
        {
            using (var con = connect())
            {
                return (await con.QueryAsync<Shop>("dbo.spEquipment_AvailableShops @UserId, @EquipmentId", new { UserId = UserManager.LoggedInUser.UserId, EquipmentId = equipmentId })).ToList();
            }
        }

        public async Task<List<EquipmentPrices>> GetEquipmentPrices(int equipmentId)
        {
            using (var con = connect())
            {
                return (await con.QueryAsync<EquipmentPrices, Shop, EquipmentPrices>(
                            sql: "dbo.spEquipment_GetPrices @UserId, @EquipmentId",
                            map: (price, shop) => { price.Shop = shop; return price; },
                            param: new { UserId = UserManager.LoggedInUser.UserId, EquipmentId = equipmentId },
                            splitOn: "ShopId"
                        )).ToList();
            }
        }

        public async Task<Equipment> GetEquipmentDetails(int equipmentId)
        {
            using (var con = connect())
            {
                Equipment equipment = (await con.QueryAsync<Equipment>("dbo.spEquipment_Details @EquipmentId, @UserId", new { UserId = UserManager.LoggedInUser.UserId, EquipmentId = equipmentId })).FirstOrDefault();
                if (equipment != null)
                {
                    var shops = await GetEquipmentAvailableShops(equipmentId);
                    equipment.Shops = shops;

                    var prices = await GetEquipmentPrices(equipmentId);
                    equipment.Prices = prices;
                }
                return equipment;
            }
        }
    }
}
