using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.ORM
{
    public class NewCriteria<T>
    {
        public string Predicate { get; set; }
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

        public NewCriteria(string predicate, string criteria, params T[] values)
        {
            Predicate = predicate;
            Criteria = criteria;
            Values = values;
        }
        public static NewCriteria<T> CreateCriteria(string predicate, string criteria, params T[] values)
        {
            NewCriteria<T> newCriteria =new NewCriteria<T>(predicate, criteria, values);
            return newCriteria;
        }
    }
}
