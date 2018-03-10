using System;
using System.Data;
using System.Data.SqlClient;
using BAL.DataTables;
using SOPB.Accounting.DAL.LoadData;
using SOPB.Accounting.DAL.TableAdapters;
using SOPB.Accounting.DAL.TableAdapters.CustomerTableAdapter;
using SOPB.Accounting.DAL.TableAdapters.Glossary;

namespace BAL.AccessData
{
    public class CustomerAccess
    {
        private static Tables _tables = new Tables();

        static CustomerAccess()
        {
            FillDictionary();
        }
        public static object GetData()
        {
            return _tables.DispancerDataSet;
        }
        public static void FillDictionary()
        {
            ClearData();
            TransactionWork transactionWork = null;
            try
            {
                using (transactionWork = (TransactionWork) TransactionFactory.Create())
                {
                    transactionWork.ReadData(_tables.DispancerDataSet.Tables["ApppTpr"]);
                    transactionWork.ReadData(_tables.DispancerDataSet.Tables["Gender"]);
                    transactionWork.ReadData(_tables.DispancerDataSet.Tables["AdminDivision"]);
                    transactionWork.ReadData(_tables.DispancerDataSet.Tables["TypeStreet"]);
                    transactionWork.ReadData(_tables.DispancerDataSet.Tables["ChiperRecept"]);
                    transactionWork.ReadData(_tables.DispancerDataSet.Tables["BenefitsCategory"]);
                    transactionWork.ReadData(_tables.DispancerDataSet.Tables["DisabilityGroup"]);
                    transactionWork.ReadData(_tables.DispancerDataSet.Tables["Land"]);
                    transactionWork.ReadData(_tables.DispancerDataSet.Tables["RegisterType"]);
                    transactionWork.ReadData(_tables.DispancerDataSet.Tables["WhyDeRegister"]);
                    transactionWork.Commit();
                }
            }
            catch (Exception)
            {
                transactionWork?.Rollback();
                throw;
            }
        }


        public static void FillCustomerData()
        {
            ClearData();
            TransactionWork transactionWork = null;
            try
            {
                using (transactionWork = (TransactionWork) TransactionFactory.Create())
                {
                    transactionWork.ReadData(_tables.DispancerDataSet.Tables["Customer"]);
                    transactionWork.ReadData(_tables.DispancerDataSet.Tables["Address"]);
                    transactionWork.ReadData(_tables.DispancerDataSet.Tables["Invalid"]);
                    transactionWork.ReadData(_tables.DispancerDataSet.Tables["InvalidBenefitsCategory"]);
                    transactionWork.ReadData(_tables.DispancerDataSet.Tables["Register"]);
                    transactionWork.Commit();
                }
            }
            catch (Exception)
            {
                transactionWork?.Rollback();
                throw;
            }
        }

        public static void GetCustomersByID(int idx)
        {
            ClearData();
            GetDataByCustomerID(idx);
        }

        private static void GetDataByCustomerID(int id)
        {
            TransactionWork transactionWork = null;

            String storageProcedureName = "uspGetCustomers";
            SqlParameter parameter = new SqlParameter();
            parameter.DbType = DbType.Int32;
            parameter.Size = 4;
            parameter.ParameterName = "@CustomerID";
            parameter.Value = id;
            SqlParameter parameterReg = new SqlParameter();
            parameterReg.DbType = DbType.Int32;
            parameterReg.Size = 4;
            parameterReg.ParameterName = "@CustomerID";
            parameterReg.Value = id;
            SqlParameter parameterInv = new SqlParameter();
            parameterInv.DbType = DbType.Int32;
            parameterInv.Size = 4;
            parameterInv.ParameterName = "@CustomerID";
            parameterInv.Value = id;
            SqlParameter parameterAddr = new SqlParameter();
            parameterAddr.DbType = DbType.Int32;
            parameterAddr.Size = 4;
            parameterAddr.ParameterName = "@CustomerID";
            parameterAddr.Value = id;
            try
            {
                using (transactionWork = (TransactionWork)TransactionFactory.Create())
                {
                    transactionWork.Execute(_tables.CustomerDataTable, storageProcedureName, parameter);
                    transactionWork.Execute(_tables.RegisterDataTable, "uspGetRegisterByCustomerID", parameterReg);
                    transactionWork.Execute(_tables.InvalidDataTable, "uspGetInvalidByCustomerID", parameterInv);
                    transactionWork.Execute(_tables.AddressDataTable, "uspGetAddressByCustomerID", parameterAddr);
                    transactionWork.Commit();
                }
            }
            catch (Exception)
            {
                transactionWork?.Rollback();
                throw;
            }
        }

        public static void GetCustomersByLastName(string lastName)
        {
            ClearData();
            GetDataByLastName(lastName);
        }

        private static void GetDataByLastName(string lastName)
        {
            TransactionWork transactionWork = null;
            SqlParameter parameter = new SqlParameter();
            parameter.DbType = DbType.String;
            parameter.Size = 100;
            parameter.ParameterName = "@LastName";

            SqlParameter parameterReg = new SqlParameter();
            parameterReg.DbType = DbType.String;
            parameterReg.Size = 100;
            parameterReg.ParameterName = "@LastName";
            SqlParameter parameterInv = new SqlParameter();
            parameterInv.DbType = DbType.String;
            parameterInv.Size = 100;
            parameterInv.ParameterName = "@LastName";
            SqlParameter parameterAddr = new SqlParameter();
            parameterAddr.DbType = DbType.String;
            parameterAddr.Size = 100;
            parameterAddr.ParameterName = "@LastName";
            try
            {
                parameter.Value = lastName;
                parameterReg.Value = lastName;
                parameterInv.Value = lastName;
                parameterAddr.Value = lastName;
                using (transactionWork = (TransactionWork)TransactionFactory.Create())
                {
                    transactionWork.Execute(_tables.CustomerDataTable, "uspGetCustomerByLastName", parameter);
                    transactionWork.Execute(_tables.RegisterDataTable, "uspGetRegisterByLastName", parameterReg);
                    transactionWork.Execute(_tables.InvalidDataTable, "uspGetInvalidByLastName", parameterInv);
                    transactionWork.Execute(_tables.AddressDataTable, "uspGetAddressByLastName", parameterAddr);
                    transactionWork.Commit();
                }
            }
            catch (Exception)
            {
                transactionWork?.Rollback();
                throw;
            }
        }
        public static void GetCustomersByBirthdayBetween(DateTime fromDateTime, DateTime toDateTime)
        {
            ClearData();
            GetDataByBirthOfDay("", fromDateTime, toDateTime, "МЕЖДУ");
        }

        public static void GetCustomersByBirthOfDay(DateTime fromDateTime, DateTime tillDateTime = new DateTime(), string predicate = "=")
        {
            ClearData();
            DateTime time = new DateTime();
            if (tillDateTime == time)
            {
                tillDateTime = DateTime.Now;
            }
            GetDataByBirthOfDay("", fromDateTime, tillDateTime, predicate);
        }
        public static void GetCustomersByBirthdayWithPredicate(DateTime fromDateTime, string predicate)
        {
            ClearData();
            GetDataByBirthOfDay("uspGetCustomerByBirthOfDay", fromDateTime, DateTime.Now, predicate);
        }
        public static void GetCustomersByBirthday(DateTime fromDateTime)
        {
            ClearData();
            GetDataByBirthOfDay("uspGetCustomerByBirthOfDay", fromDateTime, DateTime.Now);
        }
        private static void GetDataByBirthOfDay(string name, DateTime fromDateTime, DateTime tillDateTime, string predicate="=")
        {
            TransactionWork transactionWork = null;
            SqlParameter[] parameters = new SqlParameter[3];
            SqlParameter parameter = new SqlParameter();
            parameter.DbType = DbType.DateTime;
            parameter.ParameterName = "@BirthOfDay";
            parameter.Value = fromDateTime;
            parameters[0] = parameter;
            parameter = new SqlParameter();
            parameter.DbType = DbType.DateTime;
            parameter.ParameterName = "@BirthOfDayEnd";
            parameter.Value = tillDateTime;
            parameters[1] = parameter;
            parameter = new SqlParameter();
            parameter.DbType = DbType.String;
            parameter.Size = 10;
            parameter.ParameterName = "@predicate";
            parameter.Value = predicate;
            parameters[2] = parameter;

            SqlParameter[] parametersInv = new SqlParameter[3];
            parametersInv[0] = new SqlParameter(parameters[0].ParameterName, parameters[0].SqlDbType);
            parametersInv[0].Value = parameters[0].Value;
            parametersInv[1] = new SqlParameter(parameters[1].ParameterName, parameters[1].SqlDbType);
            parametersInv[1].Value = parameters[1].Value;
            parametersInv[2] = new SqlParameter(parameters[2].ParameterName, parameters[2].SqlDbType, parameters[2].Size);
            parametersInv[2].Value = parameters[2].Value;

            SqlParameter[] parametersInvBenefits = new SqlParameter[3];
            parametersInvBenefits[0] = new SqlParameter(parameters[0].ParameterName, parameters[0].SqlDbType);
            parametersInvBenefits[0].Value = parameters[0].Value;
            parametersInvBenefits[1] = new SqlParameter(parameters[1].ParameterName, parameters[1].SqlDbType);
            parametersInvBenefits[1].Value = parameters[1].Value;
            parametersInvBenefits[2] = new SqlParameter(parameters[2].ParameterName, parameters[2].SqlDbType, parameters[2].Size);
            parametersInvBenefits[2].Value = parameters[2].Value;

            SqlParameter[] parametersReg = new SqlParameter[3];
            parametersReg[0] = new SqlParameter(parameters[0].ParameterName, parameters[0].SqlDbType);
            parametersReg[0].Value = parameters[0].Value;
            parametersReg[1] = new SqlParameter(parameters[1].ParameterName, parameters[1].SqlDbType);
            parametersReg[1].Value = parameters[1].Value;
            parametersReg[2] = new SqlParameter(parameters[2].ParameterName, parameters[2].SqlDbType, parameters[2].Size);
            parametersReg[2].Value = parameters[2].Value;

            SqlParameter[] parametersAddr = new SqlParameter[3];
            parametersAddr[0] = new SqlParameter(parameters[0].ParameterName, parameters[0].SqlDbType);
            parametersAddr[0].Value = parameters[0].Value;
            parametersAddr[1] = new SqlParameter(parameters[1].ParameterName, parameters[1].SqlDbType);
            parametersAddr[1].Value = parameters[1].Value;
            parametersAddr[2] = new SqlParameter(parameters[2].ParameterName, parameters[2].SqlDbType, parameters[2].Size);
            parametersAddr[2].Value = parameters[2].Value;
            try
            {
                using (transactionWork = (TransactionWork) TransactionFactory.Create())
                {
                    transactionWork.Execute(_tables.CustomerDataTable, "uspGetCustomerByBirthOfDay", parameters);
                    transactionWork.Execute(_tables.InvalidDataTable, "uspGetInvalidByBirthOfDay", parametersInv);
                    transactionWork.Execute(_tables.InvalidBenefitsDataTable, "uspGetInvalidBenefitsByBirthOfDay", parametersInvBenefits);
                    transactionWork.Execute(_tables.RegisterDataTable, "uspGetRegisterByBirthOfDay", parametersReg);
                    transactionWork.Execute(_tables.AddressDataTable, "uspGetAddressByBirthOfDay", parametersAddr);
                    transactionWork.Commit();
                }
            }
            catch (Exception)
            {
                transactionWork?.Rollback();
                throw;
            }
        }


        public static void GetCustomersByGlossary(string name, string glossaryName, int criteria)
        {
            ClearData();
            GetDataByGlossary(glossaryName, criteria);
        }

        private static void GetDataByGlossary(string glossaryName, int criteria)
        {
            TransactionWork transactionWork = null;
            string storageProcedureName = string.Format("uspGet{0}By{1}", "Customer", glossaryName);
            SqlParameter parameter = new SqlParameter();
            parameter.DbType = DbType.Int32;
            parameter.ParameterName = "@ID";
            parameter.Value = criteria;
            string storageProcedureNameReg = string.Format("uspGet{0}By{1}", "Register", glossaryName);
            SqlParameter parameterReg = new SqlParameter();
            parameterReg.DbType = DbType.Int32;
            parameterReg.ParameterName = "@ID";
            parameterReg.Value = criteria;
            string storageProcedureNameInv = string.Format("uspGet{0}By{1}", "Invalid", glossaryName);
            SqlParameter parameterInv = new SqlParameter();
            parameterInv.DbType = DbType.Int32;
            parameterInv.ParameterName = "@ID";
            parameterInv.Value = criteria;

            string storageProcedureNameInvBenefits = string.Format("uspGet{0}By{1}", "InvalidBenefits", glossaryName);
            SqlParameter parameterInvBenefits = new SqlParameter();
            parameterInvBenefits.DbType = DbType.Int32;
            parameterInvBenefits.ParameterName = "@ID";
            parameterInvBenefits.Value = criteria;

            string storageProcedureNameAddr = string.Format("uspGet{0}By{1}", "Address", glossaryName);
            SqlParameter parameterAddr = new SqlParameter();
            parameterAddr.DbType = DbType.Int32;
            parameterAddr.ParameterName = "@ID";
            parameterAddr.Value = criteria;
            try
            {
                using (transactionWork = (TransactionWork)TransactionFactory.Create())
                {
                    transactionWork.Execute(_tables.CustomerDataTable, storageProcedureName, parameter);
                    transactionWork.Execute(_tables.RegisterDataTable, storageProcedureNameReg, parameterReg);
                    transactionWork.Execute(_tables.InvalidDataTable, storageProcedureNameInv, parameterInv);
                    transactionWork.Execute(_tables.InvalidBenefitsDataTable, storageProcedureNameInvBenefits, parameterInvBenefits);
                    transactionWork.Execute(_tables.AddressDataTable, storageProcedureNameAddr, parameterAddr);
                    transactionWork.Commit();
                }
            }
            catch (Exception)
            {
                transactionWork?.Rollback();
                throw;
            }
        }

        public static void Update()
        {
            TransactionWork transactionWork = null;
            try
            {
                using (transactionWork = (TransactionWork)TransactionFactory.Create())
                {
                    for (int i = 0; i < _tables.DispancerDataSet.Tables.Count; i++)
                    {
                        transactionWork.UpdateData(_tables.DispancerDataSet.Tables[i]);
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
        private static void GetDataByCriteria(DataTable table, string storageProcedureName, string parameterName, string value)
        {
           
            TransactionWork transactionWork = null;
           
            SqlParameter parameter = new SqlParameter();
            parameter.DbType = DbType.String;
            parameter.Size = 100;
            parameter.ParameterName = parameterName;
            SqlParameter parameterCity = new SqlParameter();
            parameterCity.DbType = DbType.String;
            parameterCity.Size = 100;
            parameterCity.ParameterName = "@city";

            SqlParameter parameterReg = new SqlParameter();
            parameterReg.DbType = DbType.String;
            parameterReg.Size = 100;
            parameterReg.ParameterName = parameterName;
            SqlParameter parameterCityReg = new SqlParameter();
            parameterCityReg.DbType = DbType.String;
            parameterCityReg.Size = 100;
            parameterCityReg.ParameterName = "@city";

            SqlParameter parameterInv = new SqlParameter();
            parameterInv.DbType = DbType.String;
            parameterInv.Size = 100;
            parameterInv.ParameterName = parameterName;
            SqlParameter parameterCityInv = new SqlParameter();
            parameterCityInv.DbType = DbType.String;
            parameterCityInv.Size = 100;
            parameterCityInv.ParameterName = "@city";
            SqlParameter parameterInvBenefits = new SqlParameter();
            parameterInvBenefits.DbType = DbType.String;
            parameterInvBenefits.Size = 100;
            parameterInvBenefits.ParameterName = parameterName;
            SqlParameter parameterCityInvBenefits = new SqlParameter();
            parameterCityInvBenefits.DbType = DbType.String;
            parameterCityInvBenefits.Size = 100;
            parameterCityInvBenefits.ParameterName = "@city";
            SqlParameter parameterAddr = new SqlParameter();
            parameterAddr.DbType = DbType.String;
            parameterAddr.Size = 100;
            parameterAddr.ParameterName = parameterName;
            SqlParameter parameterCityAddr = new SqlParameter();
            parameterCityAddr.DbType = DbType.String;
            parameterCityAddr.Size = 100;
            parameterCityAddr.ParameterName = "@city";
            try
            {
                parameter.Value = value;
                parameterCity.Value = "";
                parameterReg.Value = value;
                parameterCityReg.Value = "";
                parameterInv.Value = value;
                parameterCityInv.Value = "";
                parameterCityInvBenefits.Value = "";
                parameterInvBenefits.Value = value;
                parameterAddr.Value = value;
                parameterCityAddr.Value = "";
                using (transactionWork = (TransactionWork)TransactionFactory.Create())
                {
                    transactionWork.Execute(_tables.CustomerDataTable, storageProcedureName, parameter, parameterCity);
                    transactionWork.Execute(_tables.RegisterDataTable, "uspGetRegisterByAddress", parameterReg, parameterCityReg);
                    transactionWork.Execute(_tables.InvalidDataTable,  "uspGetInvalidByAddress", parameterInv, parameterCityInv);
                    transactionWork.Execute(_tables.InvalidBenefitsDataTable, "uspGetInvalidBenefitsByAddress", parameterInvBenefits, parameterCityInvBenefits);
                    transactionWork.Execute(_tables.AddressDataTable, "uspGetAddress", parameterAddr, parameterCityAddr);
                    transactionWork.Commit();
                }
            }
            catch (Exception)
            {
                transactionWork?.Rollback();
                throw;
            }
        }
        public static void GetCustomersByAddress( string street)
        {
            ClearData();
            GetDataByCriteria(_tables.CustomerDataTable,   "uspGetCustomerByAddress", "@NameStreet", street);
        }

        public static void ClearData()
        {

            _tables.AddressDataTable.Clear();
            _tables.InvalidBenefitsDataTable.Clear();
            _tables.InvalidDataTable.Clear();
            _tables.RegisterDataTable.Clear();
            _tables.ErrorDataTable.Clear();
            _tables.CustomerDataTable.Clear();
        }

        public static object GetByEntityName(string name)
        {
            return _tables.DispancerDataSet.Tables[name];
        }

        public static void SaveEntity(string entityName)
        {
            TransactionWork transactionWork = null;
            try
            {
                using (transactionWork = (TransactionWork)TransactionFactory.Create())
                {
                    for (int i = 0; i < _tables.DispancerDataSet.Tables.Count; i++)
                    {
                        if (_tables.DispancerDataSet.Tables[i].TableName == entityName)
                        {
                            transactionWork.UpdateData(_tables.DispancerDataSet.Tables[i]);
                            break;
                        }
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
    }
}
