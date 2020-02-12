using BG.Contract;
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
    public class TLGResponseDaoImpl
    {
        private BGLog logger = BGLog.GetLogger(typeof(TLGResponseDaoImpl));

        public void Create(TLGResponse response)
        {
            string sql = "INSERT INTO TLG_RESPONSE (ID_APPL, ID_JOB, ID_USER,  HOST_NAME, VON, TLG_NO, ERROR_CODE, ERROR_TEXT, BODY) VALUES  (:ID_APPL, :ID_JOB, :ID_USER, :HOST_NAME, SYSDATE, :TLG_NO, :ERROR_CODE, :ERROR_TEXT,:BODY)";

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

                        DBUtil.SetParam(command, "ID_APPL", DbType.Int32, response.ID_APPL);
                        DBUtil.SetParam(command, "ID_JOB", DbType.Int32, response.ID_JOB);
                        DBUtil.SetParam(command, "ID_USER", DbType.Int32, response.ID_USER);
                        DBUtil.SetParam(command, "HOST_NAME", DbType.String, response.HOST_NAME);
                        DBUtil.SetParam(command, "VON", DbType.DateTime, response.VON);
                        DBUtil.SetParam(command, "TLG_NO", DbType.Int32, response.TLG_NO);
                        DBUtil.SetParam(command, "ERROR_CODE", DbType.Int32, response.ERROR_CODE);
                        DBUtil.SetParam(command, "ERROR_TEXT", DbType.String, response.ERROR_TEXT);
                        DBUtil.SetParam(command, "BODY", DbType.String, response.BODY);

                        command.ExecuteNonQuery();
                        command.Transaction.Commit();
                        logger.Info("create TLGResponse 200, ID_JOB IS {0}", response.ID_JOB);
                    }
                }
            }
            catch (Exception e)
            {
                logger.Error("create TLGResponse failed, {0}", e.ToString());
            }
        }
    }
}
