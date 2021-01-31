using Dapper;
using InventoryTrackerFrontend.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace InventoryTrackerFrontend.Common
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
                return (await con.QueryAsync<User>("dbo.spUser_Login", new { Username = username, Password = password }, commandType: CommandType.StoredProcedure)).ToList().FirstOrDefault();
            }
        }

        public async Task<User> RegisterUser(User user)
        {
            using (var con = connect())
            {
                var p = new DynamicParameters(new
                {
                    AdminUserId = UserManager.LoggedInUser.UserId,
                    user.Username,
                    user.Password,
                    user.RoleId,
                    user.FirstName,
                    user.LastName,
                    user.Email
                });
                return (await con.QueryAsync<User>("dbo.spUser_Register", p, commandType: CommandType.StoredProcedure)).ToList().FirstOrDefault();
            }
        }

        public async Task<List<UserRole>> GetUserRoles()
        {
            using (var con = connect())
            {
                return (await con.QueryAsync<UserRole>("SELECT * FROM v_UserRoles")).ToList();
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
                return (await con.QueryAsync<Equipment>("dbo.spEquipment_List", new { UserManager.LoggedInUser.UserId }, commandType: CommandType.StoredProcedure)).ToList();
            }
        }

        public async Task<List<Shop>> GetEquipmentAvailableShops(int equipmentId)
        {
            using (var con = connect())
            {
                return (await con.QueryAsync<Shop>("dbo.spEquipment_AvailableShops", new { UserManager.LoggedInUser.UserId, EquipmentId = equipmentId }, commandType: CommandType.StoredProcedure)).ToList();
            }
        }

        public async Task<List<Shop>> GetAllShops()
        {
            using (var con = connect())
            {
                return (await con.QueryAsync<Shop>("SELECT * FROM dbo.v_Shop")).ToList();
            }
        }

        public async Task<List<EquipmentPrices>> GetEquipmentPrices(int equipmentId)
        {
            using (var con = connect())
            {
                return (await con.QueryAsync<EquipmentPrices, Shop, EquipmentPrices>(
                            sql: "dbo.spEquipment_GetPrices",
                            map: (price, shop) => { price.Shop = shop; return price; },
                            param: new { UserManager.LoggedInUser.UserId, EquipmentId = equipmentId },
                            splitOn: "ShopId",
                            commandType: CommandType.StoredProcedure
                        )).ToList();
            }
        }

        public async Task<List<EquipmentCategory>> GetEquipmentCategories(int equipmentId)
        {
            using (var con = connect())
            {
                return (await con.QueryAsync<EquipmentCategory>("dbo.spEquipment_GetCategories", new { UserManager.LoggedInUser.UserId, EquipmentId = equipmentId }, commandType: CommandType.StoredProcedure)).ToList();
            }
        }

        public async Task<Equipment> GetEquipmentDetails(int equipmentId)
        {
            using (var con = connect())
            {
                Equipment equipment = (await con.QueryAsync<Equipment>("dbo.spEquipment_Details", new { UserManager.LoggedInUser.UserId, EquipmentId = equipmentId }, commandType: CommandType.StoredProcedure)).FirstOrDefault();
                if (equipment != null)
                {
                    equipment.Shops = await GetEquipmentAvailableShops(equipmentId);
                    equipment.Prices = await GetEquipmentPrices(equipmentId);
                    equipment.Categories = await GetEquipmentCategories(equipmentId);
                }
                return equipment;
            }
        }

        public async Task<List<EquipmentChange>> GetEquipmentChanges(bool showOwnChangesOnly)
        {
            using (var con = connect())
            {
                return (await con.QueryAsync<EquipmentChange>("dbo.spEquipmentChange_List", new { UserManager.LoggedInUser.UserId, OwnOnly = showOwnChangesOnly }, commandType: CommandType.StoredProcedure)).ToList();
            }
        }

        public async Task<List<BasicEquipment>> GetAllEquipmentToBuy()
        {
            using (var con = connect())
            {
                return (await con.QueryAsync<BasicEquipment>("dbo.spEquipment_ListAllToBuy", new { UserManager.LoggedInUser.UserId }, commandType: CommandType.StoredProcedure)).ToList();
            }
        }

        public async Task<Shop> GetShopDetails(int shopId)
        {
            using (var con = connect())
            {
                return (await con.QueryAsync<Shop>("dbo.spShop_Details", new { ShopId = shopId }, commandType: CommandType.StoredProcedure)).FirstOrDefault();
            }
        }

        public async Task<int> AddEquipment(Equipment equipment)
        {
            var p = new DynamicParameters(new
            {
                equipment.Name,
                equipment.Description,
                equipment.Quantity,
                equipment.LocationInHome,
                equipment.Lost,
                equipment.ConditionId,
                equipment.Age,
                equipment.DateOfPurchase,
                equipment.ReceiptImage,
                equipment.WarrantyExpiryDate,
                equipment.WarrantyImage
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
                UserManager.LoggedInUser.UserId,
                equipment.EquipmentId,
                equipment.Name,
                equipment.Description,
                equipment.Quantity,
                equipment.LocationInHome,
                equipment.Lost,
                equipment.ConditionId,
                equipment.Age,
                equipment.DateOfPurchase,
                equipment.ReceiptImage,
                equipment.WarrantyExpiryDate,
                equipment.WarrantyImage
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
                await con.ExecuteAsync("dbo.spEquipment_UndoChange", new { UserManager.LoggedInUser.UserId, EquipmentChangeId = equipmentChangeId }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task ApproveEquipmentChange(int equipmentChangeId)
        {
            using (var con = connect())
            {
                await con.ExecuteAsync("dbo.spEquipmentChange_SetApproval", new
                {
                    UserManager.LoggedInUser.UserId,
                    EquipmentChangeId = equipmentChangeId,
                    Approved = 1
                }, commandType: CommandType.StoredProcedure);
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

        public async Task AddCategoryToEquipment(int categoryId, int equipmentId)
        {
            using (var con = connect())
            {
                await con.ExecuteAsync("dbo.spEquipmentCategory_Add", new { CategoryId = categoryId, EquipmentId = equipmentId }, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
