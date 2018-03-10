using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAL.AccessData;

namespace BAL.ORM
{
    public abstract class Query<T>
    {
        protected List<NewCriteria<T>> _criterias = new List<NewCriteria<T>>();

        public void Criterias(NewCriteria<T> criteria)
        {
            _criterias.Add(criteria);
        }

        public abstract object Execute();
    }
    public class GlossaryQuery : Query<int>
    {
        public override object Execute()
        {
            
            foreach (NewCriteria<int> criteria in _criterias)
            {
                if (criteria != null)
                    switch (criteria.Predicate)
                    {
                        case Predicate.Equals:
                            CustomerAccess.GetCustomersByGlossary("Customer", criteria.Criteria, criteria.Values[0]);
                            break;
                    }
            }
            return CustomerAccess.GetData();
        }
    }

    public class CustomerQuery<T> : Query<T> where T : IComparable<T>
    {

        public override object Execute()
        {
            foreach (NewCriteria<T> criteria in _criterias)
            {
                if (criteria != null)
                    switch (criteria.Predicate)
                    {
                        case Predicate.ID:
                            CustomerAccess.GetCustomersByID(Convert.ToInt32(criteria.First));
                            break;

                        case Predicate.Equals:
                        case Predicate.Between:
                            MakeEquals(criteria);
                            break;
                        default: MakeWithPredicate(criteria);
                            break;
                    }
            }
            return CustomerAccess.GetData();
        }

        private void MakeWithPredicate(NewCriteria<T> criteria)
        {
            switch (criteria.Predicate)
            {
                case Predicate.GreatThan:
                    CustomerAccess.GetCustomersByBirthdayWithPredicate(DateTime.Parse(criteria.Values[0].ToString()), ">");
                    break;
                case Predicate.LessThan:
                    CustomerAccess.GetCustomersByBirthdayWithPredicate(DateTime.Parse(criteria.Values[0].ToString()), "<");
                    break;
                case Predicate.GeatThanOrEquals:
                    CustomerAccess.GetCustomersByBirthdayWithPredicate(DateTime.Parse(criteria.Values[0].ToString()), ">=");
                    break;
                case Predicate.LessThanOrEquals:
                    CustomerAccess.GetCustomersByBirthdayWithPredicate(DateTime.Parse(criteria.Values[0].ToString()), "<=");
                    break;

                case Predicate.Between:
                    if (criteria.Values.Length >= 2)
                    {
                        CustomerAccess.GetCustomersByBirthOfDay(
                            DateTime.Parse(criteria.Values[0].ToString()),
                            DateTime.Parse(criteria.Values[1].ToString()), "МЕЖДУ");
                    }
                    break;
                default:
                        CustomerAccess.GetCustomersByBirthday(DateTime.Parse(criteria.Values[0].ToString()));
                    break;
            }
        }
        

        private void MakeEquals(NewCriteria<T> criteria)
        {
            switch (criteria.Criteria.ToUpper())
            {
                case "LASTNAME":
                    CustomerAccess.GetCustomersByLastName(criteria.Values[0].ToString());
                    break;
                case "BIRTHDAY":
                    MakeWithPredicate(criteria);
                    break;
                case "ADDRESS":
                    CustomerAccess.GetCustomersByAddress(criteria.Values[0].ToString());
                    break;
            }
        }
    }

}
