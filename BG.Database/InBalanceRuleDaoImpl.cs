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
    public class InBalanceRuleDaoImpl
    {
        private BGLog logger = BGLog.GetLogger(typeof(SectionRuleDaoImpl));

        public void UpdateACTValueAndSection(String topcoat, string bodyCode, int ACTValue, int section)
        {
            String sql = "UPDATE MK_RULE_BALANCE SET S=:S,ACTVALUE=:ACTVALUE WHERE TOPCOAT=:TOPCOAT AND BODYCODE=:BODYCODE";

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

                        DBUtil.SetParam(command, "S", DbType.Int16, section);
                        DBUtil.SetParam(command, "ACTVALUE", DbType.Int32, ACTValue);
                        DBUtil.SetParam(command, "TOPCOAT", DbType.String, topcoat);
                        DBUtil.SetParam(command, "BODYCODE", DbType.String, bodyCode);

                        command.ExecuteNonQuery();
                        command.Transaction.Commit();
                    }
                }
            }
            catch (Exception e)
            {
                logger.Error("Update rule failed, {0}", e.ToString());
            }
        }

        public void UpdateRuleSection(String topcoat, String bodyCode, int s)
        {
            String sql = "UPDATE MK_RULE_BALANCE SET S = :S WHERE TOPCOAT = :TOPCOAT AND BODYCODE=:BODYCODE";

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

                        DBUtil.SetParam(command, "S", DbType.Int32, s);
                        DBUtil.SetParam(command, "TOPCOAT", DbType.String, topcoat);
                        DBUtil.SetParam(command, "BODYCODE", DbType.String, bodyCode);

                        command.ExecuteNonQuery();
                        command.Transaction.Commit();
                    }
                }
            }
            catch (Exception e)
            {
                logger.Error("Update rule section failed, {0}", e.ToString());
            }
        }

        public InBalanceRule GetRule(String Topcoat, String bodyCode)
        {
            logger.Info("get rule for {0}", Topcoat);

            string sql = "SELECT TOPCOAT,BODYCODE, SETVALUE, ACTVALUE,S , ROLE FROM MK_RULE_BALANCE WHERE TOPCOAT=:TOPCOAT AND BODYCODE=:BODYCODE";

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

                        DBUtil.SetParam(command, "TOPCOAT", DbType.String, Topcoat);
                        DBUtil.SetParam(command, "BODYCODE", DbType.String, bodyCode);

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                InBalanceRule rule = new InBalanceRule();
                                rule.Topcoat = DBUtil.GetString(reader, 0);
                                rule.Bodycode = DBUtil.GetString(reader, 1);
                                rule.SetValue = DBUtil.GetInt(reader, 2);
                                rule.ActValue = DBUtil.GetInt(reader, 3);
                                rule.Section = DBUtil.GetInt(reader, 4);
                                rule.Role = DBUtil.GetString(reader, 5);

                                return rule;
                            }
                        }

                        command.Transaction.Commit();

                        logger.Info("Get rule 200");
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
