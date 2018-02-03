using System;
using System.Data;
using System.Data.SqlClient;

namespace SOPB.Accounting.DAL.TableAdapters.Glossary
{
    public sealed class BenefitsCategoryTableAdapter : UpdateBaseTableAdapter
    {
        /// <summary>
        /// _commandCollection[] = [R][U|C][D]
        /// </summary>
        protected override void InitCollection()
    {
        this._commandCollection = new SqlCommand[3];
        this._commandCollection[0] = new SqlCommand();
        this._commandCollection[0].Connection = this.Connection;
        this._commandCollection[0].CommandText = $"SELECT   [BenefitsCategoryID] \n"
                                                 + "      ,[Name] \n"
                                                 + "      ,[NotaBene] \n"
                                                 + "  FROM [dbo].[vGetBenefitsCategory]";
        this._commandCollection[0].CommandType = CommandType.Text;

        this._commandCollection[1] = new SqlCommand();
        this._commandCollection[1].Connection = this.Connection;
        this._commandCollection[1].CommandText = "uspSaveBenefitsCategory";
        this._commandCollection[1].CommandType = CommandType.StoredProcedure;
        this._commandCollection[1].Parameters.Add(new SqlParameter("@BenefitsCategoryID", SqlDbType.Int, 4,
            ParameterDirection.Input, false, 10, 0, "BenefitsCategoryID", DataRowVersion.Current, null));
        this._commandCollection[1].Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 100,
            ParameterDirection.Input, false, 23, 3, "Name", DataRowVersion.Current, null));
        this._commandCollection[1].Parameters.Add(new SqlParameter("@NotaBene", SqlDbType.NVarChar, 100,
            ParameterDirection.Input, true, 23, 3, "NotaBene", DataRowVersion.Current, null));

        this._commandCollection[2] = new SqlCommand();
        this._commandCollection[2].CommandText = "dbo.uspDeleteBenefitsCategory";
        this._commandCollection[2].Connection = this.Connection;
        this._commandCollection[2].CommandType = CommandType.StoredProcedure;
        this._commandCollection[2].Parameters.Add(new SqlParameter("@RETURN_VALUE", SqlDbType.Int, 4,
            ParameterDirection.ReturnValue, false, 10, 0, null, DataRowVersion.Current, null));
        this._commandCollection[2].Parameters.Add(new SqlParameter("@BenefitsCategoryID", SqlDbType.Int, 4,
            ParameterDirection.Input, false, 10, 0, "BenefitsCategoryID", DataRowVersion.Current, null));
    }
    public override int Fill(DataTable registerDataTable)
    {
        this.Adapter.SelectCommand = this._commandCollection[0];
        if (ClearBefore)
        {
            registerDataTable.Clear();
        }
        try
        {
            return this.Adapter.Fill(registerDataTable);
        }
        catch (InvalidOperationException)
        {
            throw;
        }
        catch
        {
            throw;
        }
    }
    public int DeleteBenefitsCategory(int benefitsCategoryId)
    {
        SqlCommand command = this._commandCollection[1];
        command.Parameters[1].Value = benefitsCategoryId;
        ConnectionState previousConnectionState = command.Connection.State;
        if ((command.Connection.State & ConnectionState.Open) != ConnectionState.Open)
        {
            command.Connection.Open();
        }
        int returnValue;
        try
        {
            returnValue = command.ExecuteNonQuery();
        }
        finally
        {
            if (previousConnectionState == ConnectionState.Closed)
            {
                command.Connection.Close();
            }
        }
        return returnValue;
    }
}
}
