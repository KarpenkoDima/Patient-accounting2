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
            //var dataTable = source as DataTable;
            //if (dataTable != null) return dataTable.DataSet;
            return null;
        }
    }

    public class CustomerQuery<T> : Query<T> where T : IComparable<T>
    {

        public override object Execute(/*IListSource source*/)
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
                            MakeEquals(criteria.Criteria, criteria.First);
                            break;
                        //case Predicate.Between:
                        //    MakeBetween(source, criteria.Criteria, criteria.Values);
                        //    break;
                        //default:
                        //{
                        //    MakeWithPredicate(source, criteria);
                        //}
                        //    break;
                    }
            }

           // if (source is DataTable dataTable) return dataTable.DataSet;
            return null;
        }

        private void MakeWithPredicate(IListSource source, NewCriteria<T> criteria)
        {
            switch (criteria.Predicate)
            {
                case Predicate.GreatThan:
                    CustomerAccess.GetCustomersByBirthdayWithPredicate( DateTime.Parse(criteria.Values[0].ToString()), ">");
                    break;                                            
                case Predicate.LessThan:                              
                    CustomerAccess.GetCustomersByBirthdayWithPredicate( DateTime.Parse(criteria.Values[0].ToString()), "<");
                    break;                                            
                case Predicate.GeatThanOrEquals:                      
                    CustomerAccess.GetCustomersByBirthdayWithPredicate( DateTime.Parse(criteria.Values[0].ToString()), ">=");
                    break;                                            
                case Predicate.LessThanOrEquals:                      
                    CustomerAccess.GetCustomersByBirthdayWithPredicate( DateTime.Parse(criteria.Values[0].ToString()), "<=");
                    break;
            }
        }

        private void MakeBetween(IListSource source, string criteria, T[] values)
        {
            switch (criteria.ToUpper())
            {
                case "FIRSTNAME":
                    break;
                case "LASTNAME":

                    break;
                case "MIDDLENAME":
                    break;
                case "LASTFIRSTMIDDLE":
                    break;
                case "BIRTHDAY":
                    if (values.Length >= 2)
                    {
                        CustomerAccess.GetCustomersByBirthdayBetween("Customer",
                            DateTime.Parse(values[0].ToString()),
                            DateTime.Parse(values[1].ToString()));
                    }
                    break;
                case "APPPTPR":
                    break;
            }
        
        }

        private void MakeEquals(/*IListSource source,*/ string criteria, T value)
        {
            switch (criteria.ToUpper())
            {
                case "FIRSTNAME":
                    //CustomerAccess.GetCustomersByFirstName(source as DataTable, value.ToString());
                    break;
                case "LASTNAME":
                    CustomerAccess.GetCustomersByLastName(value.ToString());
                    break;
                //case "MIDDLENAME":
                //    break;
                //case "LASTFIRSTMIDDLE":
                //    break;
                //case "BIRTHDAY":
                //    CustomerAccess.GetCustomersByBirthday(source as DataTable, DateTime.Parse(value.ToString()));
                //    break;
                //case "APPPTPR":
                //    break;
                case "ADDRESS":
                    CustomerAccess.GetCustomersByAddress(value.ToString());
                    break;
                //case "BENEFITSCATEGORY":
                //    CustomerAccess.GetCustomersByGlossary(source as DataTable, criteria, Int32.Parse(value.ToString()));
                //    break;
            }
        }
    }

}
