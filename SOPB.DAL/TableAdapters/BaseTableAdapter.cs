using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace SOPB.Accounting.DAL.TableAdapters
{
    public abstract class BaseTableAdapter
    {
        protected SqlDataAdapter _Adapter;
        protected SqlConnection _Connection;
        protected SqlTransaction _Transaction;
        protected SqlCommand[] _CommandCollection;
        private bool _clearBefore;

        public BaseTableAdapter()
        {
            _clearBefore = true;
        }

        void InitAdapter()
        {
            this._Adapter = new SqlDataAdapter();
        }

        public SqlDataAdapter Adapter
        {
            get
            {
                if (_Adapter == null)
                {
                    this.InitAdapter();
                }
                return this._Adapter;
            }
        }

        public bool ClearBefore
        {
            get { return _clearBefore; }
            set { _clearBefore = value; }
        }

        public SqlConnection Connection
        {
            get
            {
                if (this._Connection == null)
                {
                    this.InitConnection();
                }
                return this._Connection;
            }
            set
            {
                this._Connection = value;
                if (this.Adapter.InsertCommand != null)
                {
                    this.Adapter.InsertCommand.Connection = value;
                }
                if (this.Adapter.DeleteCommand != null)
                {
                    this.Adapter.DeleteCommand.Connection = value;
                }
                if (this.Adapter.UpdateCommand != null)
                {
                    this.Adapter.UpdateCommand.Connection = value;
                }
                for (int i = 0; i < this.CommandCollection.Length; i++)
                {
                    if (this.CommandCollection[i] != null)
                    {
                        ((SqlCommand) (CommandCollection[i])).Connection = value;
                    }
                }
            }
        }

        private void InitConnection()
        {
            this._Connection = new SqlConnection();
        }

        public SqlTransaction Transaction
        {
            get
            {
               return this._Transaction;
            }
            set
            {
                this._Transaction = value;
                if (this.Adapter.InsertCommand != null)
                {
                    this.Adapter.InsertCommand.Transaction = value;
                }
                if (this.Adapter.DeleteCommand != null)
                {
                    this.Adapter.DeleteCommand.Transaction = value;
                }
                if (this.Adapter.UpdateCommand != null)
                {
                    this.Adapter.UpdateCommand.Transaction = value;
                }
                for (int i = 0; i < this.CommandCollection.Length; i++)
                {
                    if (this.CommandCollection[i] != null)
                    {
                        ((SqlCommand)(CommandCollection[i])).Transaction = value;
                    }
                }
            }
        }

        public SqlCommand[] CommandCollection
        {
            get
            {
                if (this._CommandCollection == null)
                {
                    this.InitCollection();
                }
                return this._CommandCollection;
            }
        }

        public virtual void Update(DataTable table)
        {
            try
            {
                this.Adapter.ContinueUpdateOnError = true;
                this.Adapter.InsertCommand = this.CommandCollection[1];
                this.Adapter.InsertCommand.Transaction = this.CommandCollection[1].Transaction;

                this.Adapter.UpdateCommand = this.CommandCollection[1];
                this.Adapter.UpdateCommand.Transaction = this.Transaction;


                this.Adapter.DeleteCommand = this.CommandCollection[2];
                this.Adapter.DeleteCommand.Transaction = this.Transaction;


                this.Adapter.Update(table.Select(null, null,
                    DataViewRowState.Added | DataViewRowState.ModifiedCurrent | DataViewRowState.Deleted));

                if (table.HasErrors)
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    stringBuilder.AppendLine("Данная строка(и) была обновлена не удачно.");
                    foreach (DataRow row in table.Rows)
                    {
                        if (row.HasErrors)
                        {
                            DataColumn[] columns = row.GetColumnsInError();
                            if (columns.Length > 0)
                            {
                                for (int i = 0; i < columns.Length; i++)
                                {
                                    stringBuilder.AppendFormat("{0}: {1}", columns[i].ColumnName, row.RowError);
                                }
                            }
                            else
                            {
                                stringBuilder.AppendFormat("{0}", row.RowError);
                            }
                        }
                    }
                    throw new ArgumentException(stringBuilder.ToString());
                }
            }
            catch (Exception exception)
            {
                throw;
            }
        }

        #region Abstract memebrs

        protected abstract void InitCollection();
        public abstract int Fill(DataTable table);

        #endregion

    }
}
