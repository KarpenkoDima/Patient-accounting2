using System.Data;
using System.Data.SqlClient;
using SOPB.Accounting.DAL.LoadData;

namespace SOPB.Accounting.DAL.ConnectionManager
{
    /// <summary>
    /// What are You?
    /// </summary>
    public static class RoleForUser
    {
        public static string GetRoleForUser(string login, string password)
        {
            string role = null;
            ConnectionManager.SetConnection(login, password);
            //SqlConnection connection = ConnectionManager.Connection;


            using (SqlCommand command = GenericDataAccess.CreateCommand(ConnectionManager.Connection.CreateCommand()) as SqlCommand)
            {
                {
                    if (command != null)
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "uspGetRoleForUser";
                        SqlParameter parameter = new SqlParameter
                        {
                            ParameterName = "@Role",
                            Size = 50,
                            SqlDbType = SqlDbType.NVarChar,
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(parameter);

                        command.Connection.Open();
                        command.ExecuteNonQuery();
                        role = ((string) command.Parameters["@Role"].Value).Trim();
                        command.Connection.Close();
                    }
                }

                return role;
            }
        }
    }
}
