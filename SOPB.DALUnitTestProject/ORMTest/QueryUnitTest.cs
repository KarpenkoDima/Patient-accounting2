using System;
using System.Data;
using System.Data.SqlClient;
using BAL.AccessData;
using BAL.DataTables;
using BAL.ORM;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SOPB.Accounting.DAL.ConnectionManager;

namespace SOPB.DALUnitTestProject.ORMTest
{
    [TestClass]
    public class QueryUnitTest
    {
        [TestInitialize]
        public void Init()
        {
            ConnectionManager.SetConnection(UserSettings.UserName, UserSettings.Password);
        }
        [TestMethod]
        //[DataRow("Gender", 2)]
        //[DataRow("Gender", 1)]
        [DataRow("ApppTpr", 2)]
        [DataRow("BenefitsCategory", 1)]
        public void Query_GlossaryQuery_ExecuteMethod_TestMethod(string nameGlossary, int id)
        {
            SqlConnection connection = ConnectionManager.Connection;
            connection.Open();
            CustomerAccess.FillDictionary();
            CustomerAccess.FillCustomerData();
            DataSet dataSet = (DataSet)CustomerAccess.GetData();
            NewCriteria<int> criteria = new NewCriteria<int>("=", nameGlossary, id);
            GlossaryQuery glossary = new GlossaryQuery();
            glossary.Criterias(criteria);
            dataSet = (DataSet)glossary.Execute();
            Assert.IsTrue(dataSet.Tables["Customer"].Rows.Count > 0);
        }
        //[TestMethod]
        //public void Query_GlossaryQuery_ExecuteMethodWrongParametrValue_TestMethod()
        //{
        //    SqlConnection connection = ConnectionManager.Connection;
        //    connection.Open();
        //    DataSet dataSet = new DataSet();
        //    NewCriteria<int> criteria = new NewCriteria<int>(Predicate.Equals, "Gender", -2);
        //    GlossaryQuery glossary = new GlossaryQuery();
        //    glossary.Criterias(criteria);
        //    dataSet = (DataSet)glossary.Execute();

        //    Assert.IsTrue(dataSet.Tables["Customer"].Rows.Count <= 0);
        //}

        [TestMethod]
        public void Query_CustomerQuery_ExecuteMethod_TestMethod()
        {
            SqlConnection connection = ConnectionManager.Connection;
            connection.Open();
            CustomerAccess.FillDictionary();
            DataSet dataSet;
            NewCriteria<int> criteria = new NewCriteria<int>("=", "ID", 1 );
            CustomerQuery<int> customerQuery = new CustomerQuery<int>();
            customerQuery.Criterias(criteria);
            dataSet = (DataSet)customerQuery.Execute();
            Assert.IsTrue(dataSet.Tables["Customer"].Rows.Count > 0);
        }

        [TestMethod]
        public void Query_CustomerQuery_ExecuteMethod_ParametrWrongId_TestMethod()
        {
            SqlConnection connection = ConnectionManager.Connection;
            connection.Open();
            
            CustomerAccess.FillDictionary();
            DataSet dataSet = (DataSet)CustomerAccess.GetData();

            NewCriteria<int> criteria = new NewCriteria<int>("=", "ID", -1);
            CustomerQuery<int> customerQuery = new CustomerQuery<int>();
            customerQuery.Criterias(criteria);
           dataSet = (DataSet)customerQuery.Execute();

            Assert.IsTrue(dataSet.Tables["Customer"].Rows.Count <= 0);
        }
    }
}
