using Dapper;
using InventoryTrackerFrontend.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<List<EquipmentCategory>> GetEquipmentCategories(int equipmentId)
        {
            using (var con = connect())
            {
                return (await con.QueryAsync<EquipmentCategory>("dbo.spEquipment_GetCategories @EquipmentId, @UserId", new { UserId = UserManager.LoggedInUser.UserId, EquipmentId = equipmentId })).ToList();
            }
        }

        public async Task<Equipment> GetEquipmentDetails(int equipmentId)
        {
            using (var con = connect())
            {
                Equipment equipment = (await con.QueryAsync<Equipment>("dbo.spEquipment_Details @EquipmentId, @UserId", new { UserId = UserManager.LoggedInUser.UserId, EquipmentId = equipmentId })).FirstOrDefault();
                if (equipment != null)
                {
                    equipment.Shops = await GetEquipmentAvailableShops(equipmentId);
                    equipment.Prices = await GetEquipmentPrices(equipmentId);
                    equipment.Categories = await GetEquipmentCategories(equipmentId);
                }
                return equipment;
            }
        }

        public async Task<List<EquipmentChange>> GetEquipmentChanges()
        {
            using (var con = connect())
            {
                return (await con.QueryAsync<EquipmentChange>("dbo.spEquipmentChange_List @UserId", new { UserId = UserManager.LoggedInUser.UserId })).ToList();
            }
        }

        public async Task<List<BasicEquipment>> GetAllEquipmentToBuy()
        {
            using (var con = connect())
            {
                return (await con.QueryAsync<BasicEquipment>("dbo.spEquipment_ListAllToBuy @UserId", new { UserId = UserManager.LoggedInUser.UserId })).ToList();
            }
        }

        public async Task<Shop> GetShopDetails(int shopId)
        {
            using (var con = connect())
            {
                return (await con.QueryAsync<Shop>("dbo.spShop_Details @ShopId", new { ShopId = shopId })).FirstOrDefault();
            }
        }

        public async Task<int> AddEquipment(Equipment equipment)
        {
            var p = new DynamicParameters(new
            {
                Name = equipment.Name,
                Description = equipment.Description,
                Quantity = equipment.Quantity,
                LocationInHome = equipment.LocationInHome,
                Lost = equipment.Lost,
                ConditionId = equipment.ConditionId,
                Age = equipment.Age,
                DateOfPurchase = equipment.DateOfPurchase,
                ReceiptImage = equipment.ReceiptImage,
                WarrantyExpiryDate = equipment.WarrantyExpiryDate,
                WarrantyImage = equipment.WarrantyImage
                //ShopId = (int?)null,
                //EquipmentPrice = (int?)null,
                //DatePriceChecked = (DateTime?)null,
                //IsOriginalPurchase = (bool?)null
            });
            p.Add("EquipmentId", dbType: DbType.Int32, direction: ParameterDirection.Output);
            using (var con = connect())
            {
                string sql = "dbo.spEquipment_Add";
                await con.QueryAsync(sql, p, commandType: CommandType.StoredProcedure);
            }
            return p.Get<int>("EquipmentId");
        }

        public async Task<int> RequestEquipmentChange(Equipment equipment)
        {
            var p = new DynamicParameters(new
            {
                UserId = UserManager.LoggedInUser.UserId,
                EquipmentId = equipment.EquipmentId,
                Name = equipment.Name,
                Description = equipment.Description,
                Quantity = equipment.Quantity,
                LocationInHome = equipment.LocationInHome,
                Lost = equipment.Lost,
                ConditionId = equipment.ConditionId,
                Age = equipment.Age,
                DateOfPurchase = equipment.DateOfPurchase,
                ReceiptImage = equipment.ReceiptImage,
                WarrantyExpiryDate = equipment.WarrantyExpiryDate,
                WarrantyImage = equipment.WarrantyImage
            });
            p.Add("EquipmentChangeId", dbType: DbType.Int32, direction: ParameterDirection.Output);
            using (var con = connect())
            {
                string sql = "dbo.spEquipment_RequestChange";
                await con.QueryAsync(sql, p, commandType: CommandType.StoredProcedure);
            }
            return p.Get<int>("EquipmentChangeId");
        }

        public async Task UndoEquipmentChange(int equipmentChangeId)
        {
            using (var con = connect())
            {
                await con.ExecuteAsync("dbo.spEquipment_UndoChange", new { UserId = UserManager.LoggedInUser.UserId, EquipmentChangeId = equipmentChangeId }, commandType: CommandType.StoredProcedure);
            }
        }
        public async Task<List<EquipmentCategory>> GetCategories()
        {
            using (var con = connect())
            {
                var categories = (await con.QueryAsync<EquipmentCategory>("SELECT * FROM dbo.v_Category")).ToList();
                return categories;
            }
        }
        public async Task<int> AddCategory(string name, string description)
        {
            var p = new DynamicParameters(new { Name = name, Description = description });
            p.Add("CategoryId", dbType: DbType.Int32, direction: ParameterDirection.Output);
            using (var con = connect())
            {
                await con.ExecuteAsync("dbo.spCategory_Add", p, commandType: CommandType.StoredProcedure);
            }
            return p.Get<int>("CategoryId");
        }
    }
}
