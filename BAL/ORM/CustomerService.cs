using System;
using System.Collections.Generic;
using BAL.ORM.Repository;

namespace BAL.ORM
{
    public class CustomerService
    {
        readonly CustomRepository<string> _repo = new CustomRepository<string>();
        readonly GlossaryRepository _glossaryRepository = new GlossaryRepository();

        private static string _lastQuery;
        static List<object> _paramsObjects = new List<object>();

        public CustomerService()
        {
            //_repo.FindAll();
        }
        public object GetCustomersByFirstName(string firstName)
        {
            _lastQuery = "GetCustomersByFirstName";
            _paramsObjects.Clear();
            _paramsObjects.Add(firstName);
           return  _repo.FindBy("firstName", firstName);
            // return _tables.DispancerDataSet;
        }
        public object GetCustomersByLastName(string lastName)
        {
            _lastQuery = "GetCustomersByLastName";
            _paramsObjects.Clear();
            _paramsObjects.Add(lastName);
            return  _repo.FindBy("lastname", lastName);
        }
        public object GetCustomersByBirthdayBetween(DateTime from, DateTime to)
        {
            _lastQuery = "GetCustomersByBirthdayBetween";
            _paramsObjects.Clear();
            _paramsObjects.Add(from);
            _paramsObjects.Add(to);
            return _repo.FindByBetween("birthday", from.ToShortDateString(), to.ToShortDateString());

        }
        public object GetCustomersByBirthday(DateTime date)
        {
            _lastQuery = "GetCustomersByBirthday";
            _paramsObjects.Clear();
            _paramsObjects.Add(date);
           return _repo.FindBy("birthday", date.ToShortDateString());
        }
        public object GetCustomersByBirthdayWithPredicate(DateTime date, string predicate)
        {
            _lastQuery = "GetCustomersByBirthdayWithPredicate";
            _paramsObjects.Clear();
            _paramsObjects.Add(date);
            _paramsObjects.Add(predicate);
            return _repo.FindByPredicate("birthday", date.ToShortDateString(), predicate);
        }
        public object GetCustomerByAddress(string streetName)
        {
            _lastQuery = "GetCustomerByAddress";
            _paramsObjects.Clear();
            _paramsObjects.Add(streetName);
            return _repo.FindBy("address", streetName);
        }
        public void GetCustomerByCity(string streetName)
        {
            _lastQuery = "GetCustomerByCity";
            _paramsObjects.Clear();
            _paramsObjects.Add(streetName);
           
            _repo.FindBy("street", streetName);
        }
        public object FillAllCustomers()
        {
            _lastQuery = "FillAllCustomers";
            _paramsObjects.Clear();
            return _repo.FillAll();
        }

        public object UpdateAllCustomers()
        {
            object o;
            try
            {
                _repo.Update(null);
            }
            finally
            {
                o= FillData();
            }

            return o;
        }

        private Object FillData()
        {
            switch (_lastQuery)
            {
                case "GetCustomersByLastName":
                    return _repo.FindBy("lastname", _paramsObjects[0].ToString());
                case "GetCustomersByBirthdayWithPredicate":
                    return _repo.FindByPredicate("birthday", _paramsObjects[0].ToString(), _paramsObjects[1].ToString());
                default:
                {
                   return FillAllCustomers();
                }
            }
        }

        public object GetCustomerByLand(int numberLand)
        {
            _lastQuery = "GetCustomerByLand";
            _paramsObjects.Clear();
            _paramsObjects.Add(numberLand);
            return _glossaryRepository.FindBy("Land", numberLand);
        }

        public object GetCustomerByApppTpr(int numberApppTpr)
        {
            _lastQuery = "GetCustomerByApppTpr";
            _paramsObjects.Clear();
            _paramsObjects.Add(numberApppTpr);
           
            return _glossaryRepository.FindBy("ApppTpr", numberApppTpr);
        }
        public object GetCustomerByBenefitsCategory(int benefitsCategory)
        {
            _lastQuery = "GetCustomerByBenefitsCategory";
            _paramsObjects.Clear();
            _paramsObjects.Add(benefitsCategory);
            return _glossaryRepository.FindBy("BenefitsCategory", benefitsCategory);
        }

        public void ExportToExcel(params string[] columns)
        {
            //repo.ExportToExcel(columns);
        }

        public object GetGlossary(string name)
        {
            return _glossaryRepository.GetGlossaryByName(name);
        }

        public void SaveGlossary(string name)
        {
           
            _glossaryRepository.SaveGlossary(name);
            FillData();
        }

        public object GetGlossaries()
        {
            return _glossaryRepository.FillAll();
        }
        public object GetEmptyData()
        {
            return null;
        }
    }
}
