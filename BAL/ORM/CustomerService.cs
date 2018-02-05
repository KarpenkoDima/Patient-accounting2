using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAL.ORM.Repository;

namespace BAL.ORM
{
    public class CustomerService
    {
        CustomRepository<string> repo = new CustomRepository<string>();
        GlossaryRepository glossaryRepository = new GlossaryRepository();

        public void GetCustomersByFirstName(string firstName)
        {
            repo.FindBy("firstName", firstName);
            // return _tables.DispancerDataSet;
        }
        public void GetCustomersByLastName(string lastName)
        {
            repo.FindBy("lastname", lastName);
        }
        public void GetCustomersByBirthdayBetween(DateTime from, DateTime to)
        {
            repo.FindByBetween("birthday", from.ToShortDateString(), to.ToShortDateString());

        }
        public void GetCustomersByBirthday(DateTime date)
        {
            repo.FindBy("birthday", date.ToShortDateString());
        }

        public void GetCustomerByAddress(string streetName)
        {
            repo.FindBy("street", streetName);

        }
        public void GetCustomerByCity(string streetName)
        {
            repo.FindBy("street", streetName);

        }
        public object FillAllCustomers()
        {
            return repo.FindAll();
        }

        public void UpdateAllCustomers()
        {
            repo.Update(null);
        }

        public void GetCustomerByLand(int numberLand)
        {
            glossaryRepository.FindBy("Land", numberLand);
        }

        public void GetCustomerByApppTpr(int numberApppTpr)
        {
            glossaryRepository.FindBy("ApppTpr", numberApppTpr);
        }
        public void GetCustomerByBenefitsCategory(int benefitsCategory)
        {
            glossaryRepository.FindBy("BenefitsCategory", benefitsCategory);
        }

        public void ExportToExcel(params string[] columns)
        {
            //repo.ExportToExcel(columns);
        }

        public object GetGlossary(string name)
        {
            return glossaryRepository.GetGlossaryByName(name);
        }

    }
}
