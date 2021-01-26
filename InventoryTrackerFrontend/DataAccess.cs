using InventoryTrackerFrontend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;
using System.Data;

namespace InventoryTrackerFrontend
{
    class DataAccess
    {
        public IDbConnection connect()
        {
            return new SqlConnection(Helper.CnnVal("InventoryTrackerDB"));
        }
        public List<User> GetUsers()
        {
            using (var con = connect())
            {
                return con.Query<User>("select * from v_ApproverUsers").ToList();
            }
        }
        public List<Equipment> GetEquipment()
        {
            using (var con = connect())
            {
                return con.Query<Equipment>("dbo.spEquipment_List @UserId", new { UserId = 0 }).ToList();
            }
        }

        public User Login(string username, string password)
        {
            using (var con = connect())
            {
                return con.Query<User>("dbo.spUser_Login @Username, @Password", new { Username = username, Password = password }).ToList().FirstOrDefault();
            }
        }
    }
}
