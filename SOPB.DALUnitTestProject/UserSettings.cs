using System.Data.SqlClient;

namespace SOPB.DALUnitTestProject
{
    static class UserSettings
    {
        public static string UserName = "Катя";
        public static string Password = "1";

        public static SqlConnection InitConnection()
        {
            Accounting.DAL.ConnectionManager.ConnectionManager.SetConnection(UserName, Password);
            return Accounting.DAL.ConnectionManager.ConnectionManager.Connection;
        }
    }
}
