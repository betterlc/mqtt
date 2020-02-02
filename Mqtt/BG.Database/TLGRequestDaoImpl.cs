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
    public class TLGRequestDaoImpl
    {
        private BGLog logger = BGLog.GetLogger(typeof(TLGRequestDaoImpl));

        public void DeleteRequest(int JobId)
        {
            string sql = "Delete from TLG_REQUEST where ID_JOB=:ID_JOB";
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

                        DBUtil.SetParam(command, "ID_JOB", DbType.Int32, JobId);

                        command.ExecuteNonQuery();

                        command.Transaction.Commit();

                        logger.Info("delete TLGRequest 200, jobId is {0}", JobId);
                    }
                }
            }
            catch (Exception e)
            {
                logger.Error("delete TLGRequest failed, {0}", e.ToString());
            }
        }

        public List<TLGRequest> GetAllRequest(int section)
        {
            List<TLGRequest> result = new List<TLGRequest>();

            string sql = "Select ID_APPL, ID_JOB, ID_USER, HOST_NAME, VON, TLG_NO, BODY from TLG_REQUEST WHERE ID_APPL=:ID_APPL";

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
                        DBUtil.SetParam(command, "ID_APPL", DbType.Int32, section);

                        using (var reader = command.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                TLGRequest request = new TLGRequest();
                                request.ID_APPL = reader.GetInt16(0);
                                request.ID_JOB = reader.GetInt16(1);
                                request.ID_USER = reader.GetInt16(2);
                                request.HOST_NAME = DBUtil.GetString(reader,3);
                                request.BODY = DBUtil.GetString(reader,6);
                                request.TLG_NO = reader.GetInt16(5);
                                request.VON = reader.GetDateTime(4);

                                result.Add(request);
                            }
                        }

                        command.Transaction.Commit();

                      //  logger.Info("Get all TLGRequest 200");
                    }

                    return result;
                }
            }
            catch (Exception e)
            {
                logger.Error("Get all TLGRequest failed, {0}", e.ToString());
            }

            return result;
        }

        public void Create(TLGRequest request)
        {
            string sql = "INSERT INTO TLG_REQUEST (ID_APPL, ID_JOB, ID_USER,  HOST_NAME, VON, TLG_NO,  BODY) VALUES  (:ID_APPL, :ID_JOB, :ID_USER, :HOST_NAME, SYSDATE, :TLG_NO, :BODY)";

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

                        DBUtil.SetParam(command, "ID_APPL", DbType.Int32, request.ID_APPL);
                        DBUtil.SetParam(command, "ID_JOB", DbType.Int32, request.ID_JOB);
                        DBUtil.SetParam(command, "ID_USER", DbType.Int32, request.ID_USER);
                        DBUtil.SetParam(command, "HOST_NAME", DbType.String, request.HOST_NAME);
                        //DBUtil.SetParam(command, "VON", DbType.DateTime, request.VON);
                        DBUtil.SetParam(command, "TLG_NO", DbType.Int32, request.TLG_NO);
                        DBUtil.SetParam(command, "BODY", DbType.String, request.BODY);

                        command.ExecuteNonQuery();
                        command.Transaction.Commit();
                        logger.Info("create TLGRequest 200, ID_JOB IS {0}", request.ID_JOB);
                    }
                }
            }
            catch (Exception e)
            {
                logger.Error("create TLGRequest failed, {0}", e.ToString());
            }
        }
    }
}
