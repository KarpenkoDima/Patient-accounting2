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
        [DataRow("Козлова")]
        [DataRow("Петров")]
        [DataRow("Ива")]
        [DataRow("Аба")]
        public void CustomerService_GetCustomersByLastName_TestMethod(string name)
        {
            CustomerService service = new CustomerService();
            DataSet ds = (DataSet)service.GetCustomersByLastName(name);
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
        [DataRow("appptpr")]
        //[DataRow("gender")]
        [DataRow("land")]
        [DataRow("benefitscategory")]
        public void CustomerService_GetCustomersByGlossary_TestMethod(string name)
        {
            CustomerService service = new CustomerService();
            DataSet ds = (DataSet)service.GetCustomerByGlossary(name, 1);
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
