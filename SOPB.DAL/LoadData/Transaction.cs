using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOPB.Accounting.DAL.TableAdapters;
using SOPB.Accounting.DAL.TableAdapters.CustomerTableAdapter;
using SOPB.Accounting.DAL.TableAdapters.Glossary;

namespace SOPB.Accounting.DAL.LoadData
{
    public interface ITransactionWork : IDisposable
    {
        void ReadData(DataTable table);
        void UpdateData(DataTable table);
        void Rollback();
        void Commit();
    }
    public class TransactionWork : ITransactionWork
    {

        public TransactionWork()
        {
            BeginWork();

        }

        private SqlTransaction _transaction;
        private SqlConnection _connection;


        protected void BeginWork()
        {
            _connection = ConnectionManager.ConnectionManager.Connection;
            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }
            _transaction = _connection.BeginTransaction();
        }

        public void ReadData(DataTable table)
        {
            BaseTableAdapter baseTableAdapter = TableAdapterFactory.AdapterFactory(table.TableName);
            if (baseTableAdapter != null)
            {
                baseTableAdapter.Connection = _transaction.Connection;
                baseTableAdapter.Transaction = _transaction;
                baseTableAdapter.Fill(table);
            }
        }
        public void UpdateData(DataTable table)
        {
            BaseTableAdapter baseTableAdapter = TableAdapterFactory.AdapterFactory(table.TableName) ;
            if (baseTableAdapter is UpdateBaseTableAdapter )

            {

                baseTableAdapter.Connection = _transaction.Connection;

                baseTableAdapter.Transaction = _transaction;

                ((UpdateBaseTableAdapter)baseTableAdapter).Update(table);

            }
        }

        public void Execute(DataTable table, string storageProc, params SqlParameter[] parameters)
        {
            BaseTableAdapter baseTableAdapter = TableAdapterFactory.AdapterFactory(table.TableName);
            baseTableAdapter.Connection = _transaction.Connection;
            baseTableAdapter.Transaction = this._transaction;
            baseTableAdapter.Execute(table, storageProc, parameters);
        }
        
        public void Rollback()
        {
            _transaction.Rollback();
        }

        public void Commit()
        {
            _transaction.Commit();
        }

        public void Dispose()
        {
            _transaction.Dispose();
            if (_connection.State == ConnectionState.Open)
            {
                _connection.Close();
            }
            _connection.Dispose();
        }


        
    }


    public class TransactionFactory
    {
        public static ITransactionWork Create()
        {
            return new TransactionWork();
        }
    }
}
