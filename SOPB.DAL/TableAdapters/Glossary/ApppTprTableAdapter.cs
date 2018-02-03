using System.Data;
using System.Data.SqlClient;

namespace SOPB.Accounting.DAL.TableAdapters.Glossary
{
    public class ApppTprTableAdapter : UpdateBaseTableAdapter
    {
        /// <summary>
        /// ComandCollection[] == [R][U|C][D]
        /// </summary>
        protected override void InitCollection()
        {
            this._commandCollection = new SqlCommand[3];
            this._commandCollection[0] = new SqlCommand();
            this._commandCollection[0].Connection = this.Connection;
            this._commandCollection[0].CommandText = "SELECT [APPPTPRID] \n"
                                                     + "      ,[Name] \n"
                                                     + "  FROM [dbo].[vGetApppTpr]";
            this._commandCollection[0].CommandType = CommandType.Text;

            this._commandCollection[1] = new SqlCommand();
            this._commandCollection[1].Connection = this.Connection;
            this._commandCollection[1].CommandText = "dbo.uspSaveApppTPR";
            this._commandCollection[1].CommandType = CommandType.StoredProcedure;
            base._commandCollection[1].Parameters.Add(new SqlParameter("@RETURN_VALUE", SqlDbType.Int, 4,
               ParameterDirection.ReturnValue, false, 10, 0, null, DataRowVersion.Current, null));
            this._commandCollection[1].Parameters.Add(new SqlParameter("@ApppTprID", SqlDbType.Int, 4,
                ParameterDirection.InputOutput, false, 10, 0, "ApppTprID", DataRowVersion.Current, null));
            this._commandCollection[1].Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 5,
                ParameterDirection.Input, false, 23, 3, "Name", DataRowVersion.Current, null));

            this._commandCollection[2] = new SqlCommand();
            this._commandCollection[2].CommandText = "dbo.uspDeleteApppTpr";
            this._commandCollection[2].Connection = this.Connection;
            this._commandCollection[2].CommandType = CommandType.StoredProcedure;
            this._commandCollection[2].Parameters.Add(new SqlParameter("@RETURN_VALUE", SqlDbType.Int, 4,
                ParameterDirection.ReturnValue, false, 10, 0, null, DataRowVersion.Current, null));
            this._commandCollection[2].Parameters.Add(new SqlParameter("@ApppTprID", SqlDbType.Int, 4,
                ParameterDirection.Input, false, 10, 0, "ApppTprID", DataRowVersion.Current, null));
        }

        public override int Fill(DataTable dataTable)
        {
            this.Adapter.SelectCommand = this._commandCollection[0];
            if (ClearBefore)
            {
                dataTable.Clear();
            }
            return this.Adapter.Fill(dataTable);
        }
    }
}
