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
    public class OutBalanceRuleDaoImpl
    {
        private BGLog logger = BGLog.GetLogger(typeof(OutBalanceRuleDaoImpl));

        public OutBalanceRule GetRule(String topcoat, String body)
        {
            logger.Info("get rule for {0}, {1}", topcoat, body);

            string sql1 = "SELECT TOPCOAT, BODYCODE, CNT FROM MK_BALANCE_RULE WHERE TOPCOAT=:TOPCOAT and BODYCODE=:BODYCODE";

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
                        command.CommandText = sql1;

                        DBUtil.SetParam(command, "TOPCOAT", DbType.String, topcoat);
                        DBUtil.SetParam(command, "BODYCODE", DbType.String, body);

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                OutBalanceRule rule = new OutBalanceRule();
                                rule.Topcoat = DBUtil.GetString(reader,0);
                                rule.BodyCode = DBUtil.GetString(reader,1);
                                rule.LimitCount = DBUtil.GetInt(reader,2);

                                return rule;
                            }
                        }

                        command.Transaction.Commit();
                    }
                }
            }
            catch (Exception e)
            {
                logger.Error("Get rule failed, {0}", e.ToString());
            }

            string sql = "SELECT TOPCOAT, BODYCODE, CNT FROM MK_BALANCE_RULE WHERE TOPCOAT=:TOPCOAT";

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

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                OutBalanceRule rule = new OutBalanceRule();
                                rule.Topcoat = DBUtil.GetString(reader,0);
                                rule.BodyCode = DBUtil.GetString(reader,1);
                                rule.LimitCount = DBUtil.GetInt(reader,2);

                                return rule;
                            }
                        }

                        command.Transaction.Commit();
                    }
                }
            }
            catch (Exception e)
            {
                logger.Error("Get rule failed, {0}", e.ToString());
            }



            return null;
        }
    }
}
