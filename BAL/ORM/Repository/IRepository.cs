using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.ORM.Repository
{
    public interface IRepository<T> where T : IComparable<T>
    {
        object FindBy(string criteria, params T[] value);
        object FillAll();
        object FindByID(int id);

    }

    public interface IMutableRepository<T> : IRepository<T> where T : IComparable<T>
    {
        void Update(IList<T> list);
    }
}
