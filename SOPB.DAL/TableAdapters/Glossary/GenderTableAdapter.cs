using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace SOPB.Accounting.DAL.TableAdapters.Glossary
{
    /// <summary>
    /// 
    /// </summary>
    public class GenderTableAdapter : BaseTableAdapter
    {

        protected override void InitCollection()
        {
            this._commandCollection = new SqlCommand[1];
            this._commandCollection[0] = new SqlCommand();
            this._commandCollection[0].Connection = this.Connection;
            this._commandCollection[0].CommandText = $"SELECT [GenderID] \n"
                                                     + "      ,[Name] \n"
                                                     + "  FROM [dbo].[vGetGender]";
            this._commandCollection[0].CommandType = CommandType.Text;
        }

        public override int Fill(DataTable table)
        {
            this.Adapter.SelectCommand = _commandCollection[0];
            try
            {
                if (ClearBefore)
                {
                    table.Clear();
                }
            }
            catch (InvalidConstraintException ex)
            {

            }
            return this.Adapter.Fill(table);
        }

    }
}
