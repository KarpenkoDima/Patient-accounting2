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
        private bool _isCorrertError = true;
        private string _errorText = string.Empty;
        DateTime _maxDateTime = DateTime.Now;
        public CustomRepository()
        {
#if DEBUG
            _isCorrertError = false;
#endif
            // proglems in time save
            if (_isCorrertError)
            {
                Tables.CustomerDataTable.RowChanging += CustomerDataTableOnRowChanging;
                Tables.CustomerDataTable.RowChanged += CustomerDataTableOnRowChanged;
                Tables.RegisterDataTable.RowChanging += RegisterDataTableOnRowChanging;
            }
        }

        private void RegisterDataTableOnRowChanging(object sender, DataRowChangeEventArgs e)
        {
            
        }

        private void CustomerDataTableOnRowChanged(object sender, DataRowChangeEventArgs dataRowChangeEventArgs)
        {
            if (dataRowChangeEventArgs.Action == DataRowAction.Commit && !string.IsNullOrEmpty(_errorText))
            {
                DataRow row = Tables.ErrorDataTable.NewRow();
                row[0] = dataRowChangeEventArgs.Row[0];
                row[1] = _errorText;
                Tables.ErrorDataTable.Rows.Add(row);

            }
            _errorText = String.Empty;
        }
        
        private void CustomerDataTableOnRowChanging(object sender, DataRowChangeEventArgs dataRowChangeEventArgs)
        {
            if (Utilites.ValidateTextInputNotName(dataRowChangeEventArgs.Row["LastName"].ToString()))
            {
                //throw new ArgumentException("Not correct input valut","LastName");
                Debug.Write("BAL: Not correct input value", "LastName" + dataRowChangeEventArgs.Row["LastName"]);
                Debug.WriteLine(dataRowChangeEventArgs.Row[0]);
                _errorText += "Фамлия содержит некорректные символы. " + dataRowChangeEventArgs.Row["LastName"];
            }
            DateTime birthday;
            if (DateTime.TryParse(dataRowChangeEventArgs.Row["Birthday"].ToString(), out birthday))
            {
                if (birthday > _maxDateTime)
                {
                    //throw new ArgumentException("Not correct date time", "Birthday");
                    Debug.WriteLine("Not correct date time", "Birthday");
                    _errorText += "\nНекорректная дата в поле День рождения. " + dataRowChangeEventArgs.Row["Birthday"];
                }
            }
        }

        public void GetCustomerBy<T>(string item, T value) where T : IComparable<T>
        {
            Query<T> query = new CustomerQuery<T>();
            query.Criterias(new NewCriteria<T>(Predicate.Equals, item, value));
            query.Execute(Tables.CustomerDataTable);
        }
        public object FindByBetween(object criteria, T start, T end)
        {
            //ClearCustomerData();
            Query<T> query = new CustomerQuery<T>();
            query.Criterias(new NewCriteria<T>(Predicate.Between, criteria.ToString(), start, end));
            query.Execute(Tables.CustomerDataTable);
            return Tables.DispancerDataSet;

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

        public object FindBy(object criteria, T value)
        {
             ClearCustomerData();
            this.GetCustomerBy(criteria.ToString(), value);
            return Tables.DispancerDataSet;
        }

        public object FindAll()
        {
            ClearCustomerData();
            CustomerAccess.FillDictionary(Tables.DispancerDataSet);
            CustomerAccess.FillCustomerData(Tables.DispancerDataSet);
            //problems in time save
            if (_isCorrertError)
            {

                Tables.CustomerDataTable.RowChanging -= CustomerDataTableOnRowChanging;
                Tables.CustomerDataTable.RowChanged -= CustomerDataTableOnRowChanged;
                Tables.RegisterDataTable.RowChanging -= RegisterDataTableOnRowChanging;
            }
            return Tables.DispancerDataSet;
        }

        public void Update(IList<T> list)
        {
            CustomerAccess.Update(Tables.DispancerDataSet);
        }        

        #endregion

        private void ClearCustomerData()
        {
            Tables.AddressDataTable.Clear();
            Tables.InvalidBenefitsDataTable.Clear();
            Tables.InvalidDataTable.Clear();
            Tables.RegisterDataTable.Clear();
            Tables.ErrorDataTable.Clear();
            Tables.CustomerDataTable.Clear();
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

        public object FindBy(object criteria, int value)
        {
            ClearCustomerData();
            Query<int> query = new GlossaryQuery();
            query.Criterias(new NewCriteria<int>(Predicate.Equals, criteria.ToString(), value));
            query.Execute(Tables.DispancerDataSet.Tables["Customer"]);
            return Tables.DispancerDataSet;
        }

        public object FindAll()
        {
            CustomerAccess.FillDictionary(Tables.DispancerDataSet);
            return Tables.DispancerDataSet;
        }

        public void Update(IList<int> list)
        {
            Update();
        }

        private void Update()
        {
            TransactionWork transactionWork = null;
            try
            {
                using (transactionWork = (TransactionWork)TransactionFactory.Create())
                {
                    for (int i = 0; i < Tables.DispancerDataSet.Tables.Count; i++)
                    {
                        transactionWork.UpdateData(Tables.DispancerDataSet.Tables[i]);
                    }

                    transactionWork.Commit();
                }
            }
            catch (Exception)
            {
                transactionWork?.Rollback();
                throw;
            }
        }

        #endregion

        public object GetGlossaryByName(string name)
        {
            return Tables.DispancerDataSet.Tables[name];
        }
        private void ClearCustomerData()
        {
            Tables.AddressDataTable.Clear();
            Tables.InvalidBenefitsDataTable.Clear();
            Tables.InvalidDataTable.Clear();
            Tables.RegisterDataTable.Clear();
            Tables.ErrorDataTable.Clear();
            Tables.CustomerDataTable.Clear();
        }
    }
}
