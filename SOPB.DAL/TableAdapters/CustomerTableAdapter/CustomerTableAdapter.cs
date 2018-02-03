using System.Data;
using System.Data.SqlClient;

namespace SOPB.Accounting.DAL.TableAdapters.CustomerTableAdapter
{
    public sealed class CustomerTableAdapter : UpdateBaseTableAdapter
    {

        /// <summary>
        /// _commandCollection[] = [R][C|U][D]+[R arch]
        /// </summary>
        protected override void InitCollection()
        {
            this._commandCollection = new SqlCommand[4];
            this._commandCollection[0] = new SqlCommand();
            this._commandCollection[0].Connection = Connection;
            this._commandCollection[0].CommandText = "SELECT [CustomerID] \n"
                                                     + "      ,[MedCard] \n"
                                                     + "      ,[CodeCustomer] \n"
                                                     + "      ,[LastName] \n"
                                                     + "      ,[FirstName] \n"
                                                     + "      ,[MiddleName] \n"
                                                     + "      ,[Birthday] \n"
                                                     + "      ,[Arch] \n"
                                                     + "      ,[APPPTPRID] \n"
                                                     + "      ,[GenderID] \n"
                                                     + "      ,[NotaBene] \n"
                                                     + "  FROM [dbo].[vGetCustomers]";
            this._commandCollection[0].CommandType = CommandType.Text;

            this._commandCollection[1] = new SqlCommand();
            this._commandCollection[1].Connection = Connection;
            this._commandCollection[1].CommandText = "uspSaveCustomer";
            this._commandCollection[1].CommandType = CommandType.StoredProcedure;
            this._commandCollection[1].Parameters.Add(new SqlParameter("@RETURN_VALUE", SqlDbType.Int, 4,
                ParameterDirection.ReturnValue, false, 10, 0, null, DataRowVersion.Current, null));
            this._commandCollection[1].Parameters.Add(new SqlParameter("@CustomerID", SqlDbType.Int, 4,
                ParameterDirection.InputOutput, false, 10, 0, "CustomerID", DataRowVersion.Current, null));
            this._commandCollection[1].Parameters.Add(new SqlParameter("@MedCard", SqlDbType.Int, 4,
                ParameterDirection.Input, true, 10, 0, "MedCard", DataRowVersion.Current, null));
            this._commandCollection[1].Parameters.Add(new SqlParameter("@CodeCustomer", SqlDbType.Int, 4,
                ParameterDirection.Input, true, 10, 0, "CodeCustomer", DataRowVersion.Current, null));
            this._commandCollection[1].Parameters.Add(new SqlParameter("@LastName", SqlDbType.NVarChar, 100,
                ParameterDirection.Input, false, 0, 0, "LastName", DataRowVersion.Current, null));
            this._commandCollection[1].Parameters.Add(new SqlParameter("@FirstName", SqlDbType.NVarChar, 100,
                ParameterDirection.Input, false, 0, 0, "FirstName", DataRowVersion.Current, null));
            this._commandCollection[1].Parameters.Add(new SqlParameter("@MiddleName", SqlDbType.NVarChar, 100,
                ParameterDirection.Input, true, 0, 0, "MiddleName", DataRowVersion.Current, null));
            this._commandCollection[1].Parameters.Add(new SqlParameter("@Birthday", SqlDbType.DateTime, 8,
                ParameterDirection.Input, true, 23, 3, "Birthday", DataRowVersion.Current, null));
            this._commandCollection[1].Parameters.Add(new SqlParameter("@Arch", SqlDbType.Bit, 1,
                ParameterDirection.Input, true, 0, 0, "Arch", DataRowVersion.Current, null));
            this._commandCollection[1].Parameters.Add(new SqlParameter("@NotaBene", SqlDbType.NText, 2000,
                ParameterDirection.Input, true, 0, 0, "NotaBene", DataRowVersion.Current, null));
            this._commandCollection[1].Parameters.Add(new SqlParameter("@APPPTPRID", SqlDbType.Int, 4,
                ParameterDirection.Input, true, 0, 0, "APPPTPRID", DataRowVersion.Current, null));
            this._commandCollection[1].Parameters.Add(new SqlParameter("@GenderID", SqlDbType.Int, 4,
                ParameterDirection.Input, true, 0, 0, "GenderID", DataRowVersion.Current, null));

            this._commandCollection[2] = new SqlCommand();
            this._commandCollection[2].Connection = Connection;
            this._commandCollection[2].CommandText = "uspDeleteCustomer";
            this._commandCollection[2].CommandType = CommandType.StoredProcedure;
            this._commandCollection[2].Parameters.Add(new SqlParameter("@RETURN_VALUE", SqlDbType.Int, 4,
                ParameterDirection.ReturnValue, false, 10, 0, null, DataRowVersion.Current, null));
            this._commandCollection[2].Parameters.Add(new SqlParameter("@CustomerID", SqlDbType.Int, 4,
                ParameterDirection.Input, false, 10, 0, "CustomerID", DataRowVersion.Current, null));

            this._commandCollection[3] = new SqlCommand();
            this._commandCollection[3].Connection = Connection;
            this._commandCollection[3].CommandText = "SELECT [CustomerID] \n"
                                                     + "      ,[MedCard] \n"
                                                     + "      ,[CodeCustomer] \n"
                                                     + "      ,[LastName] \n"
                                                     + "      ,[FirstName] \n"
                                                     + "      ,[MiddleName] \n"
                                                     + "      ,[Birthday] \n"
                                                     + "      ,[Arch] \n"
                                                     + "      ,[APPPTPRID] \n"
                                                     + "      ,[GenderID] \n"
                                                     + "      ,[NotaBene] \n"
                                                     + "  FROM [dbo].[vGetCustomersFromArch]";
            this._commandCollection[3].CommandType = CommandType.Text;
        }

        public override int Fill(DataTable dataTable)
        {
            this.Adapter.SelectCommand = CommandCollection[0];
            if (this.ClearBefore)
            {
                dataTable.Clear();
            }

            return this.Adapter.Fill(dataTable);

        }

        public int FillToAcrh(DataTable customerTable)
        {
            this.Adapter.SelectCommand = CommandCollection[3];
            if (ClearBefore)
            {
                customerTable.Clear();
            }
            return this.Adapter.Fill(customerTable);
        }
    }
}
