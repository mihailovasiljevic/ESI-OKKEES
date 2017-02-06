using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace Connection
{
    public class Connection
    {
        private String server;
        private String database;
        private String uid;
        private String password;
        private MySqlConnection mysqlConn = null;
        private static Connection conn;

        public Connection() { }

        public void Close()
        {
            try
            {
                if (mysqlConn != null)
                {
                    mysqlConn.Close();
                    conn = null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Nije moguce zatvoriti konekciju. {0}", ex.Message);
            }
        }

        public void Open()
        {
            String connectionString = "Server=" + server + ";";
            connectionString += "Database=" + database + ";";
            connectionString += "Uid=" + uid + ";";
            connectionString += "Pwd=" + password + ";";
            try
            {
                mysqlConn = new MySqlConnection(connectionString);
                mysqlConn.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Nije moguce uspostaviti konekciju. {0}", ex.Message);
            }
        }

        public static Connection GetConnection()
        {
            if (conn == null)
            {
                conn = new Connection();
            }
            return conn;
        }

        public void Dispose()
        {
            Close();
        }

        public String Server
        {
            get { return server; }
            set { server = value; }
        }

        public String Database
        {
            get { return database; }
            set { database = value; }
        }

        public String Uid
        {
            get { return uid; }
            set { uid = value; }
        }

        public String Password
        {
            get { return password; }
            set { password = value; }
        }

        public MySqlConnection MysqlConn
        {
            get { return mysqlConn; }
            set { mysqlConn = value; }
        }
    }
}
