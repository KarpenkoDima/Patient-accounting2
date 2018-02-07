using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BAL.ORM.Repository;

namespace BAL.ORM
{
    public class CustomerService
    {
        readonly CustomRepository<string> _repo = new CustomRepository<string>();
        readonly GlossaryRepository _glossaryRepository = new GlossaryRepository();

        public CustomerService()
        {
            //_repo.FindAll();
        }
        public object GetCustomersByFirstName(string firstName)
        {
           return  _repo.FindBy("firstName", firstName);
            // return _tables.DispancerDataSet;
        }
        public object GetCustomersByLastName(string lastName)
        {
           return  _repo.FindBy("lastname", lastName);
        }
        public object GetCustomersByBirthdayBetween(DateTime from, DateTime to)
        {
            return _repo.FindByBetween("birthday", from.ToShortDateString(), to.ToShortDateString());

        }
        public object GetCustomersByBirthday(DateTime date)
        {
           return _repo.FindBy("birthday", date.ToShortDateString());
        }

        public void GetCustomerByAddress(string streetName)
        {
            _repo.FindBy("street", streetName);

        }
        public void GetCustomerByCity(string streetName)
        {
            _repo.FindBy("street", streetName);

        }
        public object FillAllCustomers()
        {
            return _repo.FindAll();
        }

        public void UpdateAllCustomers()
        {
            _repo.Update(null);
        }

        public object GetCustomerByLand(int numberLand)
        {
            return _glossaryRepository.FindBy("Land", numberLand);
        }

        public object GetCustomerByApppTpr(int numberApppTpr)
        {
            return _glossaryRepository.FindBy("ApppTpr", numberApppTpr);
        }
        public object GetCustomerByBenefitsCategory(int benefitsCategory)
        {
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

    }
}
