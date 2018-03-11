using System;
using System.Data;
using System.Data.Common;

namespace SOPB.Accounting.DAL.LoadData
{
    public static class GenericDataAccess
    {
        #region Execute

        public static void ExecuteSelectCommand(IDbCommand command, DataTable table)
        {

            try
            {
                if (command.Connection.State == ConnectionState.Closed)
                    command.Connection.Open();

                var reader = (DbDataReader)command.ExecuteReader();
                if (reader.HasRows)
                {
                    table.Load(reader, LoadOption.OverwriteChanges);
                }
                //reader.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
           
        }

        public static void ExecuteSelectCommand(IDbCommand command, DataSet dataSet)
        {


        }

        public static void ExecuteSelectCommand(IDbCommand command, DataSet dataSet, params DataTable[] tables)
        {

            try
            {
                if (command.Connection.State == ConnectionState.Closed)
                    command.Connection.Open();
                var reader = (DbDataReader)command.ExecuteReader();
                dataSet.Load(reader, LoadOption.OverwriteChanges, tables);
                reader.Close();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                command.Connection.Close();
            }
        }

        #endregion

        public static DbCommand CreateCommand(DbCommand createCommand)
        {
            var command = createCommand;
            command.CommandType = CommandType.StoredProcedure;
            return command;
        }
    }
}
