using System;
using System.Data;
using System.Data.SqlClient;

namespace SOPB.Accounting.DAL.TableAdapters.Glossary
{
    public sealed class TypeStreetTableAdapter : BaseTableAdapter
    {
        /// <summary>
        /// _commandCollection[0] = [R]
        /// </summary>
        protected override void InitCollection()
        {
            this._commandCollection = new SqlCommand[1];
            this._commandCollection[0] = new SqlCommand();
            this._commandCollection[0].Connection = this.Connection;
            this._commandCollection[0].CommandText = $"SELECT   [TypeStreetID] \n"
                                                     + "      ,[Name] \n"
                                                     + "      ,[SocrName] \n"
                                                     + "  FROM [dbo].[vGetTypeStreet]";
            this._commandCollection[0].CommandType = CommandType.Text;
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
