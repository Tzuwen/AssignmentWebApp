using System.Configuration;
using System.Data.SqlClient;

namespace AssignmentWebApp.Dao
{
    public class SqlServerDatabase : Database
    {
        private System.Data.Common.DbConnection _connection = null;
        private System.Data.Common.DbCommand _command = null;

        public override System.Data.Common.DbConnection Connection
        {
            get
            {
                if (_connection == null)
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["AdventureConnection"].ConnectionString;
                    _connection = new SqlConnection(connectionString);
                }
                return _connection;
            }
            set
            {
                _connection = value;
            }
        }

        public override System.Data.Common.DbCommand Command
        {
            get
            {
                if (_command == null)
                {
                    _command = new SqlCommand();
                    _command.Connection = Connection;
                }
                return _command;
            }
            set
            {
                _command = value;
            }
        }

    }
}