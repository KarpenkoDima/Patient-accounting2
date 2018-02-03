using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SOPB.Accounting.DAL.LoadData;

namespace SOPB.DALUnitTest.TableAdapters.LoadDataTest
{
    [TestClass]
    public class TableAdapterFactoryTest
    {
        [TestMethod]
        public void NullTableAdapterTest()
        {
            var obj = TableAdapterFactory.AdapterFactory("Abracadabra");
            Assert.IsNull(obj);
        }
        [TestMethod]
        public void GetTableAdapterTest()
        {
            var obj = TableAdapterFactory.AdapterFactory("CuSToMer");
            Assert.IsNotNull(obj);
        }
    }
}
