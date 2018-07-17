using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using SOPB.Accounting.DAL.LoadData;

namespace SOPB.Accounting.DAL.TableAdapters
{
    public abstract class BaseTableAdapter
    {
        protected SqlDataAdapter _adapter;
        protected SqlConnection _connection;
        protected SqlTransaction _transaction;
        protected SqlCommand[] _commandCollection;
        private bool _clearBefore;

        public BaseTableAdapter()
        {
            _clearBefore = true;
        }

        void InitAdapter()
        {
            this._adapter = new SqlDataAdapter();
        }

        public SqlDataAdapter Adapter
        {
            get
            {
                if (_adapter == null)
                {
                    this.InitAdapter();
                }
                return this._adapter;
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
                if (this._connection == null)
                {
                    this.InitConnection();
                }
                return this._connection;
            }
            set
            {
                this._connection = value;
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
            this._connection = new SqlConnection();
        }

        public SqlTransaction Transaction
        {
            get
            {
               return this._transaction;
            }
            set
            {
                this._transaction = value;
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
                if (this._commandCollection == null)
                {
                    this.InitCollection();
                }
                return this._commandCollection;
            }
        }

        public virtual void Execute(DataTable table, string storageProc, params SqlParameter[] parameters)
        {
            SqlCommand command =this.Transaction.Connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = storageProc;
            command.Transaction = Transaction;
            foreach (SqlParameter sqlParameter in parameters)
            {
                command.Parameters.Add(sqlParameter);
            }

            this.Adapter.SelectCommand = command;
            this.Adapter.Fill(table);
            //GenericDataAccess.ExecuteSelectCommand(command, table);
        }

        #region Abstract memebrs

        protected abstract void InitCollection();
        public abstract int Fill(DataTable table);

        #endregion

    }

    public abstract class UpdateBaseTableAdapter : BaseTableAdapter
    {
        public virtual int Update(DataTable table)
        {
            this.Adapter.ContinueUpdateOnError = true;
            this.Adapter.InsertCommand = this.CommandCollection[1];
            this.Adapter.InsertCommand.Transaction = this.CommandCollection[1].Transaction;

            this.Adapter.UpdateCommand = this.CommandCollection[1];
            this.Adapter.UpdateCommand.Transaction = this.Transaction;


            this.Adapter.DeleteCommand = this.CommandCollection[2];
            this.Adapter.DeleteCommand.Transaction = this.Transaction;

            //var update0 = table.Select(null, null,
            //    DataViewRowState.Added | DataViewRowState.ModifiedCurrent | DataViewRowState.Deleted);
            int update = this.Adapter.Update(table.Select(null, null,
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

            return update;
        }

        protected abstract override void InitCollection();


        public abstract override int Fill(DataTable table);

    }
}
