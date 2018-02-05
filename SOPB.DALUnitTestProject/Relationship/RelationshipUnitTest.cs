using System;
using System.Data.SqlClient;
using System.Diagnostics;
using BAL.DataTables;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SOPB.Accounting.DAL.ConnectionManager;
using SOPB.Accounting.DAL.TableAdapters;
using SOPB.Accounting.DAL.TableAdapters.CustomerTableAdapter;
using SOPB.Accounting.DAL.TableAdapters.Glossary;

namespace SOPB.DALUnitTestProject.Relationship
{
    [TestClass]
    public class RelationshipUnitTest
    {
        [TestInitialize]
        public void Init()
        {
            ConnectionManager.SetConnection(UserSettings.UserName, UserSettings.Password);
        }
        [TestMethod]
        public void Tables_CustomerAddressTables_RelationshipTest()
        {
            
            BaseTableAdapter tableAdapter = new GenderTableAdapter();
            tableAdapter.Connection = ConnectionManager.Connection;
            Tables tables = new Tables();
            tableAdapter.Fill(tables.GenderDataTable);
            tableAdapter = new ApppTprTableAdapter();
            tableAdapter.Connection = ConnectionManager.Connection;
            tableAdapter.Fill(tables.ApppDataTable);
            tableAdapter = new CustomerTableAdapter();
            tableAdapter.Connection = ConnectionManager.Connection;
            tableAdapter.Fill(tables.CustomerDataTable);

            tableAdapter = new AdminDivisionTableAdapter();
            tableAdapter.Connection = ConnectionManager.Connection;

            tableAdapter.Fill(tables.AdminDivisionDataTable);
            tableAdapter = new TypeStreetTableAdapter();
            tableAdapter.Connection = ConnectionManager.Connection;
            tableAdapter.Fill(tables.TypeStreetDataTable);
            tableAdapter = new AddressTableAdapter();
            tableAdapter.Connection = ConnectionManager.Connection;
            tableAdapter.Fill(tables.AddressDataTable);

            Assert.IsTrue(tables.AddressDataTable.Rows.Count > 0);
        }
        [TestMethod()]
        public void Tables_CustomerAddressTables_TransactionTest()
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
            tableAdapter = new CustomerTableAdapter();
            tableAdapter.Connection = connection;
            tableAdapter.Transaction = transaction;
            tableAdapter.Fill(tables.CustomerDataTable);

            tableAdapter = new AdminDivisionTableAdapter();
            tableAdapter.Connection = connection;
            tableAdapter.Transaction = transaction;

            tableAdapter.Fill(tables.AdminDivisionDataTable);
            tableAdapter = new TypeStreetTableAdapter();
            tableAdapter.Connection = connection;
            tableAdapter.Transaction = transaction;
            tableAdapter.Fill(tables.TypeStreetDataTable);
            tableAdapter = new AddressTableAdapter();
            tableAdapter.Connection = connection;
            tableAdapter.Transaction = transaction;
            tableAdapter.Fill(tables.AddressDataTable);
            transaction.Commit();
            Assert.IsTrue(tables.AddressDataTable.Rows.Count > 0);
        }
        [TestMethod()]
        public void Tables_CustomerRegisterTables_RelationshipTest()
        {
           
            BaseTableAdapter tableAdapter = new GenderTableAdapter();
            tableAdapter.Connection = ConnectionManager.Connection;
            Tables tables = new Tables();
            tableAdapter.Fill(tables.GenderDataTable);
            tableAdapter = new ApppTprTableAdapter();
            tableAdapter.Connection = ConnectionManager.Connection;
            tableAdapter.Fill(tables.ApppDataTable);
            tableAdapter = new CustomerTableAdapter();
            tableAdapter.Connection = ConnectionManager.Connection;
            tableAdapter.Fill(tables.CustomerDataTable);

            tableAdapter = new RegisterTypeTableAdapter();
            tableAdapter.Connection = ConnectionManager.Connection;
            tableAdapter.Fill(tables.RegisterTypeDataTable);
            tableAdapter = new LandTableAdapter();
            tableAdapter.Connection = ConnectionManager.Connection;
            tableAdapter.Fill(tables.LandDataTable);
            tableAdapter = new WhyDeRegisterTableAdapter();
            tableAdapter.Connection = ConnectionManager.Connection;
            tableAdapter.Fill(tables.WhyDeRegisterDataTable);

            tableAdapter = new RegisterTableAdapter();
            tableAdapter.Connection = ConnectionManager.Connection;
            tableAdapter.Fill(tables.RegisterDataTable);

            Assert.IsTrue(tables.RegisterDataTable.Rows.Count > 1);
        }
        [TestMethod()]
        public void Tables_CustomerInvalidTables_RelationshipTest()
        {
            
            BaseTableAdapter tableAdapter = new GenderTableAdapter();
            tableAdapter.Connection = ConnectionManager.Connection;
            Tables tables = new Tables();
            tableAdapter.Fill(tables.GenderDataTable);
            tableAdapter = new ApppTprTableAdapter();
            tableAdapter.Connection = ConnectionManager.Connection;
            tableAdapter.Fill(tables.ApppDataTable);
            tableAdapter = new CustomerTableAdapter();
            tableAdapter.Connection = ConnectionManager.Connection;
            tableAdapter.Fill(tables.CustomerDataTable);

            tableAdapter = new BenefitsCategoryTableAdapter();
            tableAdapter.Connection = ConnectionManager.Connection;
            tableAdapter.Fill(tables.BenefitsDataTable);
            tableAdapter = new ChiperReceptTableAdapter();
            tableAdapter.Connection = ConnectionManager.Connection;
            tableAdapter.Fill(tables.ChiperReceptDataTable);
            tableAdapter = new DisabilityGroupTableAdapter();
            tableAdapter.Connection = ConnectionManager.Connection;
            tableAdapter.Fill(tables.DisabilityGroupDataTable);

            tableAdapter = new InvalidTableAdapter();
            tableAdapter.Connection = ConnectionManager.Connection;
            tableAdapter.Fill(tables.InvalidDataTable);

            Assert.IsTrue(tables.InvalidDataTable.Rows.Count > 1);
        }

        [TestMethod()]
        public void Tables_CustomerAddressTables_Relationship_DeleteCascadTest()
        {
            ConnectionManager.SetConnection("Supervisor", "admin");
            BaseTableAdapter tableAdapter = new GenderTableAdapter();
            tableAdapter.Connection = ConnectionManager.Connection;
            Tables tables = new Tables();
            tableAdapter.Fill(tables.GenderDataTable);
            tableAdapter = new ApppTprTableAdapter();
            tableAdapter.Connection = ConnectionManager.Connection;
            tableAdapter.Fill(tables.ApppDataTable);
            tableAdapter = new CustomerTableAdapter();
            tableAdapter.Connection = ConnectionManager.Connection;
            tableAdapter.Fill(tables.CustomerDataTable);

            tableAdapter = new AdminDivisionTableAdapter();
            tableAdapter.Connection = ConnectionManager.Connection;
            tableAdapter.Fill(tables.AdminDivisionDataTable);
            tableAdapter = new TypeStreetTableAdapter();
            tableAdapter.Connection = ConnectionManager.Connection;
            tableAdapter.Fill(tables.TypeStreetDataTable);
            tableAdapter = new AddressTableAdapter();
            tableAdapter.Connection = ConnectionManager.Connection;
            tableAdapter.Fill(tables.AddressDataTable);

            int countRows = tables.AddressDataTable.Rows.Count;
            int customerId = Int32.Parse(tables.AddressDataTable.Rows[tables.AddressDataTable.Rows.Count-1]["CustomerID"].ToString());
            for (int i = 0; i < tables.CustomerDataTable.Rows.Count; i++)
            {
                if (Int32.Parse(tables.CustomerDataTable.Rows[i][0].ToString()) == customerId)
                {
                    tables.CustomerDataTable.Rows[i].Delete();
                    break;
                }
            }

            tableAdapter = new CustomerTableAdapter();
            tableAdapter.Connection = ConnectionManager.Connection;
            ((UpdateBaseTableAdapter)tableAdapter).Update(tables.CustomerDataTable);
            tableAdapter = new AddressTableAdapter();
            tableAdapter.Connection = ConnectionManager.Connection;
            tableAdapter.Fill(tables.AddressDataTable);
            Assert.IsTrue(countRows - tables.AddressDataTable.Rows.Count >= 1);
        }

        [TestMethod()]
        public void Tables_CustomerRegisterTables_Relationship_DeleteCascadTest()
        {
            ConnectionManager.SetConnection("Supervisor", "admin");
            BaseTableAdapter tableAdapter = new GenderTableAdapter();
            tableAdapter.Connection = ConnectionManager.Connection;
            Tables tables = new Tables();
            tableAdapter.Fill(tables.GenderDataTable);
            tableAdapter = new ApppTprTableAdapter();
            tableAdapter.Connection = ConnectionManager.Connection;
            tableAdapter.Fill(tables.ApppDataTable);
            tableAdapter = new CustomerTableAdapter();
            tableAdapter.Connection = ConnectionManager.Connection;
            tableAdapter.Fill(tables.CustomerDataTable);

            tableAdapter = new RegisterTypeTableAdapter();
            tableAdapter.Connection = ConnectionManager.Connection;
            tableAdapter.Fill(tables.RegisterTypeDataTable);
            tableAdapter = new LandTableAdapter();
            tableAdapter.Connection = ConnectionManager.Connection;
            tableAdapter.Fill(tables.LandDataTable);
            tableAdapter = new WhyDeRegisterTableAdapter();
            tableAdapter.Connection = ConnectionManager.Connection;
            tableAdapter.Fill(tables.WhyDeRegisterDataTable);

            tableAdapter = new RegisterTableAdapter();
            tableAdapter.Connection = ConnectionManager.Connection;
            tableAdapter.Fill(tables.RegisterDataTable);

            int countRows = tables.RegisterDataTable.Rows.Count;
            int customerId = Int32.Parse(tables.RegisterDataTable.Rows[tables.RegisterDataTable.Rows.Count - 1]["CustomerID"].ToString());
            for (int i = 0; i < tables.CustomerDataTable.Rows.Count; i++)
            {
                if (Int32.Parse(tables.CustomerDataTable.Rows[i][0].ToString()) == customerId)
                {
                    tables.CustomerDataTable.Rows[i].Delete();
                    break;
                }
            }

            tableAdapter = new CustomerTableAdapter();
            tableAdapter.Connection = ConnectionManager.Connection;
            ((UpdateBaseTableAdapter)tableAdapter).Update(tables.CustomerDataTable);
            tableAdapter = new RegisterTableAdapter();
            tableAdapter.Connection = ConnectionManager.Connection;
            tableAdapter.Fill(tables.RegisterDataTable);
            Assert.IsTrue(countRows - tables.RegisterDataTable.Rows.Count >= 1);
        }

        [TestMethod()]
        public void Tables_CustomerInvalidTables_Relationship_DeleteCascadTest()
        {
            ConnectionManager.SetConnection("Supervisor", "admin");
            BaseTableAdapter tableAdapter = new GenderTableAdapter();
            tableAdapter.Connection = ConnectionManager.Connection;
            Tables tables = new Tables();
            tableAdapter.Fill(tables.GenderDataTable);
            tableAdapter = new ApppTprTableAdapter();
            tableAdapter.Connection = ConnectionManager.Connection;
            tableAdapter.Fill(tables.ApppDataTable);
            tableAdapter = new CustomerTableAdapter();
            tableAdapter.Connection = ConnectionManager.Connection;
            tableAdapter.Fill(tables.CustomerDataTable);

            tableAdapter = new BenefitsCategoryTableAdapter();
            tableAdapter.Connection = ConnectionManager.Connection;
            tableAdapter.Fill(tables.BenefitsDataTable);
            tableAdapter = new ChiperReceptTableAdapter();
            tableAdapter.Connection = ConnectionManager.Connection;
            tableAdapter.Fill(tables.ChiperReceptDataTable);
            tableAdapter = new DisabilityGroupTableAdapter();
            tableAdapter.Connection = ConnectionManager.Connection;
            tableAdapter.Fill(tables.DisabilityGroupDataTable);

            tableAdapter = new InvalidTableAdapter();
            tableAdapter.Connection = ConnectionManager.Connection;
            tableAdapter.Fill(tables.InvalidDataTable);

            int countRows = tables.InvalidDataTable.Rows.Count;
            int customerId = Int32.Parse(tables.InvalidDataTable.Rows[tables.InvalidDataTable.Rows.Count-1]["CustomerID"].ToString());
            for (int i = 0; i < tables.CustomerDataTable.Rows.Count; i++)
            {
                if (Int32.Parse(tables.CustomerDataTable.Rows[i][0].ToString()) == customerId)
                {
                    tables.CustomerDataTable.Rows[i].Delete();
                    break;
                }
            }

            tableAdapter = new CustomerTableAdapter();
            tableAdapter.Connection = ConnectionManager.Connection;
            ((UpdateBaseTableAdapter)tableAdapter).Update(tables.CustomerDataTable);
            tableAdapter = new InvalidTableAdapter();
            tableAdapter.Connection = ConnectionManager.Connection;
            tableAdapter.Fill(tables.InvalidDataTable);
            Assert.IsTrue(countRows - tables.InvalidDataTable.Rows.Count >= 1);
        }

        [TestMethod()]
        public void Tables_InvalidBenefitsTables_Relationship_DeleteCascadTest()
        {
            ConnectionManager.SetConnection("Supervisor", "admin");
            BaseTableAdapter tableAdapter = new GenderTableAdapter();
            tableAdapter.Connection = ConnectionManager.Connection;
            Tables tables = new Tables();
            tableAdapter.Fill(tables.GenderDataTable);
            tableAdapter = new ApppTprTableAdapter();
            tableAdapter.Connection = ConnectionManager.Connection;
            tableAdapter.Fill(tables.ApppDataTable);
            tableAdapter = new CustomerTableAdapter();
            tableAdapter.Connection = ConnectionManager.Connection;
            tableAdapter.Fill(tables.CustomerDataTable);

            tableAdapter = new BenefitsCategoryTableAdapter();
            tableAdapter.Connection = ConnectionManager.Connection;
            tableAdapter.Fill(tables.BenefitsDataTable);
            tableAdapter = new ChiperReceptTableAdapter();
            tableAdapter.Connection = ConnectionManager.Connection;
            tableAdapter.Fill(tables.ChiperReceptDataTable);
            tableAdapter = new DisabilityGroupTableAdapter();
            tableAdapter.Connection = ConnectionManager.Connection;
            tableAdapter.Fill(tables.DisabilityGroupDataTable);

            tableAdapter = new InvalidTableAdapter();
            tableAdapter.Connection = ConnectionManager.Connection;
            tableAdapter.Fill(tables.InvalidDataTable);

            tableAdapter = new InvalidBenefitsCategoryTableAdapter();
            tableAdapter.Connection = ConnectionManager.Connection;
            tableAdapter.Fill(tables.InvalidBenefitsDataTable);

            int countRows = tables.InvalidBenefitsDataTable.Rows.Count;
            bool isRow = false;
            for (int i = 0; i < tables.InvalidBenefitsDataTable.Rows.Count; i++)
            {

                int invalidId = Int32.Parse(tables.InvalidBenefitsDataTable.Rows[tables.InvalidBenefitsDataTable.Rows.Count-1]["InvID"].ToString());
                Debug.WriteLine("InvID " + invalidId);
                int customerId = -10;
                for (int j = 0; j < tables.InvalidDataTable.Rows.Count; j++)
                {
                    if (Int32.Parse(tables.InvalidDataTable.Rows[j][0].ToString()) == invalidId)
                    {
                        customerId = Int32.Parse(tables.InvalidDataTable.Rows[j]["CustomerID"].ToString());
                        isRow = true;
                        Debug.WriteLine("CustomerID " + customerId);
                        break;
                    }
                }
                if (isRow)
                    for (int z = 0; z < tables.CustomerDataTable.Rows.Count; z++)
                    {
                        if (Int32.Parse(tables.CustomerDataTable.Rows[z][0].ToString()) == customerId)
                        {

                            tables.CustomerDataTable.Rows[z].Delete();
                            break;
                        }
                    }
                if (isRow)
                {
                    break;
                }
            }
            tableAdapter = new CustomerTableAdapter();
            tableAdapter.Connection = ConnectionManager.Connection;
            ((UpdateBaseTableAdapter)tableAdapter).Update(tables.CustomerDataTable);
            tables.InvalidBenefitsDataTable.Clear();
            tableAdapter = new InvalidTableAdapter();
            tableAdapter.Connection = ConnectionManager.Connection;
            tableAdapter.Fill(tables.InvalidDataTable);
            tableAdapter = new InvalidBenefitsCategoryTableAdapter();
            tableAdapter.Connection = ConnectionManager.Connection;
            tableAdapter.Fill(tables.InvalidBenefitsDataTable);
            Assert.IsTrue(countRows - tables.InvalidBenefitsDataTable.Rows.Count >= 1);
        }

    }

}
