using System;
using System.Data;
using System.Data.SqlClient;
using BAL.DataTables;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SOPB.Accounting.DAL.ConnectionManager;
using SOPB.Accounting.DAL.TableAdapters;
using SOPB.Accounting.DAL.TableAdapters.CustomerTableAdapter;
using SOPB.Accounting.DAL.TableAdapters.Glossary;

namespace SOPB.DALUnitTestProject.Relationship
{
    [TestClass]
    public class RelationShipUpdateCustomerDataUnitTest1
    {
        [TestInitialize]
        public void Init()
        {
            ConnectionManager.SetConnection(UserSettings.UserName, UserSettings.Password);
        }

        [TestMethod()]
        public void Tables_AddNewCustomerAndAddressTest()
        {

            SqlConnection connection = ConnectionManager.Connection;
            connection.Open();
            Tables tables = new Tables();
            BaseTableAdapter tableAdapter = new GenderTableAdapter();
            tableAdapter.Connection = connection;
            tableAdapter.Fill(tables.GenderDataTable);
            tableAdapter = new ApppTprTableAdapter();
            tableAdapter.Connection = connection;
            tableAdapter.Fill(tables.ApppDataTable);
            tableAdapter = new CustomerTableAdapter();
            tableAdapter.Connection = connection;
            tableAdapter.Fill(tables.CustomerDataTable);
            tableAdapter = new AdminDivisionTableAdapter();
            tableAdapter.Connection = connection;
            tableAdapter.Fill(tables.AdminDivisionDataTable);
            tableAdapter = new TypeStreetTableAdapter();
            tableAdapter.Connection = connection;
            tableAdapter.Fill(tables.TypeStreetDataTable);
            tableAdapter = new AddressTableAdapter();
            tableAdapter.Connection = connection;
            tableAdapter.Fill(tables.AddressDataTable);

            DataRow newRow = tables.CustomerDataTable.NewRow();
            newRow["MedCard"] = 123;
            newRow["CodeCustomer"] = 321;
            newRow["LastName"] = "Bogodur";
            newRow["FirstName"] = "Ivan";
            newRow["MiddleName"] = "Bogodurovich";
            newRow["Birthday"] = new DateTime(1970,1,1);
            newRow["Arch"] = false;
            newRow["APPPTPRID"] = tables.ApppDataTable.Rows[0]["APPPTPRID"];
            newRow["GenderID"] = tables.GenderDataTable.Rows[0]["GenderID"];
            tables.CustomerDataTable.Rows.Add(newRow);
            int newCustomerId = (int)newRow["CustomerID"];
            newRow = tables.AddressDataTable.NewRow();
            newRow["City"] = "Slavyansk";
            newRow["AdminDivisionID"] = tables.AdminDivisionDataTable.Rows[0]["AdminDivisionID"]; ;
            newRow["NameStreet"] = "Lenina";
            newRow["NumberHouse"] = "57";
            newRow["NumberApartment"] = "37";
            newRow["CustomerID"] = newCustomerId;
            tables.AddressDataTable.Rows.Add(newRow);
            tables.DispancerDataSet.AcceptChanges();
            var ds= tables.DispancerDataSet.GetChanges();
            int count = 0;
            if (ds != null)
            {
                count  = ds.Tables.Count;
            }
            
            Assert.IsTrue(count == 0);
        }
        [TestMethod()]
        public void Tables_AddNewCustomerAndAddressAndWriteToDataBaseTest()
        {

            SqlConnection connection = ConnectionManager.Connection;
            connection.Open();
            SqlTransaction transaction = connection.BeginTransaction();
            BaseTableAdapter tableAdapter = new GenderTableAdapter();
            tableAdapter.Connection = connection;
            tableAdapter.Transaction = transaction;
            Tables tables = new Tables();
            tableAdapter.Fill(tables.GenderDataTable);
            tableAdapter = new ApppTprTableAdapter();
            tableAdapter.Connection = connection;
            tableAdapter.Transaction = transaction;
            tableAdapter.Fill(tables.ApppDataTable);
            UpdateBaseTableAdapter tableAdapterCustomer = new CustomerTableAdapter();
            tableAdapterCustomer.Connection = connection;
            tableAdapterCustomer.Transaction = transaction;
            tableAdapterCustomer.Fill(tables.CustomerDataTable);

            tableAdapter = new AdminDivisionTableAdapter();
            tableAdapter.Connection = connection;
            tableAdapter.Transaction = transaction;

            tableAdapter.Fill(tables.AdminDivisionDataTable);
            tableAdapter = new TypeStreetTableAdapter();
            tableAdapter.Connection = connection;
            tableAdapter.Transaction = transaction;
            tableAdapter.Fill(tables.TypeStreetDataTable);
            UpdateBaseTableAdapter tableAdapterAddr = new AddressTableAdapter();
            tableAdapterAddr.Connection = connection;
            tableAdapterAddr.Transaction = transaction;
            tableAdapterAddr.Fill(tables.AddressDataTable);

            DataRow newRow = tables.CustomerDataTable.NewRow();
            newRow["MedCard"] = 123;
            newRow["CodeCustomer"] = 321;
            newRow["LastName"] = "Bogodur";
            newRow["FirstName"] = "Ivan";
            newRow["MiddleName"] = "Bogodurovich";
            newRow["Birthday"] = new DateTime(1970, 1, 1);
            newRow["Arch"] = false;
            newRow["APPPTPRID"] = tables.ApppDataTable.Rows[0]["APPPTPRID"];
            newRow["GenderID"] = tables.GenderDataTable.Rows[0]["GenderID"];
            tables.CustomerDataTable.Rows.Add(newRow);
            int newCustomerId = (int)newRow["CustomerID"];
            newRow = tables.AddressDataTable.NewRow();
            newRow["City"] = "Slavyansk";
            newRow["AdminDivisionID"] = tables.AdminDivisionDataTable.Rows[0]["AdminDivisionID"]; ;
            newRow["NameStreet"] = "Lenina";
            newRow["NumberHouse"] = "57";
            newRow["NumberApartment"] = "37";
            newRow["CustomerID"] = newCustomerId;
            tables.AddressDataTable.Rows.Add(newRow);
            tableAdapterCustomer.Update(tables.CustomerDataTable);
            tableAdapterAddr.Update(tables.AddressDataTable);
            transaction.Commit();

            var ds = tables.DispancerDataSet.GetChanges();
            int count = 0;
            if (ds != null)
            {
                count = ds.Tables.Count;
            }
            Assert.IsTrue(count == 0);
        }
    }
}
