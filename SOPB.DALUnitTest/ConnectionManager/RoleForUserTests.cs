using Microsoft.VisualStudio.TestTools.UnitTesting;
using SOPB.Accounting.DAL.ConnectionManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOPB.Accounting.DAL.ConnectionManager.Tests
{
    [TestClass()]
    public class RoleForUserTests
    {
        [TestMethod()]
        public void GetRoleForUserTest()
        {
            string role = RoleForUser.GetRoleForUser("Катя", "");

            Assert.IsNotNull(role);
        }

      
    }
}