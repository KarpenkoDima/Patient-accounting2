using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace SOPB.Dispensary.DAL.TableAdapters.CustomerTableAdapters
{
    public class InvalidTableAdapter : BaseTableAdapter
    {
        protected override void InitCollection()
        {
            this._commandCollection = new SqlCommand[3];
            this._commandCollection[0] = new SqlCommand();
            this._commandCollection[0].Connection = this.Connection;
            this._commandCollection[0].CommandText = string.Format("SELECT   V.[InvalidID] \n"
                                                                   + "      ,V.[DisabilityGroupID] \n"
                                                                   + "      ,V.[DataInvalidity] \n"
                                                                   + "      ,V.[PeriodInvalidity] \n"
                                                                   + "      ,V.[ChiperReceptID] \n"
                                                                   + "      ,V.[Incapable] \n"
                                                                   + "      ,V.[DateIncapable] \n"
                                                                   + "      ,V.[CustomerID] \n"
                                                                   + "       FROM vGetInvalid AS V \n"
                                                                   + "  INNER JOIN dbo.{0} AS c \n"
                                                                   + "  ON c.CustomerID = V.CustomerID \n", "vGetCustomers");

            this._commandCollection[0].CommandType = CommandType.Text;

            this._commandCollection[1] = new SqlCommand();
            this._commandCollection[1].Connection = this.Connection;
            this._commandCollection[1].CommandText = "uspSaveInvalid";
            this._commandCollection[1].CommandType = CommandType.StoredProcedure;
            this._commandCollection[1].Parameters.Add(new SqlParameter("@InvalidID", SqlDbType.Int, 4,
                ParameterDirection.InputOutput, false, 10, 0, "InvalidID", DataRowVersion.Current, null));
            this._commandCollection[1].Parameters.Add(new SqlParameter("@DisabilityGroupID", SqlDbType.Int, 4,
                ParameterDirection.Input, true, 23, 3, "DisabilityGroupID", DataRowVersion.Current, null));
            this._commandCollection[1].Parameters.Add(new SqlParameter("@DataInvalidity", SqlDbType.DateTime, 8,
                ParameterDirection.Input, true, 23, 3, "DataInvalidity", DataRowVersion.Current, null));
            this._commandCollection[1].Parameters.Add(new SqlParameter("@PeriodInvalidity", SqlDbType.DateTime, 8,
                ParameterDirection.Input, true, 23, 3, "PeriodInvalidity", DataRowVersion.Current, null));
            //this._commandCollection[2].Parameters.Add(new SqlParameter("@PeriodInvalidity", SqlDbType.DateTime, 8,
            //    ParameterDirection.Input, true, 0, 0, "PeriodInvalidity", DataRowVersion.Current, null));
            this._commandCollection[1].Parameters.Add(new SqlParameter("@ChiperReceptID", SqlDbType.Int, 4,
                ParameterDirection.Input, true, 0, 0, "ChiperReceptID", DataRowVersion.Current, null));
            this._commandCollection[1].Parameters.Add(new SqlParameter("@CustomerID", SqlDbType.Int, 4,
                ParameterDirection.Input, false, 10, 0, "CustomerID", DataRowVersion.Current, null));
            this._commandCollection[1].Parameters.Add(new SqlParameter("@Incapable", SqlDbType.Bit, 1,
                ParameterDirection.Input, false, 0, 0, "Incapable", DataRowVersion.Current, null));
            this._commandCollection[1].Parameters.Add(new SqlParameter("@DateIncapable", SqlDbType.DateTime, 8,
                ParameterDirection.Input, true, 10, 0, "DateIncapable", DataRowVersion.Current, null));

            this._commandCollection[2] = new SqlCommand();
            this._commandCollection[2].CommandText = "dbo.uspDeleteInvalid";
            this._commandCollection[2].Connection = this.Connection;
            this._commandCollection[2].CommandType = CommandType.StoredProcedure;
            this._commandCollection[2].Parameters.Add(new SqlParameter("@RETURN_VALUE", SqlDbType.Int, 4,
                ParameterDirection.ReturnValue, false, 10, 0, null, DataRowVersion.Current, null));
            this._commandCollection[2].Parameters.Add(new SqlParameter("@InvalidID", SqlDbType.Int, 4,
                ParameterDirection.Input, false, 10, 0, "InvalidID", DataRowVersion.Current, null));
        }

        public override int Fill(DataTable invalidateDataTable)
        {
            this.Adapter.SelectCommand = this._commandCollection[0];

            if (ClearBefore)
            {
                invalidateDataTable.Clear();
            }
            try
            {
                return this.Adapter.Fill(invalidateDataTable);
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
    }

    public sealed class InvalidBenefitsCategoryTableAdapter : BaseTableAdapter
    {
        protected override void InitCollection()
        {
            this._commandCollection = new SqlCommand[3];
            this._commandCollection[0] = new SqlCommand();
            this._commandCollection[0].Connection = this.Connection;
            this._commandCollection[0].CommandText = string.Format("SELECT  [invID] \n"
                                                                   + "    ,[BenefitsID] \n"
                                                                   +
                                                                   " FROM [dbo].[vGetInvalid_BenefitsCategory] AS ibc \n"
                                                                   + "INNER JOIN vGetInvalid AS i \n"
                                                                   + " ON i.InvalidID = ibc.invID \n"
                                                                   + "INNER JOIN {0} AS c \n"
                                                                   + " ON c.CustomerID = i.CustomerID \n", "vGetCustomers");
            this._commandCollection[0].CommandType = CommandType.Text;

            this._commandCollection[1] = new SqlCommand();
            this._commandCollection[1].Connection = this.Connection;
            this._commandCollection[1].CommandText = "[uspSaveInvalidBenefitsCategory]";
            this._commandCollection[1].CommandType = CommandType.StoredProcedure;
            this._commandCollection[1].Parameters.Add(new SqlParameter("@InvalidID", SqlDbType.Int, 4,
                ParameterDirection.Input, false, 10, 0, "InvID", DataRowVersion.Current, null));
            this._commandCollection[1].Parameters.Add(new SqlParameter("@BenefitsCategoryID", SqlDbType.Int, 4,
                ParameterDirection.Input, true, 23, 3, "BenefitsID", DataRowVersion.Current, null));

            this._commandCollection[2] = new SqlCommand();
            this._commandCollection[2].CommandText = "dbo.[uspDeleteInvalidBenefitsCategory]";
            this._commandCollection[2].Connection = this.Connection;
            this._commandCollection[2].CommandType = CommandType.StoredProcedure;
            this._commandCollection[2].Parameters.Add(new SqlParameter("@InvalidID", SqlDbType.Int, 4,
                ParameterDirection.Input, false, 10, 0, "InvID", DataRowVersion.Current, null));
            this._commandCollection[2].Parameters.Add(new SqlParameter("@BenefitsCategory", SqlDbType.Int, 4,
                ParameterDirection.Input, true, 10, 0, "BenefitsID", DataRowVersion.Current, null));


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
    }
}
