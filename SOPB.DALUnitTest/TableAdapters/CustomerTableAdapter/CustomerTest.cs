using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SOPB.Accounting.DAL.TableAdapters;
using SOPB.Accounting.DAL.TableAdapters.CustomerTableAdapter;

namespace SOPB.DALUnitTest.TableAdapters.CustomerTableAdapter
{
    [TestClass]
    public class CustomerTest
    {
        private SqlConnection _conn;
        private string[] _glossariesName;
        protected BaseTableAdapter GetGlossary(string name)
        {
            BaseTableAdapter table = null;
            switch (name)
            {
                case "Address":
                    table = new AddressTableAdapter() { Connection = _conn };
                    break;
                case "Customer":
                    table = new Accounting.DAL.TableAdapters.CustomerTableAdapter.CustomerTableAdapter() { Connection = _conn };
                    break;
                case "Invalid":
                    table = new InvalidBenefitsCategoryTableAdapter() { Connection = _conn };
                    break;
                case "Register":
                    table = new RegisterTableAdapter() { Connection = _conn };
                    break;
               
            }

            return table;
        }

        [TestInitialize]
        public void Init()
        {
            _conn = UserSettings.InitConnection();
            _glossariesName = new[] { "Customer", "Address", "Invalid", "Register"};
        }

        [TestMethod]
        public void FillTablesFromDbForCustomerDataTest()
        {
            BaseTableAdapter table = null;
            foreach (string name in _glossariesName)
            {
                table = GetGlossary(name);
            }

            if (table != null)
            {
                int count = table.Fill(new DataTable(""));
                Assert.IsTrue(count > 0);
            }

        }
        
        [TestMethod]
        public void FillCustomerTableFromArchivTest()
        {
            Accounting.DAL.TableAdapters.CustomerTableAdapter.CustomerTableAdapter table = new Accounting.DAL.TableAdapters.CustomerTableAdapter.CustomerTableAdapter() { Connection = _conn };
            int count = table.FillToAcrh(new DataTable(""));
            Assert.IsTrue(count > 1);

        }
        [TestMethod]
        public void InsertCustomerNameSureNameAndLastName()
        {
            DataTable customerTable = new DataTable("Customer");
            Accounting.DAL.ConnectionManager.ConnectionManager.SetConnection("Supervisor", "admin");
            _conn = Accounting.DAL.ConnectionManager.ConnectionManager.Connection;
            UpdateBaseTableAdapter table = new Accounting.DAL.TableAdapters.CustomerTableAdapter.CustomerTableAdapter() { Connection = _conn };
            int count = table.Fill(customerTable);

            DataRow newRow = customerTable.NewRow();
            newRow[1] = 123;
            newRow[2] = 321;
            newRow["FirstName"] = "A.";
            newRow["MiddleName"] = "N.";
            newRow["LastName"] = "Nimus";
            newRow[6] = DateTime.Now;
            newRow[7] = 0;
            newRow[8] = 1;
            newRow[9] = 2;
            customerTable.Rows.Add(newRow);
            
            int insert = table.Update(customerTable);
            Assert.IsTrue(insert>0);

        }
        [TestMethod]
        public void UpdateCustomerNameSureNameAndLastName()
        {
            DataTable customerTable = new DataTable("Customer");
            Accounting.DAL.ConnectionManager.ConnectionManager.SetConnection("Supervisor", "admin");
            _conn = Accounting.DAL.ConnectionManager.ConnectionManager.Connection;
            UpdateBaseTableAdapter table = new Accounting.DAL.TableAdapters.CustomerTableAdapter.CustomerTableAdapter() { Connection = _conn };
            int count = table.Fill(customerTable);
            DataRow newRow = customerTable.NewRow();
            newRow[1] = 123;
            newRow[2] = 321;
            newRow["FirstName"] = "A.";
            newRow["MiddleName"] = "N.";
            newRow["LastName"] = "Nimus";
            newRow[6] = DateTime.Now;
            newRow[7] = 0;
            newRow[8] = 1;
            newRow[9] = 2;
            customerTable.Rows.Add(newRow);
            for (int i = 0; i < customerTable.Rows.Count; i++)
            {
                if ((string)customerTable.Rows[i]["LastName"] == "Nimus")
                {
                    customerTable.Rows[i]["FirstName"] = "Nimus";
                }
            }

            int update = table.Update(customerTable);
            Assert.IsTrue(update > 0);

        }
        [TestMethod]
        public void DeleteCustomerNameSureNameAndLastName()
        {
            DataTable customerTable = new DataTable("Customer");
            Accounting.DAL.ConnectionManager.ConnectionManager.SetConnection("Supervisor", "admin");
            _conn = Accounting.DAL.ConnectionManager.ConnectionManager.Connection;
            UpdateBaseTableAdapter table = new Accounting.DAL.TableAdapters.CustomerTableAdapter.CustomerTableAdapter() { Connection = _conn };
            int count = table.Fill(customerTable);
            DataRow newRow = customerTable.NewRow();
            newRow[1] = 123;
            newRow[2] = 321;
            newRow["FirstName"] = "A.";
            newRow["MiddleName"] = "N.";
            newRow["LastName"] = "Nimus";
            newRow[6] = DateTime.Now;
            newRow[7] = 0;
            newRow[8] = 1;
            newRow[9] = 2;
            customerTable.Rows.Add(newRow);
            for (int i = 0; i < customerTable.Rows.Count; i++)
            {
                if ((string) customerTable.Rows[i]["LastName"] == "Nimus" || (string)customerTable.Rows[i]["FirstName"] == "Nimus")
                {
                    customerTable.Rows[i].Delete();
                }
            }
            int delete = table.Update(customerTable);
            Assert.IsTrue(delete > 0);

        }
    }
}
