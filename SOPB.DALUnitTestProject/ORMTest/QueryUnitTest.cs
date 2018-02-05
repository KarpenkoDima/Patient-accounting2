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
        public void Query_GlossaryQuery_ExecuteMethod_TestMethod()
        {
            SqlConnection connection = ConnectionManager.Connection;
            connection.Open();
            Tables tables = new Tables();
            CustomerAccess.FillDictionary(tables.DispancerDataSet);
            CustomerAccess.FillCustomerData(tables.DispancerDataSet);

            NewCriteria<int> criteria = new NewCriteria<int>(Predicate.Equals, "ApppTpr", 2);
            GlossaryQuery glossary = new GlossaryQuery();
            glossary.Criterias(criteria);
            tables.DispancerDataSet = (DataSet)glossary.Execute(tables.DispancerDataSet.Tables["Customer"]);

            Assert.IsTrue(tables.CustomerDataTable.Rows.Count > 0);
        }
        [TestMethod]
        public void Query_GlossaryQuery_ExecuteMethodWrongParametrValue_TestMethod()
        {
            SqlConnection connection = ConnectionManager.Connection;
            connection.Open();
            Tables tables = new Tables();
            //CustomerAccess.FillDictionary(tables.DispancerDataSet);
            //CustomerAccess.FillCustomerData(tables.DispancerDataSet);

            NewCriteria<int> criteria = new NewCriteria<int>(Predicate.Equals, "Gender", -2);
            GlossaryQuery glossary = new GlossaryQuery();
            glossary.Criterias(criteria);
            tables.DispancerDataSet = (DataSet)glossary.Execute(tables.DispancerDataSet.Tables["Customer"]);

            Assert.IsTrue(tables.CustomerDataTable.Rows.Count <= 0);
        }

        [TestMethod]
        public void Query_CustomerQuery_ExecuteMethod_TestMethod()
        {
            SqlConnection connection = ConnectionManager.Connection;
            connection.Open();
            Tables tables = new Tables();
            CustomerAccess.FillDictionary(tables.DispancerDataSet);
            //CustomerAccess.FillCustomerData(tables.DispancerDataSet);

            NewCriteria<int> criteria = new NewCriteria<int>(Predicate.ID, "Id", 1 );
            CustomerQuery<int> customerQuery = new CustomerQuery<int>();
            customerQuery.Criterias(criteria);
            tables.DispancerDataSet = (DataSet)customerQuery.Execute(tables.DispancerDataSet.Tables["Customer"]);

            Assert.IsTrue(tables.CustomerDataTable.Rows.Count > 0);
        }

        [TestMethod]
        public void Query_CustomerQuery_ExecuteMethod_ParametrWrongId_TestMethod()
        {
            SqlConnection connection = ConnectionManager.Connection;
            connection.Open();
            Tables tables = new Tables();
            CustomerAccess.FillDictionary(tables.DispancerDataSet);
           

            NewCriteria<int> criteria = new NewCriteria<int>(Predicate.ID, "Id", -1);
            CustomerQuery<int> customerQuery = new CustomerQuery<int>();
            customerQuery.Criterias(criteria);
            tables.DispancerDataSet = (DataSet)customerQuery.Execute(tables.DispancerDataSet.Tables["Customer"]);

            Assert.IsTrue(tables.CustomerDataTable.Rows.Count <= 0);
        }
    }
}
