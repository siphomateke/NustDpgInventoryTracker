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
    }
}
