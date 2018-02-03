using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace SOPB.Dispensary.DAL.TableAdapters.CustomerTableAdapters
{
   public sealed partial class AddressTableAdapter : BaseTableAdapter
    {
        /// <summary>
        /// _commandCollection[] = [R][C|U][D]+[R arch]
        /// </summary>
        protected override void InitCollection()
        {
            this._commandCollection = new SqlCommand[4];
            this._commandCollection[0] = new SqlCommand();
            this._commandCollection[0].Connection = this.Connection;
            this._commandCollection[0].CommandText = string.Format("SELECT   V.[AddressID] \n"
                                                                   + "      ,V.[CustomerID] \n"
                                                                   + "      ,V.[Region] \n"
                                                                   + "      ,V.[City] \n"
                                                                   + "      ,V.[AdminDivisionID] \n"
                                                                   + "      ,V.[TypeStreetID] \n"
                                                                   + "      ,V.[NameStreet] \n"
                                                                   + "      ,V.[NumberHouse] \n"
                                                                   + "      ,V.[NumberApartment] \n"
                                                                   + "      ,V.[ModifiedDate] \n"
                                                                   + "  FROM [dbo].[vGetAddress] AS V"
                                                                   + "  INNER JOIN dbo.{0} AS c \n"
                                                                   + "  ON c.CustomerID = V.CustomerID \n", "vGetCustomers");

            base._commandCollection[0].CommandType = CommandType.Text;

         base._commandCollection[1] = new SqlCommand();
            base._commandCollection[1].Connection = this.Connection;
            base._commandCollection[1].CommandText = "uspSaveAddress";
            base._commandCollection[1].CommandType = CommandType.StoredProcedure;
            base._commandCollection[1].Parameters.Add(new SqlParameter("@RETURN_VALUE", SqlDbType.Int, 4,
               ParameterDirection.ReturnValue, false, 10, 0, null, DataRowVersion.Current, null));
            base._commandCollection[1].Parameters.Add(new SqlParameter("@AddressID", SqlDbType.Int, 4,
                ParameterDirection.InputOutput, false, 10, 0, "AddressID", DataRowVersion.Current, null));
            base._commandCollection[1].Parameters.Add(new SqlParameter("@Region", SqlDbType.NChar, 50,
                ParameterDirection.Input, true, 23, 3, "Region", DataRowVersion.Current, null));
            base._commandCollection[1].Parameters.Add(new SqlParameter("@Contry", SqlDbType.NChar, 50,
                ParameterDirection.Input, true, 23, 3, "Contry", DataRowVersion.Current, null));
            base._commandCollection[1].Parameters.Add(new SqlParameter("@City", SqlDbType.NVarChar, 100,
                ParameterDirection.Input, false, 23, 3, "City", DataRowVersion.Current, null));
            base._commandCollection[1].Parameters.Add(new SqlParameter("@AdminDivisionID", SqlDbType.Int, 8,
                ParameterDirection.Input, false, 0, 0, "AdminDivisionID", DataRowVersion.Current, null));
            base._commandCollection[1].Parameters.Add(new SqlParameter("@TypeStreetID", SqlDbType.Int, 4,
                ParameterDirection.Input, true, 0, 0, "TypeStreetID", DataRowVersion.Current, null));
            base._commandCollection[1].Parameters.Add(new SqlParameter("@CustomerID", SqlDbType.Int, 4,
                ParameterDirection.Input, false, 10, 0, "CustomerID", DataRowVersion.Current, null));
            base._commandCollection[1].Parameters.Add(new SqlParameter("@NameStreet", SqlDbType.NVarChar, 100,
                ParameterDirection.Input, true, 0, 0, "NameStreet", DataRowVersion.Current, null));
            base._commandCollection[1].Parameters.Add(new SqlParameter("@NumberHouse", SqlDbType.NVarChar, 5,
                ParameterDirection.Input, true, 10, 0, "NumberHouse", DataRowVersion.Current, null));
            base._commandCollection[1].Parameters.Add(new SqlParameter("@NumberApartment", SqlDbType.NVarChar, 5,
                ParameterDirection.Input, true, 10, 0, "NumberApartment", DataRowVersion.Current, null));

            base._commandCollection[2] = new SqlCommand();
            base._commandCollection[2].CommandText = "dbo.[uspDeleteAddress]";
            base._commandCollection[2].Connection = this.Connection;
            base._commandCollection[2].CommandType = CommandType.StoredProcedure;
            base._commandCollection[2].Parameters.Add(new SqlParameter("@RETURN_VALUE", SqlDbType.Int, 4,
                ParameterDirection.ReturnValue, false, 10, 0, null, DataRowVersion.Current, null));
            base._commandCollection[2].Parameters.Add(new SqlParameter("@AddressID", SqlDbType.Int, 4,
                ParameterDirection.Input, false, 10, 0, "AddressID", DataRowVersion.Current, null));

   

            this._commandCollection[3] = new SqlCommand();
            this._commandCollection[3].Connection = this.Connection;
            this._commandCollection[3].CommandText = "SELECT [AddressID] \n"
                                                     + "      ,[CustomerID] \n"
                                                     + "      ,[Region] \n"
                                                     + "      ,[Contry] \n"
                                                     + "      ,[City] \n"
                                                     + "      ,[AdminDivisionID] \n"
                                                     + "      ,[TypeStreetID] \n"
                                                     + "      ,[NameStreet] \n"
                                                     + "      ,[NumberHouse] \n"
                                                     + "      ,[NumberApartment] \n"
                                                     + "      ,[ModifiedDate] \n"
                                                     + "  FROM [dbo].[vGetAddressFromArch]";
            base._commandCollection[3].CommandType = CommandType.Text;
        }


        public override int Fill(DataTable dataTable)
        {
            this.Adapter.SelectCommand = this._commandCollection[0];
            if (ClearBefore)
            {
                dataTable.Clear();
            }
            try
            {

                return this.Adapter.Fill(dataTable);
            }
            catch (InvalidOperationException)
            {

                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
