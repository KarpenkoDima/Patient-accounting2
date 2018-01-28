using Microsoft.VisualStudio.TestTools.UnitTesting;
using SOPB.Accounting.DAL.TableAdapters.Glossary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOPB.Accounting.DAL.TableAdapters.Glossary.Tests
{
    [TestClass()]
    public class GenderTests
    {
        private SqlConnection conn;
        public void Mehod()
        {
            
        }
        [TestMethod()]
        public void FillTest()
        {
            ConnectionManager.ConnectionManager.SetConnection("Катя", "1");
            conn = ConnectionManager.ConnectionManager.Connection;
           // conn.Open();
            GenderTableAdapter gender = new GenderTableAdapter();
            gender.Connection = conn;
            int count = gender.Fill(new DataTable(""));
            Assert.IsTrue(count > 0);
        }

        [TestMethod()]
        public void UpdateFailedTest()
        {
            ConnectionManager.ConnectionManager.SetConnection("Катя", "1");
            conn = ConnectionManager.ConnectionManager.Connection;
            GenderTableAdapter gender = new GenderTableAdapter();
            gender.Connection = conn;
          gender.Update(new DataTable(""));
            Assert.IsNull(null);
        }
    }
}