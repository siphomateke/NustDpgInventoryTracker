using InventoryTrackerFrontend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryTrackerFrontend
{
    public static class UserManager
    {
        public static User LoggedInUser { get; set; }

        public static async Task<User> Login(string username, string password)
        {
            DataAccess db = new DataAccess();
            var user = await db.Login(username, password);
            if (user != null)
            {
                LoggedInUser = user;
            }
            return user;
        }
        public static void Logout()
        {
            LoggedInUser = null;
            LoginWindow win = new LoginWindow();
            win.Show();
        }
    }
}
