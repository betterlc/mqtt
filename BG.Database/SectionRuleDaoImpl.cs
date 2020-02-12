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
    public class SectionRuleDaoImpl
    {
        private BGLog logger = BGLog.GetLogger(typeof(SectionRuleDaoImpl));

        public SectionRule GetRule(String Topcoat)
        {
            logger.Info("get rule for {0}", Topcoat);

            string sql = "SELECT TOPCOAT, S , P FROM SSC_RULE WHERE TOPCOAT=:TOPCOAT";

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

                        using (var reader = command.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                SectionRule rule = new SectionRule();
                                rule.Topcoat = DBUtil.GetString(reader,0);
                                rule.Section = DBUtil.GetInt(reader,1);
                                rule.Property = DBUtil.GetInt(reader,2);

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
