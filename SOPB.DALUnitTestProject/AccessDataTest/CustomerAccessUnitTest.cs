using System;
using System.Data;
using System.Data.SqlClient;
using BAL.AccessData;
using BAL.DataTables;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SOPB.Accounting.DAL.ConnectionManager;

namespace SOPB.DALUnitTestProject.AccessDataTest
{
    [TestClass]
    public class CustomerAccessUnitTest
    {
        [TestInitialize]
        public void Init()
        {
            ConnectionManager.SetConnection(UserSettings.UserName, UserSettings.Password);
        }
        [TestMethod]
        public void CustomerAccess_FillGlossary_TestMethod()
        {
            SqlConnection connection = ConnectionManager.Connection;
            connection.Open();
            CustomerAccess.FillDictionary();
            DataSet dataSet =(DataSet) CustomerAccess.GetData();
            Assert.IsTrue(dataSet.Tables["Gender"].Rows.Count > 0);
        }
        [TestMethod]
        public void CustomerAccess_FillCustomerData_TestMethod()
        {
            SqlConnection connection = ConnectionManager.Connection;
            connection.Open();
            CustomerAccess.FillDictionary();
            CustomerAccess.FillCustomerData();
            DataSet dataSet = (DataSet)CustomerAccess.GetData();
            Assert.IsTrue(dataSet.Tables["Customer"].Rows.Count > 0);
        }
        [TestMethod]
        [DataRow(1)]
        [DataRow(108)]
        public void CustomerAccess_GetCustomersByID_TestMethod(int id)
        {
            SqlConnection connection = ConnectionManager.Connection;
            connection.Open();
            
            CustomerAccess.FillDictionary();
            CustomerAccess.FillCustomerData();
            CustomerAccess.GetCustomersByID(id);
            DataSet dataSet = (DataSet)CustomerAccess.GetData();
            Assert.IsTrue(dataSet.Tables["Customer"].Rows.Count>0);
        }

        [TestMethod]
        public void CustomerAccess_GetCustomersByLastName_TestMethod()
        {
            SqlConnection connection = ConnectionManager.Connection;
            connection.Open();
            CustomerAccess.FillDictionary();
            CustomerAccess.GetCustomersByLastName("Bogodur");
            DataSet dataSet = (DataSet)CustomerAccess.GetData();
            Assert.IsTrue(dataSet.Tables["Customer"].Rows.Count > 0);
        }
        [TestMethod]
        public void CustomerAccess_GetCustomersByBirthdayBetween_TestMethod()
        {
            SqlConnection connection = ConnectionManager.Connection;
            connection.Open();
            CustomerAccess.FillDictionary();
            CustomerAccess.FillCustomerData();
            CustomerAccess.GetCustomersByBirthOfDay(new DateTime(1970, 1, 1), new DateTime(2016, 01, 01), "МЕЖДУ");
            DataSet dataSet = (DataSet)CustomerAccess.GetData();
            Assert.IsTrue(dataSet.Tables["Customer"].Rows.Count > 0);
        }
        [TestMethod]
        [DataRow(1992, 1, 1)]
        [DataRow(2000, 1, 1)]
        [DataRow(1918, 1, 1)]
        public void CustomerAccess_GetCustomersByBirthday_TestMethod(int year, int m, int day)
        {
            SqlConnection connection = ConnectionManager.Connection;
            connection.Open();
            CustomerAccess.FillDictionary();
            CustomerAccess.FillCustomerData();
            CustomerAccess.GetCustomersByBirthday(new DateTime(year, m, day));
            DataSet dataSet = (DataSet)CustomerAccess.GetData();
            Assert.IsTrue(dataSet.Tables["Customer"].Rows.Count > 0);
        }
        [TestMethod]
        [DataRow("Customer", "Gender", 1)]
        [DataRow("Customer", "Gender", 2)]
        [DataRow("Customer", "ApppTpr", 1)]
        [DataRow("Customer", "BenefitsCategory", 1)]
        public void CustomerAccess_GetCustomersByGlossary_TestMethod(string name, string glossary, int Id)
        {
            SqlConnection connection = ConnectionManager.Connection;
            connection.Open();
            CustomerAccess.FillDictionary();
            CustomerAccess.FillCustomerData();
            CustomerAccess.GetCustomersByGlossary(name, glossary, Id);
            DataSet dataSet = (DataSet)CustomerAccess.GetData();
            Assert.IsTrue(dataSet.Tables["Customer"].Rows.Count > 0);
        }
        [TestMethod]
        public void CustomerAccess_UpdateCustomers_TestMethod()
        {
            SqlConnection connection = ConnectionManager.Connection;
            connection.Open();
           
            CustomerAccess.FillDictionary();
            CustomerAccess.FillCustomerData();
            CustomerAccess.Update();
            DataSet dataSet = (DataSet)CustomerAccess.GetData();
            Assert.IsTrue(dataSet.Tables["Customer"].Rows.Count > 0);
        }
        [TestMethod]
        [DataRow("Ив", "Ленина", "Сыр","Вольная")]
        [DataRow("Сыр", "Вольная", "Ив", "Ленина")]
        [DataRow("Ив", "Hd", "Сыр", "Вольная")]
        [DataRow("Сыр", "Вольная", "Ив", "Ленина")]
        [DataRow("Ив", "Ленина", "Сыр", "Вольная")]
        [DataRow("Сыр", "Искры", "Ив", "Ленина")]
        public void CustomerAccess_GetByAddressAndByLastName_TestMethod(string lastname1, string addr1, string lastName2, string addr2)
        {
            SqlConnection connection = ConnectionManager.Connection;
            connection.Open();

            CustomerAccess.FillDictionary();
            CustomerAccess.FillCustomerData();
            CustomerAccess.GetCustomersByLastName(lastname1);
            CustomerAccess.GetCustomersByAddress(addr1);
            CustomerAccess.GetCustomersByLastName(lastName2);
            CustomerAccess.GetCustomersByAddress(addr2);
            DataSet dataSet = (DataSet)CustomerAccess.GetData();
            Assert.IsTrue(dataSet.Tables["Customer"].Rows.Count > 0);
        }

        [TestMethod]
        [DataRow("Вольная")]
        [DataRow("Ленина")]
        [DataRow("Ленина")]
        public void CustomerAccess_GetByAddress_TestMethod(string addr)
        {
            SqlConnection connection = ConnectionManager.Connection;
            connection.Open();

            CustomerAccess.FillDictionary();
            CustomerAccess.FillCustomerData();
            CustomerAccess.GetCustomersByAddress(addr);
            DataSet dataSet = (DataSet)CustomerAccess.GetData();
            Assert.IsTrue(dataSet.Tables["Customer"].Rows.Count > 0);
        }
    }
}
