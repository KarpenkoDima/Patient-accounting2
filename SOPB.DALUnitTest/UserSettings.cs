using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOPB.DALUnitTest
{
    static class UserSettings
    {
        public static string UserName = "Катя";
        public static string Password = "";

        public static SqlConnection InitConnection()
        {
            Accounting.DAL.ConnectionManager.ConnectionManager.SetConnection(UserName, Password);
            return Accounting.DAL.ConnectionManager.ConnectionManager.Connection;
        }
    }
}
