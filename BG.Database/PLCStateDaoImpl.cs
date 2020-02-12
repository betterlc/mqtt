using BG.Log;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG.Database
{
    /// <summary>
    /// SSC_STATE
    /// </summary>
    public class PLCStateDaoImpl
    {
        private BGLog logger = BGLog.GetLogger(typeof(HeartBeatDaoImpl));

        public void UpdateState(int section, int state)
        {
            string sql = "update SSC_STATE set EMG_MODE=:EMG_MODE where SECTION=:SECTION";

            try
            {
                using (var connection = new OracleConnection(DBUtil.ConnectionString))
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }

                    using (IDbCommand command = connection.CreateCommand())
                    {
                        command.Transaction = connection.BeginTransaction();
                        command.CommandText = sql;

                        DBUtil.SetParam(command, "EMG_MODE", DbType.Int32, state);
                        DBUtil.SetParam(command, "SECTION", DbType.Int32, section);

                        command.ExecuteNonQuery();
                        command.Transaction.Commit();
                    }
                }
            }
            catch (Exception e)
            {
                logger.Error("update PLC STATE failed, {0}", e.ToString());
            }
        }


        public int GetState(int section)
        {
            string sql = "Select EMG_MODE FROM SSC_STATE where SECTION=:SECTION";

            try
            {
                using (var connection = new OracleConnection(DBUtil.ConnectionString))
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }

                    using (IDbCommand command = connection.CreateCommand())
                    {
                        command.Transaction = connection.BeginTransaction();
                        command.CommandText = sql;

                        DBUtil.SetParam(command, "SECTION", DbType.Int32, section);

                        using (var reader = command.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                return reader.GetInt16(0);
                            }
                        }

                        command.Transaction.Commit();
                    }
                }
            }
            catch (Exception e)
            {
                logger.Error("update PLC STATE failed, {0}", e.ToString());
            }

            return 0;
        }
    }
}
