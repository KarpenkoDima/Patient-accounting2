using System;
using System.Data;
using System.Data.SqlClient;

namespace SOPB.Accounting.DAL.TableAdapters.CustomerTableAdapter
{
    public class RegisterTableAdapter : UpdateBaseTableAdapter
    {
        protected override void InitCollection()
        {
            this._commandCollection = new SqlCommand[5];
            this._commandCollection[0] = new SqlCommand();
            this._commandCollection[0].Connection = this.Connection;
            this._commandCollection[0].CommandText = string.Format("SELECT   V.[RegisterID] \n"
                                                 + "      ,V.[FirstRegister] \n"
                                                 + "      ,V.[FirstDeregister] \n"
                                                 + "      ,V.[SecondRegister] \n"
                                                 + "      ,V.[Diagnosis] \n"
                                                 + "      ,V.[RegisterTypeID] \n"
                                                 + "      ,V.[CustomerID] \n"
                                                 + "      ,V.[WhyDeRegisterID] \n"
                                                 + "      ,V.[SecondDeRegister] \n"
                                                 + "      ,V.[SecondRegisterTypeID] \n"
                                                 + "      ,V.[WhySecondDeRegisterID] \n"
                                                 + "      ,V.[LandID] \n"
                                                 + "      ,V.[NotaBene] \n"
                                                 + "  FROM [dbo].[vGetRegister] AS V"
                                                 + "  INNER JOIN dbo.{0} AS c \n"
                                                 + "  ON c.CustomerID = V.CustomerID \n", "vGetCustomers");
            this._commandCollection[0].CommandType = CommandType.Text;
        
            this._commandCollection[1] = new SqlCommand();
            this._commandCollection[1].Connection = this.Connection;
            this._commandCollection[1].CommandText = "uspSaveRegister";
            this._commandCollection[1].CommandType = CommandType.StoredProcedure;
            this._commandCollection[1].Parameters.Add(new SqlParameter("@RegisterID", SqlDbType.Int, 4,
                ParameterDirection.InputOutput, false, 10, 0, "RegisterID", DataRowVersion.Current, null));
            this._commandCollection[1].Parameters.Add(new SqlParameter("@FirstRegister", SqlDbType.DateTime, 8,
                ParameterDirection.Input, true, 23, 3, "FirstRegister", DataRowVersion.Current, null));
            this._commandCollection[1].Parameters.Add(new SqlParameter("@FirstDeregister", SqlDbType.DateTime, 8,
                ParameterDirection.Input, true, 23, 3, "FirstDeregister", DataRowVersion.Current, null));
            this._commandCollection[1].Parameters.Add(new SqlParameter("@SecondRegister", SqlDbType.DateTime, 8,
                ParameterDirection.Input, true, 23, 3, "SecondRegister", DataRowVersion.Current, null));
            this._commandCollection[1].Parameters.Add(new SqlParameter("@SecondDeRegister", SqlDbType.DateTime, 8,
               ParameterDirection.Input, true, 23, 3, "SecondDeRegister", DataRowVersion.Current, null));
            this._commandCollection[1].Parameters.Add(new SqlParameter("@Diagnosis", SqlDbType.NVarChar, 10,
                ParameterDirection.Input, true, 0, 0, "Diagnosis", DataRowVersion.Current, null));
            this._commandCollection[1].Parameters.Add(new SqlParameter("@RegisterTypeID", SqlDbType.Int, 4,
                 ParameterDirection.Input, true, 0, 0, "RegisterTypeID", DataRowVersion.Current, null));
            this._commandCollection[1].Parameters.Add(new SqlParameter("@SecondRegisterTypeID", SqlDbType.Int, 4,
                ParameterDirection.Input, true, 0, 0, "SecondRegisterTypeID", DataRowVersion.Current, null));
            this._commandCollection[1].Parameters.Add(new SqlParameter("@CustomerID", SqlDbType.Int, 4,
                ParameterDirection.Input, false, 10, 0, "CustomerID", DataRowVersion.Current, null));
            this._commandCollection[1].Parameters.Add(new SqlParameter("@WhyDeRegister", SqlDbType.Int, 4,
                ParameterDirection.Input, true, 0, 0, "WhyDeRegisterID", DataRowVersion.Current, null));
            this._commandCollection[1].Parameters.Add(new SqlParameter("@WhySecondDeRegister", SqlDbType.Int, 4,
                ParameterDirection.Input, true, 0, 0, "WhySecondDeRegisterID", DataRowVersion.Current, null));
            this._commandCollection[1].Parameters.Add(new SqlParameter("@LandID", SqlDbType.Int, 4,
                ParameterDirection.Input, false, 10, 0, "LandID", DataRowVersion.Current, null));
            this._commandCollection[1].Parameters.Add(new SqlParameter("@NotaBene", SqlDbType.NText, 2000,
                ParameterDirection.Input, true, 10, 0, "NotaBene", DataRowVersion.Current, null));
            
            this._commandCollection[2] = new SqlCommand();
            this._commandCollection[2].CommandText = "dbo.uspDeleteRegister";
            this._commandCollection[2].Connection = this.Connection;
            this._commandCollection[2].CommandType = CommandType.StoredProcedure;
            this._commandCollection[2].Parameters.Add(new SqlParameter("@RETURN_VALUE", SqlDbType.Int, 4,
                ParameterDirection.ReturnValue, false, 10, 0, null, DataRowVersion.Current, null));
            this._commandCollection[2].Parameters.Add(new SqlParameter("@RegisterID", SqlDbType.Int, 4,
                ParameterDirection.Input, false, 10, 0, "RegisterID", DataRowVersion.Current, null));

            this._commandCollection[3] = new SqlCommand();
            this._commandCollection[3].Connection = this.Connection;
            this._commandCollection[3].CommandText = "dbo.uspGetRegister";
            this._commandCollection[3].CommandType = CommandType.StoredProcedure;
            this._commandCollection[3].Parameters.Add(new SqlParameter("@RETURN_VALUE", SqlDbType.Int, 4,
                ParameterDirection.ReturnValue, false, 10, 0, null, DataRowVersion.Current, null));
            this._commandCollection[3].Parameters.Add(new SqlParameter("@RegisterID", SqlDbType.Int, 4,
                ParameterDirection.Input, false, 10, 0, "RegisterID", DataRowVersion.Current, null));

            this._commandCollection[4] = new SqlCommand();
            this._commandCollection[4].Connection = this.Connection;
            this._commandCollection[4].CommandText = "SELECT [RegisterID] \n"
                                                     + "      ,[FirstRegister] \n"
                                                     + "      ,[FirstDeregister] \n"
                                                     + "      ,[SecondRegister] \n"
                                                     + "      ,[Diagnosis] \n"
                                                     + "      ,[RegisterTypeID] \n"
                                                     + "      ,[CustomerID] \n"
                                                     + "      ,[WhyDeRegisterID] \n"
                                                     + "      ,[SecondDeRegister] \n"
                                                     + "      ,[SecondRegisterTypeID] \n"
                                                     + "      ,[WhySecondDeRegisterID] \n"
                                                     + "      ,[LandID] \n"
                                                     + "      ,[NotaBene] \n"
                                                     + "  FROM [dbo].[vGetRegisterFromArch]";
            this._commandCollection[4].CommandType = CommandType.Text;
        }

        public override int Fill(DataTable table)
        {
            this.Adapter.SelectCommand = this._commandCollection[0];
            if (ClearBefore)
            {
                table.Clear();
            }

            return this.Adapter.Fill(table);
        }
        public int FillFromArch(DataTable table)
        {
            this.Adapter.SelectCommand = this._commandCollection[4];
            if (ClearBefore)
            {
                table.Clear();
            }

            return this.Adapter.Fill(table);
        }


    }
}
