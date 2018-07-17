using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using BAL.AccessData;
using SOPB.Accounting.DAL.LoadData;

namespace BAL.ORM.Repository
{
    internal class CustomRepository<T> : RepositoryBase, IMutableRepository<T> where T : IComparable<T>
    {
        public bool IsCorrertError { get; set; } = false;
        private string _errorText = string.Empty;
        DateTime _maxDateTime = DateTime.Now;
        public CustomRepository()
        {
            
#if DEBUG
            IsCorrertError = false;
#endif

            #region add events for correction error

            // proglems in time save
            ////if (IsCorrertError)
            ////{
            ////    Tables.CustomerDataTable.RowChanging += CustomerDataTableOnRowChanging;
            ////    Tables.CustomerDataTable.RowChanged += CustomerDataTableOnRowChanged;
            ////    Tables.RegisterDataTable.RowChanging += RegisterDataTableOnRowChanging;
            ////}

            #endregion
        }

        //private void RegisterDataTableOnRowChanging(object sender, DataRowChangeEventArgs e)
        //{
            
        //}

        //private void CustomerDataTableOnRowChanged(object sender, DataRowChangeEventArgs dataRowChangeEventArgs)
        //{
        //    if (dataRowChangeEventArgs.Action == DataRowAction.Commit && !string.IsNullOrEmpty(_errorText))
        //    {
        //        DataRow row = Tables.ErrorDataTable.NewRow();
        //        row[0] = dataRowChangeEventArgs.Row[0];
        //        row[1] = _errorText;
        //        Tables.ErrorDataTable.Rows.Add(row);

        //    }
        //    _errorText = String.Empty;
        //}
        
        //private void CustomerDataTableOnRowChanging(object sender, DataRowChangeEventArgs dataRowChangeEventArgs)
        //{
        //    if (Utilites.ValidateTextInputNotName(dataRowChangeEventArgs.Row["LastName"].ToString()))
        //    {
        //        //throw new ArgumentException("Not correct input valut","LastName");
        //        Debug.Write("BAL: Not correct input value", "LastName" + dataRowChangeEventArgs.Row["LastName"]);
        //        Debug.WriteLine(dataRowChangeEventArgs.Row[0]);
        //        _errorText += "Фамлия содержит некорректные символы. " + dataRowChangeEventArgs.Row["LastName"];
        //    }
        //    DateTime birthday;
        //    if (DateTime.TryParse(dataRowChangeEventArgs.Row["Birthday"].ToString(), out birthday))
        //    {
        //        if (birthday > _maxDateTime)
        //        {
        //            //throw new ArgumentException("Not correct date time", "Birthday");
        //            Debug.WriteLine("Not correct date time", "Birthday");
        //            _errorText += "\nНекорректная дата в поле День рождения. " + dataRowChangeEventArgs.Row["Birthday"];
        //        }
        //    }
        //}

        public object QueryData<T>(NewCriteria<T> newCriteria) where T : IComparable<T>
        {
            Query<T> query;
            query = new CustomerQuery<T>();
            query.Criterias(newCriteria);
            return query.Execute();
        }
        public object FindByBetween(string criteria, T start, T end)
        {
            Query<T> query = new CustomerQuery<T>();
            query.Criterias(new NewCriteria<T>("Between", criteria, start, end));
            return query.Execute();
          
        }
        #region Interface and BaseClass Methods

        protected override string GetBaseQuery()
        {
            throw new NotImplementedException();
        }

        protected override string GetBaseWhereClause()
        {
            throw new NotImplementedException();
        }

        protected override string GetEntityName()
        {
            return "Customer";
        }

        protected override string GetKeyFieldName()
        {
            return "CustomerID";
        }

        protected override void BuildChildCallbacks()
        {
            throw new NotImplementedException();
        }

        public object FindBy(string criteria, params T[] value)
        {
            NewCriteria<T> newCriteria = NewCriteria<T>.CreateCriteria("=", criteria, value);
            return this.QueryData(newCriteria);
        }
        public object FindByPredicate(object criteria, T value, string predicate)
        {
            NewCriteria<T> newCriteria = NewCriteria<T>.CreateCriteria(predicate, criteria.ToString(), value);
            return this.QueryData(newCriteria);
        }
        public object FindByID(int id)
        {
            NewCriteria<int> criteria = NewCriteria<int>.CreateCriteria("=", "CustomerID", id);
            return this.QueryData(criteria);
        }

        public object FillAll()
        {
            CustomerAccess.FillDictionary();
            CustomerAccess.FillCustomerData();
            return CustomerAccess.GetData();
        }
        public void Update(IList<T> list)
        {
            CustomerAccess.Update();
        }
        #endregion

        public object GetEmpty()
        {
            return null;
        }

        public void ExportToExcel(string[] columns)
        {
            CustomerAccess.ExportToExcel();
        }

        internal void Validated()
        {
            CustomerAccess.Vapidated();
        }
    }

    internal class GlossaryRepository : RepositoryBase, IMutableRepository<Int32>
    {

        #region Override Methods

        protected override string GetBaseQuery()
        {
            throw new NotImplementedException();
        }

        protected override string GetBaseWhereClause()
        {
            throw new NotImplementedException();
        }

        protected override string GetEntityName()
        {
            return String.Empty;
        }

        protected override string GetKeyFieldName()
        {
            return String.Empty;
        }

        protected override void BuildChildCallbacks()
        {
            throw new NotImplementedException();
        }

        public object FindBy(string criteria, params int[] value)
        {
            Query<int> query = new GlossaryQuery();
            query.Criterias(new NewCriteria<int>("=", criteria, value));
            return query.Execute();
        }

        public object FindByID(int id)
        {
            return FindBy("ID", id);
        }
        public object FillAll()
        {
            CustomerAccess.FillDictionary();
            return CustomerAccess.GetData();
        }

        public void Update(IList<int> list)
        {
            CustomerAccess.Update();
        }

        #endregion

        public object GetGlossaryByName(string name)
        {
            return CustomerAccess.GetByEntityName(name);
        }
        public void SaveGlossary(string nameGlossary)
        {
            CustomerAccess.SaveEntity(nameGlossary);
        }


    }
}
