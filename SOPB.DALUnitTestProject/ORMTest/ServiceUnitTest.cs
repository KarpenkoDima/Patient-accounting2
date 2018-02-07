using System;
using System.Data;
using BAL.ORM;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SOPB.Accounting.DAL.ConnectionManager;

namespace SOPB.DALUnitTestProject.ORMTest
{
    [TestClass]
    public class ServiceUnitTest
    {
        [TestInitialize]
        public void Init()
        {
            ConnectionManager.SetConnection(UserSettings.UserName, UserSettings.Password);
        }
        [TestMethod]
        public void CustomerService_GetCustomersByLastName_TestMethod()
        {
            CustomerService service = new CustomerService();
            DataSet ds = (DataSet)service.GetCustomersByLastName("Козлова");
            int count = ds.Tables["Customer"].Rows.Count;
            Assert.IsTrue(count > 0);
        }
        [TestMethod]
        public void CustomerService_GetCustomersByBirthday_TestMethod()
        {
            CustomerService service = new CustomerService();
            DataSet ds = (DataSet)service.GetCustomersByBirthday(new DateTime(1990,1,1));
            int count = ds.Tables["Customer"].Rows.Count;
            Assert.IsTrue(count > 0);
        }
        [TestMethod]
        public void CustomerService_GetCustomersByBirthdayBetween_TestMethod()
        {
            CustomerService service = new CustomerService();
            DataSet ds = (DataSet)service.GetCustomersByBirthdayBetween(new DateTime(1990, 1, 1), DateTime.Now);
            int count = ds.Tables["Customer"].Rows.Count;
            Assert.IsTrue(count > 0);
        }
        [TestMethod]
        public void CustomerService_GetCustomersByApppTpr_TestMethod()
        {
            CustomerService service = new CustomerService();
            DataSet ds = (DataSet)service.GetCustomerByApppTpr(1);
            int count = ds.Tables["Customer"].Rows.Count;
            Assert.IsTrue(count > 0);
        }
        [TestMethod]
        public void CustomerService_GetCustomersByBenefitsCategory_TestMethod()
        {
            CustomerService service = new CustomerService();
            DataSet ds = (DataSet)service.GetCustomerByBenefitsCategory(1);
            int count = ds.Tables["Customer"].Rows.Count;
            Assert.IsTrue(count > 0);
        }
        [TestMethod]
        public void CustomerService_GetCustomersByLand_TestMethod()
        {
            CustomerService service = new CustomerService();
            DataSet ds = (DataSet)service.GetCustomerByLand(1);
            int count = ds.Tables["Customer"].Rows.Count;
            Assert.IsTrue(count > 0);
        }
        [TestMethod]
        public void CustomerService_FillAllCustomers_TestMethod()
        {
            CustomerService service = new CustomerService();
            DataSet ds = (DataSet)service.FillAllCustomers();
            int count = ds.Tables["Customer"].Rows.Count;
            Assert.IsTrue(count > 0);
        }
    }
}
