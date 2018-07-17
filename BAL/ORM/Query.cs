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
                    CustomerAccess.GetCustomersByGlossary("Customer", criteria.Criteria, criteria.Values[0]);
                break;
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
                    switch (criteria.Criteria)
                    {
                        case Utilites.QueryCriteria.ID:
                            CustomerAccess.GetCustomersByID(Convert.ToInt32(criteria.First));
                            break;
                        case Utilites.QueryCriteria.Bithday:
                            if (criteria.Predicate == "Between")
                            {
                                CustomerAccess.GetCustomersByBirthdayBetween(Convert.ToDateTime(criteria.First),
                                    Convert.ToDateTime(criteria.Second));
                            }
                            else
                            {
                                CustomerAccess.GetCustomersByBirthOfDay(Convert.ToDateTime(criteria.First),
                                    criteria.Predicate);
                            }
                            break;
                        default:
                            CustomerAccess.GetDataByCriteria(criteria.Criteria,
                                criteria.Values.Cast<object>().ToArray(), criteria.Predicate);
                            break;
                    }
            }
            return CustomerAccess.GetData();
        }
    }

}


