using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class DatabaseConnection
    {
        MySqlConnection dbConnection;
        string connectionstring;
        public DatabaseConnection()
        {this.connectionstring = ConfigurationManager.ConnectionStrings["truckflowdb"].ConnectionString;
            this.dbConnection = new(connectionstring);
        }
        public MySqlConnection getConnection()
        { return dbConnection; }
    }
}
