using System;
using System.Collections.Generic;
using System.Linq;
using BAL.ORM.Repository;
using static BAL.Utilites;

namespace BAL.ORM
{

    
    public class CustomerService
    {
        
        readonly GlossaryRepository _glossaryRepository = new GlossaryRepository();

        private static string _lastQuery;
        static List<object> _paramsObjects = new List<object>();
    
        public object GetCustomersByFirstName(string firstName)
        {
            _lastQuery = QueryCriteria.FirstName;
            _paramsObjects.Clear();
            _paramsObjects.Add(firstName);
            CustomRepository<string> _repo = new CustomRepository<string>();
           return  _repo.FindBy(QueryCriteria.FirstName, firstName);
        }
        public object GetCustomersByLastName(string lastName)
        {
            _lastQuery = QueryCriteria.LastName;
            _paramsObjects.Clear();
            _paramsObjects.Add(lastName);
            CustomRepository<string> _repo = new CustomRepository<string>();
            return  _repo.FindBy(QueryCriteria.LastName, lastName);
        }
        public object GetCustomersByBirthdayBetween(DateTime from, DateTime to)
        {
            _lastQuery = QueryCriteria.Bithday;
            _paramsObjects.Clear();
            _paramsObjects.Add(from);
            _paramsObjects.Add(to);
            _paramsObjects.Add("BETWEEN");
            CustomRepository<DateTime> repo = new CustomRepository<DateTime>();
            return repo.FindByBetween(QueryCriteria.Bithday, from, to);
        }
        public object GetCustomersByBirthday(DateTime date)
        {
            _lastQuery = QueryCriteria.Bithday;
            _paramsObjects.Clear();
            _paramsObjects.Add(date);
            CustomRepository<DateTime> _repo = new CustomRepository<DateTime>();
            return _repo.FindBy(QueryCriteria.Bithday, date);
        }
        public object GetCustomersByBirthdayWithPredicate(DateTime date, string predicate)
        {
            _lastQuery = QueryCriteria.Bithday;
            _paramsObjects.Clear();
            _paramsObjects.Add(date);
            _paramsObjects.Add(predicate);
            CustomRepository<DateTime> _repo = new CustomRepository<DateTime>();
            return _repo.FindByPredicate(QueryCriteria.Bithday, date, predicate);
        }
        public object GetCustomerByAddress(string streetName, string city="Славянск")
        {
            _lastQuery = QueryCriteria.Address;
            _paramsObjects.Clear();
            _paramsObjects.Add(streetName);
            _paramsObjects.Add(city);
            var _repo = new CustomRepository<string>();
            return _repo.FindBy(QueryCriteria.Address, new []{city,streetName});
        }
        public object FillAllCustomers()
        {
            _lastQuery = QueryCriteria.GetAll;
            _paramsObjects.Clear();
            var _repo = new CustomRepository<string>();
            return _repo.FillAll();
        }

        public object UpdateAllCustomers()
        {
            object o;
            try
            {
                var _repo = new CustomRepository<string>();
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
                case QueryCriteria.LastName:
                    CustomRepository<string> repo = new CustomRepository<string>();
                    return repo.FindBy(QueryCriteria.LastName, _paramsObjects[0].ToString());
                case QueryCriteria.Address:
                    repo = new CustomRepository<string>();
                    return repo.FindBy(QueryCriteria.Address, _paramsObjects.Cast<string>().ToArray());
                case QueryCriteria.Bithday:
                    CustomRepository<DateTime> repo2 = new CustomRepository<DateTime>();
                    if (_paramsObjects.Count == 2)
                    {
                        return repo2.FindByPredicate(QueryCriteria.Bithday, Convert.ToDateTime(_paramsObjects[0]),
                            _paramsObjects[1].ToString());
                    }
                    else  if (_paramsObjects.Count == 3)
                    {
                        return repo2.FindByBetween(QueryCriteria.Bithday, Convert.ToDateTime(_paramsObjects[0]),
                            Convert.ToDateTime(_paramsObjects[0]));
                    }
                    else
                    {
                        return repo2.FindBy(QueryCriteria.Bithday, _paramsObjects.Cast<DateTime>().ToArray());
                    }
                case QueryCriteria.Glossary:
                    return _glossaryRepository.FindBy(_paramsObjects[0].ToString(), (int)_paramsObjects[1]);
                case QueryCriteria.GetGlossaries:
                    return _glossaryRepository.FillAll();
                default:
                {
                    return FillAllCustomers();
                }
            }
        }
        public object GetCustomerByGlossary(string name, int id)
        {
            _lastQuery = QueryCriteria.Glossary;
            _paramsObjects.Clear();
            _paramsObjects.Add(name);
           _paramsObjects.Add(id);
            return _glossaryRepository.FindBy(name, id);
        }

        public void ExportToExcel(params string[] columns)
        {
            CustomRepository<string> _repo = new CustomRepository<string>();
            _repo.ExportToExcel(columns);
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
            _lastQuery = QueryCriteria.GetGlossaries;
            _paramsObjects.Clear();
            return _glossaryRepository.FillAll();
        }
        public object GetEmptyData()
        {
            CustomRepository<string> _repo = new CustomRepository<string>();
            return _repo.FillAll();
        }

        public void Validation()
        {
            CustomRepository<string> repo = new CustomRepository<string>();
            repo.Validated();
        }
      
    }
}
