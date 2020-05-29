using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;
using BG.Log;
using Newtonsoft.Json;
using BG.Contract;

namespace BG.Database
{
    /*
     * 1.按照dno查询tb_data_recorder，没有记录，则写入，有则更新
     */
    public class DataRecorderDaoImpl
    {
        private BGLog logger = BGLog.GetLogger(typeof(DeviceDaoImpl));
        string sqlSelect_dataR = "select count(*) from tb_data_recorder where dno=?dno";
        string sqlInsert_dataR = @"insert into tb_data_recorder(dno,config_version,datatime,gps_latitude,gps_longitude,gps_altitude,gps_speed,create_time) 
                           values(?dno,?config_version,?datatime,?gps_latitude,?gps_longitude,?gps_altitude,?gps_speed,?create_time)";        
        string sqlInsert_dataRBase = @"insert into tb_data_recorder_base(dno,name,create_time) values(?dno,?name,?create_time)";

        public long Implement(string msg)
        {
            long id = 0;
            try
            {
                DataRecorderSummaryVO drConfigSummaryVO = new DataRecorderSummaryVO();
                drConfigSummaryVO = JsonConvert.DeserializeObject<DataRecorderSummaryVO>(msg);
                
                //using (var connection = new MySqlConnection(DBUtil.ConnectionString))
                using (var connection = new MySqlConnection("server=localhost;user id=root;password=root;database=railway"))
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }

                    //using (IDbCommand command = connection.CreateCommand())
                    using (MySqlCommand command = connection.CreateCommand())
                    {
                        command.Transaction = connection.BeginTransaction();

                        command.CommandText = sqlSelect_dataR;
                        DBUtil.SetParam(command, "dno", DbType.String, drConfigSummaryVO.dno);
                        var reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            if (reader.GetInt16(0) == 0)
                            {
                                reader.Close(); 

                                command.CommandText = sqlInsert_dataRBase;//基本表
                                command.Parameters.Clear();
                                DBUtil.SetParam(command, "dno", DbType.String, drConfigSummaryVO.dno);
                                DBUtil.SetParam(command, "name", DbType.String, "数据记录仪");
                                DBUtil.SetParam(command, "model", DbType.String, "CX22");
                                DBUtil.SetParam(command, "create_time", DbType.String, DateTime.Now.ToString());
                                command.ExecuteNonQuery();

                                id = command.LastInsertedId;
                                command.CommandText = sqlInsert_dataR;//实时表
                                command.Parameters.Clear();
                                DBUtil.SetParam(command, "dno", DbType.String, drConfigSummaryVO.dno);
                                DBUtil.SetParam(command, "config_version", DbType.String, drConfigSummaryVO.configVersion);
                                DBUtil.SetParam(command, "datatime", DbType.String, drConfigSummaryVO.datatime);
                                DBUtil.SetParam(command, "gps_latitude", DbType.String, drConfigSummaryVO.gps_Latitude);
                                DBUtil.SetParam(command, "gps_longitude", DbType.String, drConfigSummaryVO.gps_Longitude);
                                DBUtil.SetParam(command, "gps_altitude", DbType.String, drConfigSummaryVO.gps_Altitude);
                                DBUtil.SetParam(command, "gps_speed", DbType.String, drConfigSummaryVO.gps_Speed);
                                DBUtil.SetParam(command, "create_time", DbType.String, DateTime.Now.ToString());
                                command.ExecuteNonQuery();
                            }
                            else
                                reader.Close();
                        }
                        command.Transaction.Commit();
                    }
                }
            }
            catch (Exception e)
            {
                logger.Error("device dao implement failed, {0}", e.ToString());
            }
            return id;
        }
    }
}
