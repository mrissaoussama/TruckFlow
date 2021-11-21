using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace DAO.DBConnection
{
    public class DBConnect
    {
        private static DBConnect DBInstance;
        public MySqlConnection Connection;
        private DBConnect()
        {

        }
        public static DBConnect GetDBInstance(String ConnectionString)
        {
            if (DBInstance==null)
            {
                try
                {
                    DBInstance = new DBConnect();
                    DBInstance.Connection = new MySqlConnection(ConnectionString);
                    DBInstance.Connection.Open();
                }
                catch(MySqlException expp)
                {

                }
            }
            return DBInstance;

        }
    }
}
