using System;
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
            Tables tables = new Tables();
            CustomerAccess.FillDictionary(tables.DispancerDataSet);
            Assert.IsTrue(tables.GenderDataTable.Rows.Count > 0);
        }
        [TestMethod]
        public void CustomerAccess_FillCustomerData_TestMethod()
        {
            SqlConnection connection = ConnectionManager.Connection;
            connection.Open();
            Tables tables = new Tables();
            CustomerAccess.FillDictionary(tables.DispancerDataSet);
            CustomerAccess.FillCustomerData(tables.DispancerDataSet);
            Assert.IsTrue(tables.CustomerDataTable.Rows.Count > 0);
        }
        [TestMethod]
        public void CustomerAccess_GetCustomersByID_TestMethod()
        {
            SqlConnection connection = ConnectionManager.Connection;
            connection.Open();
            Tables tables = new Tables();
            CustomerAccess.FillDictionary(tables.DispancerDataSet);
            CustomerAccess.FillCustomerData(tables.DispancerDataSet);
            CustomerAccess.GetCustomersByID(tables.CustomerDataTable, 1);
            Assert.IsTrue(tables.CustomerDataTable.Rows.Count>0);
        }

        [TestMethod]
        public void CustomerAccess_GetCustomersByLastName_TestMethod()
        {
            SqlConnection connection = ConnectionManager.Connection;
            connection.Open();
            Tables tables = new Tables();
            CustomerAccess.FillDictionary(tables.DispancerDataSet);
            CustomerAccess.FillCustomerData(tables.DispancerDataSet);
            CustomerAccess.GetCustomersByLastName(tables.CustomerDataTable, "Bogodur");
            Assert.IsTrue(tables.CustomerDataTable.Rows.Count > 0);
        }
        [TestMethod]
        public void CustomerAccess_GetCustomersByBirthdayBetween_TestMethod()
        {
            SqlConnection connection = ConnectionManager.Connection;
            connection.Open();
            Tables tables = new Tables();
            CustomerAccess.FillDictionary(tables.DispancerDataSet);
            CustomerAccess.FillCustomerData(tables.DispancerDataSet);
            CustomerAccess.GetCustomersByBirthdayBetween(tables.CustomerDataTable, new DateTime(1970,1,1), new DateTime(2016,01,01));
            Assert.IsTrue(tables.CustomerDataTable.Rows.Count > 0);
        }
        [TestMethod]
        public void CustomerAccess_GetCustomersByBirthday_TestMethod()
        {
            SqlConnection connection = ConnectionManager.Connection;
            connection.Open();
            Tables tables = new Tables();
            CustomerAccess.FillDictionary(tables.DispancerDataSet);
            CustomerAccess.FillCustomerData(tables.DispancerDataSet);
            CustomerAccess.GetCustomersByBirthday(tables.CustomerDataTable, new DateTime(1992, 1, 1));
            Assert.IsTrue(tables.CustomerDataTable.Rows.Count > 0);
        }
        [TestMethod]
        public void CustomerAccess_GetCustomersByGlossary_TestMethod()
        {
            SqlConnection connection = ConnectionManager.Connection;
            connection.Open();
            Tables tables = new Tables();
            CustomerAccess.FillDictionary(tables.DispancerDataSet);
            CustomerAccess.FillCustomerData(tables.DispancerDataSet);
            CustomerAccess.GetCustomersByGlossary(tables.CustomerDataTable, "Gender", 2);
            Assert.IsTrue(tables.CustomerDataTable.Rows.Count > 0);
        }
    }
}
