using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.ORM
{
    public enum Predicate
    {
        ID,
        Equals,
        GreatThan,
        LessThan,
        Between,
        GeatThanOrEquals = GreatThan | Equals,
        LessThanOrEquals = LessThan | Equals
    }
    public class NewCriteria<T>
    {
        public Predicate Predicate { get; set; }
        public string Criteria { get; set; }
        public T[] Values { get; set; }

        public T First
        {
            get
            {
                if (Values.Length >= 1)
                {
                    return Values[0];
                }
                return default(T);
            }
            set
            {
                if (Values.Length == 0)
                {
                    Values = new T[1];
                }
                Values[0] = value;
            }
        }

        public T Second
        {
            get
            {
                if (Values.Length >= 2)
                {
                    return Values[1];
                }
                return default(T);
            }
        }



        public NewCriteria(Predicate predicate, string criteria, params T[] values)
        {
            Predicate = predicate;
            Criteria = criteria;
            Values = values;
        }

        public static NewCriteria<T> Equals(string criteria, T value1, T value2)
        {
            return new NewCriteria<T>(Predicate.Equals, criteria, value1, value2);
        }

        public static NewCriteria<T> Between(string criteria, T start, T end)
        {
            return new NewCriteria<T>(Predicate.Between, criteria, start, end);
        }
    }
}
