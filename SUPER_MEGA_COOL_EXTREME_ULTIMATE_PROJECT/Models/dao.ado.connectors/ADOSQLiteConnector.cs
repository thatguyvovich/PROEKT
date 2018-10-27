using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace CyberPushkin.dao.ado.connectors
{
    public class ADOSQLiteConnector
    {

        private readonly String dbName;

  public ADOSQLiteConnector(String dbName)
        {
            if (dbName == null || dbName.Equals(""))
            {
                throw new ArgumentException();
            }

            this.dbName = dbName;
        }
        public SqlConnection getConnection()
        {
            SqlConnection connection = new SqlConnection(dbName);
            try
            {
                // Открываем подключение
                connection.Open();
                Console.WriteLine("Подключение открыто");
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }

            return connection;
        }
    }
}
