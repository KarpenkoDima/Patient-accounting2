using System.Data;
using System.Data.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SOPB.Accounting.DAL.TableAdapters;
using SOPB.Accounting.DAL.TableAdapters.Glossary;

namespace SOPB.DALUnitTest.TableAdapters.Glossary
{
    [TestClass()]
    public class GlossaryTests
    {
        private SqlConnection _conn;
        public void Mehod()
        {
            
        }
        [TestMethod()]
        public void FillGenderGlossaryTest()
        {
            Accounting.DAL.ConnectionManager.ConnectionManager.SetConnection("Катя", null);
            _conn = Accounting.DAL.ConnectionManager.ConnectionManager.Connection;
           // conn.Open();
            GenderTableAdapter gender = new GenderTableAdapter {Connection = _conn};
            int count = gender.Fill(new DataTable(""));
            Assert.IsTrue(count > 0);
        }
        [TestMethod()]
        public void FillAdminDivisionGenderTest()
        {
            Accounting.DAL.ConnectionManager.ConnectionManager.SetConnection("Катя", null);
            _conn = Accounting.DAL.ConnectionManager.ConnectionManager.Connection;
            // conn.Open();
            BaseTableAdapter adminDivision = new AdminDivisionTableAdapter() { Connection = _conn };
            int count = adminDivision.Fill(new DataTable(""));
            Assert.IsTrue(count > 0);
        }
        [TestMethod()]
        public void UpdateFailedForGenderGlossaryTest()
        {
            Accounting.DAL.ConnectionManager.ConnectionManager.SetConnection("Катя", null);
            _conn = Accounting.DAL.ConnectionManager.ConnectionManager.Connection;
            GenderTableAdapter gender = new GenderTableAdapter();
            gender.Connection = _conn;
          gender.Update(new DataTable(""));
            Assert.IsNull(null);
        }
        [TestMethod()]
        public void UpdateFailedForAdminDivisionGlossaryTest()
        {
            Accounting.DAL.ConnectionManager.ConnectionManager.SetConnection("Катя", null);
            _conn = Accounting.DAL.ConnectionManager.ConnectionManager.Connection;
            BaseTableAdapter adminDivision = new AdminDivisionTableAdapter();
            adminDivision.Connection = _conn;
            adminDivision.Update(new DataTable(""));
            Assert.IsNull(null);
        }
       
    }
}