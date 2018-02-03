using System.Data;
using System.Data.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SOPB.Accounting.DAL.TableAdapters;
using SOPB.Accounting.DAL.TableAdapters.Glossary;

namespace SOPB.DALUnitTest.TableAdapters.Glossary
{
    [TestClass()]
    public class GlossaryTests
    {
        private SqlConnection _conn;
        private string[] _glossariesName;
        protected BaseTableAdapter GetGlossary(string name)
        {
            BaseTableAdapter table=null;
            switch (name)
            {
                case "AdminDivision":
                    table = new AdminDivisionTableAdapter(){Connection = _conn};
                        break;
                case "Gender":
                     table = new GenderTableAdapter() { Connection = _conn };
                    break;
                case "ApppTpr":
                     table = new ApppTprTableAdapter() { Connection = _conn };
                    break;
                case "Benefits":
                    table = new BenefitsCategoryTableAdapter() { Connection = _conn };
                    break;
                case "ChiperRecept":
                     table = new ChiperReceptTableAdapter() { Connection = _conn };
                    break;
                case "Disability":
                    table = new DisabilityGroupTableAdapter() { Connection = _conn };
                    break;
                case "Land":
                     table = new LandTableAdapter() { Connection = _conn };
                    break;
                case "Register":
                     table = new RegisterTypeTableAdapter() { Connection = _conn };
                    break;
                case "TypeStreet":
                     table = new TypeStreetTableAdapter() { Connection = _conn };
                    break;
                case "WhyDeReg":
                     table = new WhyDeRegisterTableAdapter() { Connection = _conn };
                    break;
            }

            return table;
        }
        [TestInitialize]
        public void Init()
        {
            _glossariesName = new[] { "AdminDivision","Gender","ApppTpr", "Benefits","ChiperRecept", "Disability","Land", "Register", "TypeStreet", "WhyDeReg" };

            _conn = UserSettings.InitConnection();
        }
        [TestMethod()]
        public void FillGlossariesTest()
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
    }
}