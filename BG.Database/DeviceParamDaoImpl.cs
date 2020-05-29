using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;
using BG.Log;

namespace BG.Database
{
    public class DeviceParamDaoImpl
    {
        private BGLog logger = BGLog.GetLogger(typeof(DeviceParamDaoImpl));
        public int SelectParam(string strsno)
        {
            string sql = "select count(*) from tb_device where sno=?sno";
            try
            {
                //using (var connection = new MySqlConnection(DBUtil.ConnectionString))
                using (var connection = new MySqlConnection("server=localhost;user id=root;password=root;database=railway"))
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }

                    using (IDbCommand command = connection.CreateCommand())
                    {
                        command.Transaction = connection.BeginTransaction();
                        command.CommandText = sql;

                        DBUtil.SetParam(command, "sno", DbType.String, strsno);

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
                logger.Error("select tb_device failed, {0}", e.ToString());                
            }
            return 0;
        }
        public bool InsertParam(string sno,string model,string ratetype,string ip,string runstatus,string refreshmillis,string refreshtime,string runtime)
        {
            string sql = @"insert into tb_device(sno,model,sample_ratetype,ip,run_status,last_refreshmillis,last_refreshtime,runtime) 
                           values(?sno,?model,?sample_ratetype,?ip,?run_status,?last_refreshmillis,?last_refreshtime,?runtime)";

            try
            {
                //using (var connection = new MySqlConnection(DBUtil.ConnectionString))
                using (var connection = new MySqlConnection("server=localhost;user id=root;password=root;database=railway"))
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }

                    using (IDbCommand command = connection.CreateCommand())
                    {
                        command.Transaction = connection.BeginTransaction();
                        command.CommandText = sql;

                        DBUtil.SetParam(command, "sno", DbType.String, sno);
                        DBUtil.SetParam(command, "model", DbType.String, model);
                        DBUtil.SetParam(command, "sample_ratetype", DbType.String, ratetype);
                        DBUtil.SetParam(command, "ip", DbType.String, ip);
                        DBUtil.SetParam(command, "run_status", DbType.String, runstatus);
                        DBUtil.SetParam(command, "last_refreshmillis", DbType.String, refreshmillis);
                        DBUtil.SetParam(command, "last_refreshtime", DbType.String, refreshtime);
                        DBUtil.SetParam(command, "runtime", DbType.String, runtime);

                        command.ExecuteNonQuery();
                        command.Transaction.Commit();
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                logger.Error("insert tb_device failed, {0}", e.ToString());
            }
            return false;
        }

        public bool UpdateParam(string sno, string model, string ratetype, string ip, string runstatus, string refreshmillis, string refreshtime, string runtime)
        {
            string sql = @"update tb_device set 
                            model=?model,
                            sample_ratetype=?sample_ratetype,
                            ip=?ip,
                            run_status=?run_status,
                            last_refreshmillis=?last_refreshmillis,
                            last_refreshtime=?last_refreshtime,
                            runtime=?runtime 
                           where sno=?sno";

            try
            {
                //using (var connection = new MySqlConnection(DBUtil.ConnectionString))
                using (var connection = new MySqlConnection("server=localhost;user id=root;password=root;database=railway"))
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }

                    using (IDbCommand command = connection.CreateCommand())
                    {
                        command.Transaction = connection.BeginTransaction();
                        command.CommandText = sql;

                        DBUtil.SetParam(command, "sno", DbType.String, sno);
                        DBUtil.SetParam(command, "model", DbType.String, model);
                        DBUtil.SetParam(command, "sample_ratetype", DbType.String, ratetype);
                        DBUtil.SetParam(command, "ip", DbType.String, ip);
                        DBUtil.SetParam(command, "run_status", DbType.String, runstatus);
                        DBUtil.SetParam(command, "last_refreshmillis", DbType.String, refreshmillis);
                        DBUtil.SetParam(command, "last_refreshtime", DbType.String, refreshtime);
                        DBUtil.SetParam(command, "runtime", DbType.String, runtime);

                        command.ExecuteNonQuery();
                        command.Transaction.Commit();
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                logger.Error("update tb_device failed, {0}", e.ToString());
            }
            return false;
        }
    }
}
