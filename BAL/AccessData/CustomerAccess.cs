using System;
using System.Data;
using System.Data.SqlClient;
using SOPB.Accounting.DAL.LoadData;
using SOPB.Accounting.DAL.TableAdapters;
using SOPB.Accounting.DAL.TableAdapters.CustomerTableAdapter;
using SOPB.Accounting.DAL.TableAdapters.Glossary;

namespace BAL.AccessData
{
    public class CustomerAccess
    {

        public static void FillDictionary(DataSet dataSet)
        {
            TransactionWork transactionWork = null;
            try
            {
                using (transactionWork = (TransactionWork) TransactionFactory.Create())
                {
                    transactionWork.ReadData(dataSet.Tables["ApppTpr"]);

                    transactionWork.ReadData(dataSet.Tables["Gender"]);

                    transactionWork.ReadData(dataSet.Tables["AdminDivision"]);

                    transactionWork.ReadData(dataSet.Tables["TypeStreet"]);

                    transactionWork.ReadData(dataSet.Tables["ChiperRecept"]);

                    transactionWork.ReadData(dataSet.Tables["BenefitsCategory"]);

                    transactionWork.ReadData(dataSet.Tables["DisabilityGroup"]);

                    transactionWork.ReadData(dataSet.Tables["Land"]);

                    transactionWork.ReadData(dataSet.Tables["RegisterType"]);


                    transactionWork.ReadData(dataSet.Tables["WhyDeRegister"]);
                    transactionWork.Commit();
                }
            }
            catch (Exception)
            {
                transactionWork?.Rollback();
                throw;
            }
        }


        public static void FillCustomerData(DataSet dataSet)
        {
            TransactionWork transactionWork = null;
            try
            {
                using (transactionWork = (TransactionWork) TransactionFactory.Create())
                {
                    transactionWork.ReadData(dataSet.Tables["Customer"]);

                    transactionWork.ReadData(dataSet.Tables["Address"]);

                    transactionWork.ReadData(dataSet.Tables["Invalid"]);

                    transactionWork.ReadData(dataSet.Tables["InvalidBenefitsCategory"]);

                    transactionWork.ReadData(dataSet.Tables["Register"]);
                    transactionWork.Commit();
                }
            }
            catch (Exception)
            {
                transactionWork?.Rollback();
                throw;
            }
        }

        public static void GetCustomersByID(DataTable table, int idx)
        {
            TransactionWork transactionWork = null;

            String storageProcedureName = "uspGetCustomers";
            SqlParameter parameter = new SqlParameter();
            parameter.DbType = DbType.Int32;
            parameter.Size = 4;
            parameter.ParameterName = "@CustomerID";
            parameter.Value = idx;
            try
            {
                using (transactionWork = (TransactionWork) TransactionFactory.Create())
                {
                    transactionWork.Execute(table, storageProcedureName, parameter);
                    transactionWork.Commit();
                }
            }
            catch (Exception)
            {
                transactionWork?.Rollback();
                throw;
            }
        }

        public static void GetCustomersByLastName(DataTable table, string lastName)
        {
            TransactionWork transactionWork = null;


            string storageProcedureName = "uspGet" + table.TableName + "byLastName";
            SqlParameter parameter = new SqlParameter();
            parameter.DbType = DbType.String;
            parameter.Size = 100;
            parameter.ParameterName = "@LastName";
            try
            {
                parameter.Value = lastName;
                using (transactionWork = (TransactionWork) TransactionFactory.Create())
                {
                    transactionWork.Execute(table, storageProcedureName, parameter);
                    transactionWork.Commit();
                }
            }
            catch (Exception)
            {
                transactionWork?.Rollback();
                throw;
            }
        }

        public static void GetCustomersByBirthdayBetween(DataTable table, DateTime fromDateTime, DateTime toDateTime)
        {
            TransactionWork transactionWork = null;


            string storageProcedureName = "uspGet" + table.TableName + "ByBirthOfDay";
            SqlParameter[] parameters = new SqlParameter[3];
            SqlParameter parameter = new SqlParameter();
            parameter.DbType = DbType.DateTime;
            parameter.ParameterName = "@BirthOfDay";
            parameter.Value = fromDateTime;
            parameters[0] = parameter;
            parameter = new SqlParameter();
            parameter.DbType = DbType.DateTime;
            parameter.ParameterName = "@BirthOfDayEnd";
            parameter.Value = toDateTime;
            parameters[1] = parameter;
            parameter = new SqlParameter();
            parameter.DbType = DbType.String;
            parameter.Size = 10;
            parameter.ParameterName = "@predicate";
            parameter.Value = "МЕЖДУ";
            parameters[2] = parameter;
            try
            {
                using (transactionWork = (TransactionWork) TransactionFactory.Create())
                {
                    transactionWork.Execute(table, storageProcedureName, parameters);
                    transactionWork.Commit();
                }
            }
            catch (Exception)
            {
                transactionWork?.Rollback();
                throw;
            }
        }

        public static void GetCustomersByBirthday(DataTable table, DateTime fromDateTime)
        {

            TransactionWork transactionWork = null;

            string storageProcedureName = "uspGet" + table.TableName + "ByBirthOfDay";
            SqlParameter[] parameters = new SqlParameter[3];
            SqlParameter parameter = new SqlParameter();
            parameter.DbType = DbType.DateTime;
            parameter.ParameterName = "@BirthOfDay";
            parameter.Value = fromDateTime;
            parameters[0] = parameter;
            parameter = new SqlParameter();
            parameter.DbType = DbType.DateTime;
            parameter.ParameterName = "@BirthOfDayEnd";
            parameter.Value = DateTime.Now;
            parameters[1] = parameter;
            parameter = new SqlParameter();
            parameter.DbType = DbType.String;
            parameter.Size = 10;
            parameter.ParameterName = "@predicate";
            parameter.Value = "МЕЖДУ";
            parameters[2] = parameter;
            try
            {
                using (transactionWork = (TransactionWork) TransactionFactory.Create())
                {
                    transactionWork.Execute(table, storageProcedureName, parameters);
                    transactionWork.Commit();
                }
            }
            catch (Exception)
            {
                transactionWork?.Rollback();
                throw;
            }
        }

        public static void GetCustomersByGlossary(DataTable table, string glossaryName, int criteria)
        {
            TransactionWork transactionWork = null;


            string storageProcedureName = string.Format("uspGet{0}By{1}", table.TableName, glossaryName);
            SqlParameter parameter = new SqlParameter();
            parameter.DbType = DbType.Int32;
            parameter.ParameterName = "@ID";
            parameter.Value = criteria;
            try
            {
                using (transactionWork = (TransactionWork) TransactionFactory.Create())
                {
                    transactionWork.Execute(table, storageProcedureName, parameter);
                    transactionWork.Commit();
                }
            }
            catch (Exception)
            {
                transactionWork?.Rollback();
                throw;
            }
        }
    }
}
