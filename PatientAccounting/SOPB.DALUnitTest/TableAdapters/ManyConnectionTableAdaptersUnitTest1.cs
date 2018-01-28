using System;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SOPB.Accounting.DAL.TableAdapters;
using SOPB.Accounting.DAL.TableAdapters.Glossary;

namespace SOPB.DALUnitTest.TableAdapters
{
    [TestClass]
    public class ManyConnectionTableAdaptersUnitTest1
    {
        [TestMethod]
        public void GetManyManyConnectionTableAdaptersTestMethod1()
        {
            Accounting.DAL.ConnectionManager.ConnectionManager.SetConnection("Катя", "1");
           //Accounting.DAL.ConnectionManager.ConnectionManager.Connection;
            BaseTableAdapter[] adapters = new GenderTableAdapter[10];
            for (int i = 0; i < adapters.Length; i++)
            {
                adapters[i] = new GenderTableAdapter();
                adapters[i].Connection = Accounting.DAL.ConnectionManager.ConnectionManager.Connection;
                adapters[i].Fill(new DataTable());
            }
            Assert.AreEqual(adapters[0].Connection, adapters[1].Connection);

        }
    }
}
