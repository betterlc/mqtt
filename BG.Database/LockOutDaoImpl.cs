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
    public class LockOutDaoImpl
    {
        private BGLog logger = BGLog.GetLogger(typeof(LockOutDaoImpl));

        public void UpdateLastOutTime(String Id, int Section)
        {
            String LASTOUTTIME = "LASTOUTTIME";

            if (Section == 2)
            {
                LASTOUTTIME = "LASTOUTTIME2";
            }

            String sql = "UPDATE SSC_OUTCARRULE SET " + LASTOUTTIME + " = :LASTOUTTIME WHERE ID=:ID";

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
                        DBUtil.SetParam(command, "LASTOUTTIME", DbType.DateTime, DateTime.Now);
                        DBUtil.SetParam(command, "ID", DbType.String, Id);

                        command.ExecuteNonQuery();
                        command.Transaction.Commit();
                    }
                }
            }
            catch (Exception e)
            {
                logger.Error("update all lock rule failed, {0}", e.ToString());
            }
        }

        public void UpdateClearState(String Id, int Section, int state)
        {
            String CLEARSTATE = "CLEARSTATE";

            if (Section == 2)
            {
                CLEARSTATE = "CLEARSTATE2";
            }

            String sql = "UPDATE SSC_OUTCARRULE SET " + CLEARSTATE + " = :CLEARSTATE WHERE ID=:ID";

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
                        DBUtil.SetParam(command, "CLEARSTATE", DbType.Int32, state);
                        DBUtil.SetParam(command, "ID", DbType.String, Id);

                        command.ExecuteNonQuery();
                        command.Transaction.Commit();
                    }
                }
            }
            catch (Exception e)
            {
                logger.Error("update all lock rule failed, {0}", e.ToString());
            }
        }

        public List<OutLock> GetAllRules(String topcoat, String bodyCode)
        {
            List<OutLock> result = new List<OutLock>();

            string sql = "SELECT ID,  BODYCODE ,TOPCOAT, OUTINTERVAL,LASTOUTTIME,LASTOUTTIME2, CLEARSTATE, CLEARSTATE2 FROM SSC_OUTCARRULE  Where TOPCOAT=:TOPCOAT AND BODYCODE=:BODYCODE";

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
                        DBUtil.SetParam(command, "TOPCOAT", DbType.String, topcoat);
                        DBUtil.SetParam(command, "BODYCODE", DbType.String, bodyCode);

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                OutLock rule = new OutLock();
                                rule.Id = DBUtil.GetString(reader, 0);
                                rule.BodyCode = DBUtil.GetString(reader, 1);
                                rule.Topcoat = DBUtil.GetString(reader, 2);
                                rule.Interval = DBUtil.GetInt(reader, 3);
                                rule.LastOutTime = DBUtil.GetDateTime(reader, 4);
                                rule.LastOutTime2 = DBUtil.GetDateTime(reader, 5);
                                rule.ClearState = DBUtil.GetInt(reader, 6);
                                rule.ClearState2 = DBUtil.GetInt(reader, 7);

                                result.Add(rule);
                            }
                        }

                        command.Transaction.Commit();

                        logger.Info("Get all lock rule 200");
                    }

                    return result;
                }
            }
            catch (Exception e)
            {
                logger.Error("Get all lock rule failed, {0}", e.ToString());
            }

            return result;
        }

        public List<OutLock> GetAllRules()
        {
            List<OutLock> result = new List<OutLock>();

            string sql = "SELECT ID,  BODYCODE ,TOPCOAT, OUTINTERVAL,LASTOUTTIME,LASTOUTTIME2, CLEARSTATE, CLEARSTATE2 FROM SSC_OUTCARRULE";

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

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                OutLock rule = new OutLock();
                                rule.Id = DBUtil.GetString(reader, 0);
                                rule.BodyCode = DBUtil.GetString(reader, 1);
                                rule.Topcoat = DBUtil.GetString(reader, 2);
                                rule.Interval = DBUtil.GetInt(reader, 3);
                                rule.LastOutTime = DBUtil.GetDateTime(reader, 4);
                                rule.LastOutTime2 = DBUtil.GetDateTime(reader, 5);
                                rule.ClearState = DBUtil.GetInt(reader, 6);
                                rule.ClearState2 = DBUtil.GetInt(reader, 7);

                                result.Add(rule);
                            }
                        }

                        command.Transaction.Commit();

                        logger.Info("Get all lock rule 200");
                    }

                    return result;
                }
            }
            catch (Exception e)
            {
                logger.Error("Get all lock rule failed, {0}", e.ToString());
            }

            return result;
        }
    }
}
