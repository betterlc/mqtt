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
    public class HeartBeatDaoImpl
    {
        private BGLog logger = BGLog.GetLogger(typeof(HeartBeatDaoImpl));

        public void Beat(int PLCPosition)
        {
            string sql = "update STA_HEARTBIT set VON=SYSDATE where WHAT_ID=:WHAT_ID";

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

                        DBUtil.SetParam(command, "WHAT_ID", DbType.Int32, PLCPosition);

                        command.ExecuteNonQuery();
                        command.Transaction.Commit();
                    }
                }
            }
            catch (Exception e)
            {
                logger.Error("Beat update date time failed, {0}", e.ToString());
            }
        }
    }
}
