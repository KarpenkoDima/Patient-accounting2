using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOPB.Accounting.DAL.TableAdapters;
using SOPB.Accounting.DAL.TableAdapters.CustomerTableAdapter;
using SOPB.Accounting.DAL.TableAdapters.Glossary;

namespace SOPB.Accounting.DAL.LoadData
{
    public static class TableAdapterFactory
    {
        static Dictionary<string, BaseTableAdapter> _factory = new Dictionary<string, BaseTableAdapter>();
        public static BaseTableAdapter AdapterFactory(string tableName)
        {
            if (!_factory.ContainsKey(tableName))
            {
                _factory.Add(tableName, GetTableAdapter(tableName));
            }

            return _factory[tableName];
            
        }

        private static BaseTableAdapter GetTableAdapter(string tableName)
        {
            switch (tableName.ToUpper())
            {
                case "CUSTOMER":
                    return new CustomerTableAdapter();
                case "ADDRESS":
                    return new AddressTableAdapter();
                case "REGISTER":
                    return new RegisterTableAdapter();
                case "APPPTPR":
                    return new ApppTprTableAdapter();
                case "ADMINDIVISION":
                    return new AdminDivisionTableAdapter();
                case "TYPESTREET":
                    return new TypeStreetTableAdapter();
                case "LAND":
                    return new LandTableAdapter();
                case "WHYDEREGISTER":
                    return new WhyDeRegisterTableAdapter();
                case "REGISTERTYPE":
                    return new RegisterTypeTableAdapter();
                case "CHIPERRECEPT":
                    return new ChiperReceptTableAdapter();
                case "DISABILITYGROUP":
                    return new DisabilityGroupTableAdapter();
                case "BENEFITSCATEGORY":
                    return new BenefitsCategoryTableAdapter();
                case "INVALID":
                    return new InvalidTableAdapter();
                case "INVALIDBENEFITSCATEGORY":
                    return new InvalidBenefitsCategoryTableAdapter();
                case "GENDER":
                    return new GenderTableAdapter();
                default:
                    return new CustomerTableAdapter();
            }
        }
    }
}
