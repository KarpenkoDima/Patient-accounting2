using Microsoft.VisualStudio.TestTools.UnitTesting;
using SOPB.Accounting.DAL.ConnectionManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SOPB.DALUnitTest;

namespace SOPB.Accounting.DAL.ConnectionManager.Tests
{
    [TestClass()]
    public class ConnectionManagerTests
    {
        private static SqlConnection conn;
        [ClassInitialize()]
        public static void  ManagerTests(TestContext context)
        {
           
            DAL.ConnectionManager.ConnectionManager.SetConnection(UserSettings.UserName, UserSettings.Password);
            conn = ConnectionManager.Connection;
            //conn.Open();
        }
        [TestMethod()]
        public void GetConnectionSuccessTest()
        {
           // DAL.ConnectionManager.ConnectionManager.SetConnection("Катя", "1");
            //SqlConnection conn = DAL.ConnectionManager.ConnectionManager.Connection;
            Assert.AreNotEqual(conn.ConnectionString, string.Empty);
        }

        [TestMethod()]
        public void TestConnectionTest()
        {
            Assert.IsTrue(ConnectionManager.TestConnection(UserSettings.UserName, UserSettings.Password));
        }
        [TestMethod()]
        public void OpenConnectionTest()
        {
           //DAL.ConnectionManager.ConnectionManager.SetConnection("Катя", "1");
            conn = DAL.ConnectionManager.ConnectionManager.Connection;
            conn.Open();
            Assert.IsTrue(conn.State == ConnectionState.Open);
        }
        [TestMethod()]
        public void OpenCloseConnectionAnyMoreTest()
        {
           // DAL.ConnectionManager.ConnectionManager.SetConnection("Катя", "1");
            conn = DAL.ConnectionManager.ConnectionManager.Connection;

            int i = 10;
            while (--i > 0)
            {
                if(conn.State == ConnectionState.Closed)
                   conn.Open();
                Assert.IsTrue(conn.State == ConnectionState.Open);

               conn.Close();
                Assert.IsTrue(conn.State == ConnectionState.Closed);
            }
        }
        [TestMethod()]
        public void GetConnectionAndConnectionAnyMoreTest()
        {
           DAL.ConnectionManager.ConnectionManager.SetConnection(UserSettings.UserName,UserSettings.Password);
           SqlConnection conn = ConnectionManager.Connection;
           SqlConnection conn2 = ConnectionManager.Connection;
            conn.Open();
            int i = 10;
            Trace.Write(conn.ConnectionString);
            if (conn2.State == ConnectionState.Open)
            {
                conn2.Close();
                conn2.Open();
            }
            Assert.AreEqual(conn2.State, ConnectionState.Open);
            
        }

      

        [TestMethod()]
        public void CreateManyManConnectionAndConnectionAnyMoreTest()
        {
           // DAL.ConnectionManager.ConnectionManager.SetConnection("Катя", "1");
            //SqlConnection conn = DAL.ConnectionManager.ConnectionManager.Connection;
          

            int i = 10000;
            while (--i > 0)
            {
                SqlConnection conn2 = ConnectionManager.Connection;
            }
        }
    }
}