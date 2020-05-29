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
    //实时表、base表和历史表
    public class DeviceDaoImpl
    {
        private BGLog logger = BGLog.GetLogger(typeof(DeviceDaoImpl));
        string sqlSelect_device = "select count(*) from tb_device where sno=?sno";        
        string sqlInsert_device = @"insert into tb_device(sno,model,sample_ratetype,ip,create_time) values(?sno,?model,?sample_ratetype,?ip,?create_time)";
        string sqlInsert_deviceBase = @"insert into tb_device_base(name,sno,channel_num,drID,create_time) values(?name,?sno,?channel_num,?drID,?create_time)";

        public bool Implement(long id, string msg)
        {
            string drID = id.ToString();
            try
            {
                DataRecorderSummaryVO drConfigSummaryVO = new DataRecorderSummaryVO();
                drConfigSummaryVO = JsonConvert.DeserializeObject<DataRecorderSummaryVO>(msg);
                List<DeviceSummaryVO> deviceSummaryList = drConfigSummaryVO.deviceList;

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
                        command.CommandText = sqlSelect_device;
                        foreach (DeviceSummaryVO deviceScan in deviceSummaryList)
                        {
                            DBUtil.SetParam(command, "sno", DbType.String, deviceScan.sno);
                            var reader = command.ExecuteReader();
                            if (reader.Read())
                            {
                                if (reader.GetInt16(0) == 0)
                                {
                                    reader.Close();

                                    command.CommandText = sqlInsert_deviceBase;//基本表
                                    command.Parameters.Clear();
                                    DBUtil.SetParam(command, "name", DbType.String, deviceScan.name);
                                    DBUtil.SetParam(command, "sno", DbType.String, deviceScan.sno);
                                    DBUtil.SetParam(command, "channel_num", DbType.Int16, deviceScan.channelList.Count);
                                    DBUtil.SetParam(command, "drID", DbType.String, drID);
                                    DBUtil.SetParam(command, "create_time", DbType.String, DateTime.Now.ToString());
                                    command.ExecuteNonQuery();

                                    command.CommandText = sqlInsert_device;//实时表
                                    command.Parameters.Clear();
                                    DBUtil.SetParam(command, "sno", DbType.String, deviceScan.sno);
                                    DBUtil.SetParam(command, "model", DbType.String, deviceScan.model); 
                                    DBUtil.SetParam(command, "sample_ratetype", DbType.String, deviceScan.sampleRateType);
                                    DBUtil.SetParam(command, "ip", DbType.String, deviceScan.ip);
                                    DBUtil.SetParam(command, "create_time", DbType.String, DateTime.Now.ToString());
                                    command.ExecuteNonQuery();
                                }
                                else
                                    reader.Close();
                            }
                        }
                        command.Transaction.Commit();
                    }
                    return true;
                }
            }
            catch (Exception e)
            {
                logger.Error("device dao implement failed, {0}", e.ToString());
            }
            return false;
        }
    }
}
