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
    internal class TransactionWork : ITransactionWork
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
            UpdateBaseTableAdapter baseTableAdapter = TableAdapterFactory.AdapterFactory(table.TableName) as UpdateBaseTableAdapter;
            if (baseTableAdapter != null)
            {
                baseTableAdapter.Connection = _transaction.Connection;
                baseTableAdapter.Transaction = _transaction;
                baseTableAdapter.Update(table);
            }
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
            //_transaction?.Dispose();
        }


        private void FillDictionary(DataSet dataSet)
        {
            try
            {
                BaseTableAdapter baseTableAdapter = new ApppTprTableAdapter();
                baseTableAdapter.Connection = ConnectionManager.ConnectionManager.Connection;
                baseTableAdapter.Transaction = _transaction as SqlTransaction;
                baseTableAdapter.Fill(dataSet.Tables["ApppTpr"]);

                baseTableAdapter = new GenderTableAdapter();
                baseTableAdapter.Connection = ConnectionManager.ConnectionManager.Connection;
                baseTableAdapter.Transaction = _transaction as SqlTransaction;
                baseTableAdapter.Fill(dataSet.Tables["Gender"]);

                baseTableAdapter = new AdminDivisionTableAdapter();
                baseTableAdapter.Connection = ConnectionManager.ConnectionManager.Connection;
                baseTableAdapter.Transaction = _transaction as SqlTransaction;
                baseTableAdapter.Fill(dataSet.Tables["AdminDivision"]);

                baseTableAdapter = new TypeStreetTableAdapter();
                baseTableAdapter.Connection = ConnectionManager.ConnectionManager.Connection;
                baseTableAdapter.Transaction = _transaction as SqlTransaction;
                baseTableAdapter.Fill(dataSet.Tables["TypeStreet"]);

                baseTableAdapter = new ChiperReceptTableAdapter();
                baseTableAdapter.Connection = ConnectionManager.ConnectionManager.Connection;
                baseTableAdapter.Transaction = _transaction as SqlTransaction;
                baseTableAdapter.Fill(dataSet.Tables["ChiperRecept"]);

                baseTableAdapter = new BenefitsCategoryTableAdapter();
                baseTableAdapter.Connection = ConnectionManager.ConnectionManager.Connection;
                baseTableAdapter.Transaction = _transaction as SqlTransaction;
                baseTableAdapter.Fill(dataSet.Tables["BenefitsCategory"]);

                baseTableAdapter = new DisabilityGroupTableAdapter();
                baseTableAdapter.Connection = ConnectionManager.ConnectionManager.Connection;
                baseTableAdapter.Transaction = _transaction as SqlTransaction;
                baseTableAdapter.Fill(dataSet.Tables["DisabilityGroup"]);

                baseTableAdapter = new LandTableAdapter();
                baseTableAdapter.Connection = ConnectionManager.ConnectionManager.Connection;
                baseTableAdapter.Transaction = _transaction as SqlTransaction;
                baseTableAdapter.Fill(dataSet.Tables["Land"]);

                baseTableAdapter = new RegisterTypeTableAdapter();
                baseTableAdapter.Connection = ConnectionManager.ConnectionManager.Connection;
                baseTableAdapter.Transaction = _transaction as SqlTransaction;
                baseTableAdapter.Fill(dataSet.Tables["RegisterType"]);


                baseTableAdapter = new WhyDeRegisterTableAdapter();
                baseTableAdapter.Connection = ConnectionManager.ConnectionManager.Connection;
                baseTableAdapter.Transaction = _transaction as SqlTransaction;
                baseTableAdapter.Fill(dataSet.Tables["WhyDeRegister"]);
            }
            catch (Exception)
            {

                throw;
            }
        }


        private void FillData(DataSet dataSet)
        {
            try
            {
                BaseTableAdapter baseTableAdapter = new CustomerTableAdapter();
                baseTableAdapter.Connection = ConnectionManager.ConnectionManager.Connection;
                baseTableAdapter.Transaction = _transaction as SqlTransaction;
                baseTableAdapter.Fill(dataSet.Tables["Customer"]);

                baseTableAdapter = new AddressTableAdapter();
                baseTableAdapter.Connection = ConnectionManager.ConnectionManager.Connection;
                baseTableAdapter.Transaction = _transaction as SqlTransaction;
                baseTableAdapter.Fill(dataSet.Tables["Address"]);

                baseTableAdapter = new InvalidTableAdapter();
                baseTableAdapter.Connection = ConnectionManager.ConnectionManager.Connection;
                baseTableAdapter.Transaction = _transaction as SqlTransaction;
                baseTableAdapter.Fill(dataSet.Tables["Invalid"]);

                baseTableAdapter = new InvalidBenefitsCategoryTableAdapter();
                baseTableAdapter.Connection = ConnectionManager.ConnectionManager.Connection;
                baseTableAdapter.Transaction = _transaction as SqlTransaction;
                baseTableAdapter.Fill(dataSet.Tables["InvalidBenefitsCategory"]);

                baseTableAdapter = new RegisterTableAdapter();
                baseTableAdapter.Connection = ConnectionManager.ConnectionManager.Connection;
                baseTableAdapter.Transaction = _transaction as SqlTransaction;
                baseTableAdapter.Fill(dataSet.Tables["Register"]);
            }
            catch (Exception)
            {
                throw;
            }
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
