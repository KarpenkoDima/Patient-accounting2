using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOPB.Accounting.DAL.ConnectionManager;

namespace BAL.ORM
{
    public static class LogInApplication
    {
        public static bool LogIn(string userName, string password)
        {
            if (ConnectionManager.TestConnection(userName, password))
            {
                ConnectionManager.SetConnection(userName, password);
                return true;
            }
            return false;
        }
        
    }
}
