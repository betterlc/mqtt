using BG.Contract;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG.Database
{
    public class SystemSettingDaoImpl
    {
        public List<int> GetAllMatchRule(String body)
        {
            List<int> result = new List<int>();

            string inString = "SELECT ID FROM MK_INFORM  where txt like '%" + body + "%'";

            using (var connection = new OracleConnection(DBUtil.ConnectionString))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                using (IDbCommand command = connection.CreateCommand())
                {
                    command.Transaction = connection.BeginTransaction();
                    command.CommandText = inString;

                    using (var reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            result.Add(DBUtil.GetInt(reader, 0));
                        }
                    }
                    command.Transaction.Commit();
                }

            }

            return result;
        }

        public void DeleteSetting(int id)
        {
            string inString = "Delete FROM MK_INFORM WHERE ID=:ID";

            using (var connection = new OracleConnection(DBUtil.ConnectionString))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                using (IDbCommand command = connection.CreateCommand())
                {
                    command.Transaction = connection.BeginTransaction();
                    command.CommandText = inString;
                    DBUtil.SetParam(command, "ID", DbType.Int16, id);
                    command.ExecuteNonQuery();
                    command.Transaction.Commit();
                }
            }
        }

        public void CreateAlertSetting(String text)
        {
            try
            {
                string inString = "INSERT INTO MK_INFORM (ID, TXT) VALUES (-5, :TXT)";

                using (var connection = new OracleConnection(DBUtil.ConnectionString))
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }

                    using (IDbCommand command = connection.CreateCommand())
                    {
                        command.Transaction = connection.BeginTransaction();
                        command.CommandText = inString;
                        DBUtil.SetParam(command, "TXT", DbType.String, text);
                        command.ExecuteNonQuery();
                        command.Transaction.Commit();
                    }
                }
            }
            catch (Exception e)
            {
            }
        }

        public Boolean ExistSetting(int id)
        {
            string inString = "SELECT TXT FROM MK_INFORM WHERE ID=:ID";

            using (var connection = new OracleConnection(DBUtil.ConnectionString))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                string text = string.Empty;

                using (IDbCommand command = connection.CreateCommand())
                {
                    command.Transaction = connection.BeginTransaction();
                    command.CommandText = inString;
                    DBUtil.SetParam(command, "ID", DbType.Int16, id);

                    using (var reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            return true;
                        }
                    }
                    command.Transaction.Commit();
                }
                return false;
            }
        }

        public SystemSetting GetSetting()
        {
            string inString = "SELECT TXT FROM MK_INFORM WHERE ID=-1";

            using (var connection = new OracleConnection(DBUtil.ConnectionString))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                string text = string.Empty;

                using (IDbCommand command = connection.CreateCommand())
                {
                    command.Transaction = connection.BeginTransaction();
                    command.CommandText = inString;

                    using (var reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            text = DBUtil.GetString(reader, 0);
                        }
                    }
                    command.Transaction.Commit();
                }

                var result = text.Split('|');

                SystemSetting setting = new SystemSetting();
                setting.TDMax = int.Parse(result[0]);
                setting.TDMin = int.Parse(result[1]);
                setting.TDCurrent = int.Parse(result[2]);
                setting.SectionMax = int.Parse(result[3]);
                setting.EnableManual = bool.Parse(result[4]);
                setting.EnableTimeOut = bool.Parse(result[6]);
                setting.ManualOrder = int.Parse(result[5]);
                setting.TimeOutOrder = int.Parse(result[7]);
                setting.EnableNoFollow = bool.Parse(result[8]);
                setting.EnableMaxPerLine = bool.Parse(result[9]);
                setting.TimeOutInterval = int.Parse(result[10]);

                return setting;
            }
        }
    }
}
