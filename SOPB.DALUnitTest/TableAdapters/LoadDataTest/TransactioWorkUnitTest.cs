using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SOPB.Accounting.DAL.ConnectionManager;
using SOPB.Accounting.DAL.LoadData;

namespace SOPB.DALUnitTest.TableAdapters.LoadDataTest
{
    /// <summary>
    /// Summary description for TransactioWorkUnitTest1
    /// </summary>
    [TestClass]
    public class TransactioWorkUnitTest
    {
        public TransactioWorkUnitTest()
        {
            ConnectionManager.SetConnection("Supervisor", "admin");
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        #region Additional test attributes

        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //

        #endregion

        [TestMethod]
        public void TransactionReadDataCommitTestMethod()
        {
            ITransactionWork transactionWork = null;

            using (transactionWork = TransactionFactory.Create())
            {
                transactionWork.ReadData(new DataTable("AdminDivision"));
                transactionWork.ReadData(new DataTable("Appp"));
                transactionWork.ReadData(new DataTable("Benefits"));
                transactionWork.ReadData(new DataTable("ChiperRecept"));
                transactionWork.ReadData(new DataTable("Gender"));
                transactionWork.ReadData(new DataTable("Land"));
                transactionWork.ReadData(new DataTable("DisabilityGroup"));
                transactionWork.ReadData(new DataTable("RegisterType"));
                transactionWork.ReadData(new DataTable("TypeStreet"));
                transactionWork.ReadData(new DataTable("WhyDeRegister"));

                transactionWork.ReadData(new DataTable("Customer"));
                transactionWork.ReadData(new DataTable("InvalidBenefits"));
                transactionWork.Commit();
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void RollbackTransactionReadDataTestMethod()
        {
            ITransactionWork transactionWork = null;

            using (transactionWork = TransactionFactory.Create())
            {
                transactionWork.ReadData(new DataTable("Customer"));
                transactionWork.Rollback();
            }
            Assert.IsTrue(true);
        }
        [TestMethod]
        public void RollbackTransactionUpdateDataTestMethod()
        {
            ITransactionWork transactionWork = null;

            DataTable customerTable = new DataTable("Customer");
            using (transactionWork = TransactionFactory.Create())
            {
                transactionWork.ReadData(customerTable);
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
                    if ((string) customerTable.Rows[i]["LastName"] == "Nimus" ||
                        (string) customerTable.Rows[i]["FirstName"] == "Nimus")
                    {
                        customerTable.Rows[i].Delete();
                    }
                }
                transactionWork.UpdateData(customerTable);
                transactionWork.Rollback();
            }

            Assert.IsTrue(true);
        }
        [TestMethod]
        public void FailedAndRollbackTransactionUpdateDataTestMethod()
        {
            ITransactionWork transactionWork = null;

            DataTable adminDivisionTable = new DataTable("AdminDivision");
            using (transactionWork = TransactionFactory.Create())
            {
                transactionWork.ReadData(adminDivisionTable);
                DataRow newRow = adminDivisionTable.NewRow();
                newRow[1] = "aaa";
                adminDivisionTable.Rows.Add(newRow);

                transactionWork.UpdateData(adminDivisionTable);
                transactionWork.Rollback();
            }

            Assert.IsTrue(true);
        }
    }
}

