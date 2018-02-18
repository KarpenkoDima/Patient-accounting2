using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Security;

namespace SOPB.Accounting.DAL.ConnectionManager
{
    /// <summary>
    /// Repository for PatientAccounting connection settings
    /// </summary>
    public static class ConnectionManager
    {
        private static bool _isInit = true;

        // cache data for connection settings.
        private static readonly string _dbProviderName;
        private static readonly string _dbDatabaseName;
        private static readonly string _dbServerName;
                       
        private static SecureString _secureString;
        private static string _connectionString;
        private static string _userID;
        private static   SqlConnection _sqlConnection;

        static ConnectionManager()
        {
            _dbDatabaseName = ConfigurationManager.AppSettings["DatabaseName"];
            _dbServerName = ConfigurationManager.AppSettings["ServerName"];
            _dbProviderName = ConfigurationManager.AppSettings["ProviderName"];
            
          _secureString=new SecureString();
        }

        /// <summary>
        /// Return SqlConnecction object with Statet Closed.
        /// </summary>
        public static SqlConnection Connection
        {
            get
            {
                SqlCredential cred = new SqlCredential(_userID, _secureString);
                //if (_sqlConnection != null  && _sqlConnection.State == ConnectionState.Closed)
                //{
                //    _sqlConnection.ConnectionString = _connectionString;
                //    _sqlConnection.Credential = cred;
                //}

                //else if(_sqlConnection == null)
                {
                   _sqlConnection = new SqlConnection();
                   _sqlConnection.ConnectionString = _connectionString;
                   _sqlConnection.Credential = cred;
                }

                return _sqlConnection;
            }
        }

        public static void SetConnection(string login, string password)
        {

            _secureString = new SecureString();
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = _dbServerName;
            builder.InitialCatalog = _dbDatabaseName;
            _userID = login;
            builder.MultipleActiveResultSets = true;
            if (password == null)
                password = "";
            foreach (char c in password)
            {
                _secureString.AppendChar(c);
            }

            _secureString.MakeReadOnly();
            _connectionString = builder.ToString();
        }

        public static bool TestConnection(string login, string password)
        {
            bool isOpen = false;
            try
            {
                SetConnection(login, password);
                using (SqlConnection connection = Connection)
                {
                    connection.Open();
                    isOpen= connection.State == ConnectionState.Open;
                    connection.Close();
                }
            }
          
            catch (Exception)
            {
                return false;
            }

            return isOpen;
        }
    }
}
