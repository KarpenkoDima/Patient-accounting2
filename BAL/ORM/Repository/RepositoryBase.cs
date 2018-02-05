using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAL.DataTables;

namespace BAL.ORM.Repository
{
    abstract class RepositoryBase
    {
        protected static Tables Tables = new Tables();
        protected abstract string GetBaseQuery();
        protected abstract string GetBaseWhereClause();
        protected abstract string GetEntityName();
        protected abstract string GetKeyFieldName();
        protected abstract void BuildChildCallbacks();

    }
}
