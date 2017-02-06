using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using MySql.Data.Types;
using System.Globalization;
using Common;

namespace Connection
{
    public class DBRepository : IDisposable
    {
        public DBRepository(String server, String database, String uid, String password)
        {
            Connection conn = Connection.GetConnection();
            conn.Database = database;
            conn.Server = server;
            conn.Uid = uid;
            conn.Password = password;

            try
            {
                conn.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception();
            }
            
        }

        public void DeleteEverything(string tableName)
        {
            MySqlCommand cmd;

            string stateName = string.Empty;
            MySqlDataReader reader = null;

            try
            {
                cmd = Connection.GetConnection().MysqlConn.CreateCommand();
                string query = "DELETE FROM " + tableName + " ;";

                cmd = new MySqlCommand(query, Connection.GetConnection().MysqlConn);
                reader = cmd.ExecuteReader();
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            if (!reader.IsClosed)
            {
                reader.Close();
            }
        }

        public void Dispose()
        {
            Connection.GetConnection().Dispose();
        }

        public Connection GetConnection()
        {
            return Connection.GetConnection();
        }

        public void InsertIntoSystemConfTable(int deadband, int state, double pMin, double pMax)
        {
            if (deadband < 0 || pMax < pMin || (state != 0 && state != 1))
            {
                throw new ArgumentException("Ne mozete ovo da uradite");
            }

            MySqlCommand cmd;
            Random rnd = new Random();
            int id = rnd.Next(25000);
            string stateName = string.Empty;
            MySqlDataReader reader = null;

            try
            {
                cmd = Connection.GetConnection().MysqlConn.CreateCommand();
                string query = "INSERT into system_configuration(id, deadband, state, pmin, pmax) VALUES('" + id + "','" + deadband + "','" + state + "','" + pMin + "','" + pMax + "') " +
                               "ON DUPLICATE KEY UPDATE id = '" + id + "', deadband = '" + deadband + "', state = '" + state + "', pMin = '" + pMin + "', pMax = '" + pMax + "';";

                cmd = new MySqlCommand(query, Connection.GetConnection().MysqlConn);
                reader = cmd.ExecuteReader();
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            if (reader != null)
            {
                if (!reader.IsClosed)
                {
                    reader.Close();
                }
            }
        }

        public void InsertIntoDumpingPropTable(Codes code, double vrednost)
        {
            Util.ElementExistsInEnumeration(typeof(Codes), code);

            MySqlCommand cmd;
            string naziv = code.ToString();
            Random rnd = new Random();
            int id = rnd.Next(25000);
            DateTime localDate = DateTime.Now;
            var isoDateTimeFormat = CultureInfo.InvariantCulture.DateTimeFormat;
            string date = localDate.ToString(isoDateTimeFormat.SortableDateTimePattern);
            MySqlDataReader reader = null;
            try
            {
                cmd = Connection.GetConnection().MysqlConn.CreateCommand();
                string query = "INSERT into dumping_property(id, naziv, vrednost, timestamp) VALUES('" + id + "','" + naziv + "','" + vrednost + "','" + date + "') " +
                               "ON DUPLICATE KEY UPDATE id = '" + id + "', naziv = '" + naziv + "', vrednost = '" + vrednost + "', timestamp = '" + date + "';";

                cmd = new MySqlCommand(query, Connection.GetConnection().MysqlConn);
                reader = cmd.ExecuteReader();
                reader.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);   
            }

            if (reader != null)
            {
                if (!reader.IsClosed)
                {
                    reader.Close();
                }
            }    
        }

        public void UpdateSystemConfigurtation(int deadband, double pMin, double pMax)
        {
            MySqlCommand cmd;
            MySqlDataReader dataReader = null;

            try
            {
                cmd = Connection.GetConnection().MysqlConn.CreateCommand();
                string query = "UPDATE system_configuration SET deadband = " + deadband + ", pMin = " + pMin + ", pMax = " + pMax + ";";

                //Create Command
                cmd = new MySqlCommand(query, Connection.GetConnection().MysqlConn);
                //Create a data reader and execute the command
                dataReader = cmd.ExecuteReader();

                dataReader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            if (dataReader != null)
            {
                if (!dataReader.IsClosed)
                {
                    dataReader.Close();
                }
            }
        }

        public Dictionary<string, double> GetAllDataSystemConf()
        {
            MySqlCommand cmd;
            Dictionary<string, double> dict = new Dictionary<string, double>();
            MySqlDataReader dataReader = null;

            try
            {
                cmd = Connection.GetConnection().MysqlConn.CreateCommand();
                string query = "SELECT * FROM system_configuration;";

                //Create Command
                cmd = new MySqlCommand(query, Connection.GetConnection().MysqlConn);
                //Create a data reader and execute the command
                dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    double deadband = Convert.ToDouble(dataReader["deadband"]);
                    double state = Convert.ToDouble(dataReader["state"]);
                    double pMin = Convert.ToDouble(dataReader["pMin"]);
                    double pMax = Convert.ToDouble(dataReader["pMax"]);

                    dict.Add("deadband", deadband);
                    dict.Add("state", state);
                    dict.Add("pMin", pMin);
                    dict.Add("pMax", pMax);
                }
                //close data reader
                dataReader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            if (!dataReader.IsClosed)
            {
                dataReader.Close();
            }

            return dict;
        }

        public Dictionary<string, Dictionary<string, double>> GetAllDataDumpingProp()
        {
            MySqlCommand cmd;
            Dictionary<string, Dictionary<string, double>> dict = new Dictionary<string, Dictionary<string, double>>();
            MySqlDataReader dataReader = null;

            try
            {
                cmd = Connection.GetConnection().MysqlConn.CreateCommand();
                string query = "SELECT * FROM dumping_property;";

                //Create Command
                cmd = new MySqlCommand(query, Connection.GetConnection().MysqlConn);
                //create a data reader and execute the command
                dataReader = cmd.ExecuteReader();

                //read the data and store them in the list
                while (dataReader.Read())
                {
                    Dictionary<string, double> kvp = new Dictionary<string, double>();
                    string dataTime = dataReader["timestamp"] + "";
                    string naziv = dataReader["naziv"] + "";
                    double vrednost = Convert.ToDouble(dataReader["vrednost"]);
                    kvp.Add(naziv, vrednost);

                    dict.Add(dataTime, kvp);
                }
                //close data reader
                dataReader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            if (!dataReader.IsClosed)
            {
                dataReader.Close();
            }

            return dict;
        }

        public Dictionary<string, Dictionary<string, double>> GetDataForBufferStatistics(DateTime startDate, DateTime endDate)
        {
            MySqlCommand cmd;
            //Dictionary<string, double> dict = new Dictionary<string, double>();
            Dictionary<string, Dictionary<string, double>> dict = new Dictionary<string, Dictionary<string, double>>();
            var isoDateTimeFormat = CultureInfo.InvariantCulture.DateTimeFormat;
            string date1 = startDate.ToString(isoDateTimeFormat.SortableDateTimePattern);
            string date2 = endDate.ToString(isoDateTimeFormat.SortableDateTimePattern);
            MySqlDataReader dataReader = null;

            try
            {
                cmd = Connection.GetConnection().MysqlConn.CreateCommand();
                string query = "SELECT * FROM dumping_property WHERE timestamp between '" + date1 + "'AND '" + date2 + "' ;";

                //create command
                cmd = new MySqlCommand(query, Connection.GetConnection().MysqlConn);
                //create a data reader and execute the command
                dataReader = cmd.ExecuteReader();

                //read the data and store them in the list
                while (dataReader.Read())
                {
                    Dictionary<string, double> kvp = new Dictionary<string, double>();
                    string dataTime = dataReader["timestamp"] + "";
                    string naziv = dataReader["naziv"] + "";
                    double vrednost = Convert.ToDouble(dataReader["vrednost"]);

                    kvp.Add(naziv, vrednost);
                    dict.Add(dataTime, kvp);
                }
                //close data reader
                dataReader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            if (!dataReader.IsClosed)
            {
                dataReader.Close();
            }

            return dict;
        }

        public bool CheckIfCodeExistDumpProp(Codes codeEnum)
        {
            Util.ElementExistsInEnumeration(typeof(Codes), codeEnum);

            MySqlCommand cmd;
            string code = codeEnum.ToString();
            MySqlDataReader dataReader = null;

            try
            {
                cmd = Connection.GetConnection().MysqlConn.CreateCommand();
                string query = "SELECT * FROM dumping_property WHERE naziv='" + code + "';";

                //create command
                cmd = new MySqlCommand(query, Connection.GetConnection().MysqlConn);
                //create a data reader and execute command
                dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    string naziv = dataReader["naziv"] + "";
                    if (code.Equals(naziv))
                    {
                        dataReader.Close();
                        return true;
                    }
                }

                dataReader.Close();
                return false;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            if (!dataReader.IsClosed)
            {
                dataReader.Close();
            }
            return false;
        }

        //metoda koja vraca par<code, vrednost> sa najsvezijim datumom
        public KeyValuePair<Codes, double> GetFreshDumpValue(Codes codeEnum)
        {
            Util.ElementExistsInEnumeration(typeof(Codes), codeEnum);

            MySqlCommand cmd;
            string code = codeEnum.ToString();
            MySqlDataReader dataReader = null;

            try
            {
                cmd = Connection.GetConnection().MysqlConn.CreateCommand();
                string query = "SELECT * FROM dumping_property WHERE naziv = '" + code + "' and timestamp IN(SELECT max(timestamp) FROM dumping_property WHERE naziv = '" + code + "')" + ";";

                //create command
                cmd = new MySqlCommand(query, Connection.GetConnection().MysqlConn);
                //create a data reader and execute command
                dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    string naziv = dataReader["naziv"] + "";
                    double vrednost = Convert.ToDouble(dataReader["vrednost"]);
                    dataReader.Close();
                    return new KeyValuePair<Codes, double>(codeEnum, vrednost);
                }
                //close data reader
                dataReader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            if (!dataReader.IsClosed)
            {
                dataReader.Close();
            }

            return new KeyValuePair<Codes, double>(Codes.ANALOG, -99999);
        }

        public void ChangeServiceState(int state)
        {
            MySqlCommand cmd;
            MySqlDataReader dataReader = null;

            try
            {
                cmd = Connection.GetConnection().MysqlConn.CreateCommand();
                string query = "UPDATE system_configuration SET state = " + state + ";";

                //Create Command
                cmd = new MySqlCommand(query, Connection.GetConnection().MysqlConn);
                //Create a data reader and execute the command
                dataReader = cmd.ExecuteReader();

                dataReader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            if (!dataReader.IsClosed)
            {
                dataReader.Close();
            }
        }
    }
}
