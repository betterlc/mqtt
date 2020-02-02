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
    public class FollowRuleDaoImpl
    {
        private BGLog logger = BGLog.GetLogger(typeof(FollowRuleDaoImpl));

        public List<FollowRule> GetAllRules()
        {
            List<FollowRule> result = new List<FollowRule>();

            string sql = "SELECT TOPCOAT, NO_FOLLOW_TOPCOAT FROM ZLT_FOLLOW_RULES";

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
                                FollowRule rule = new FollowRule();
                                rule.Color = DBUtil.GetString(reader,0);
                                rule.NoFollowColor = DBUtil.GetString(reader,1);
                                rule.Interval = DBUtil.GetInt(reader,2);

                                result.Add(rule);
                            }
                        }

                        command.Transaction.Commit();

                        logger.Info("Get all follow rule 200");
                    }

                    return result;
                }
            }
            catch (Exception e)
            {
                logger.Error("Get all follow rule failed, {0}", e.ToString());
            }

            return result;
        }

        public List<FollowRule> GetAllRulesByColor(String topcoat)
        {
            List<FollowRule> result = new List<FollowRule>();

            string sql = "SELECT TOPCOAT, NO_FOLLOW_TOPCOAT,INTERVAL_NUM FROM ZLT_FOLLOW_RULES Where TOPCOAT=:TOPCOAT";

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
                                FollowRule rule = new FollowRule();
                                rule.Color = DBUtil.GetString(reader,0);
                                rule.NoFollowColor = DBUtil.GetString(reader,1);
                                rule.Interval = DBUtil.GetInt(reader,2);

                                result.Add(rule);
                            }
                        }

                        command.Transaction.Commit();

                        logger.Info("Get all follow rule 200");
                    }

                    return result;
                }
            }
            catch (Exception e)
            {
                logger.Error("Get all follow rule failed, {0}", e.ToString());
            }

            return result;
        }

    }
}
