using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BG.Log;
using BG.Database;
using BG.Contract;
using Newtonsoft.Json;

namespace BG.Mqtt
{
    public class DownloadParamClient : MqttClientService
    {
        private BGLog logger = BGLog.GetLogger(typeof(HeartBeatClient));
        public override void MqttMsgPublish(string topic, string msg, byte level)
        {
            try
            {

                ModifyTableDaoImpl dao = new ModifyTableDaoImpl();
                List<ModifyTableName> tableList = dao.Query(0);
                DataRecorderSummaryVO drSummary = new DataRecorderSummaryVO();
                List<DeviceSummaryVO> deviceList = new List<DeviceSummaryVO>();
                List<ChannelSummaryVO> channelList = new List<ChannelSummaryVO>();
                SensorSummaryVO sensorVO = (SensorSummaryVO)null;
                string dr_id="0";
                if (tableList != null)
                {
                    foreach (ModifyTableName table in tableList)//必修现找到dr_id
                    {
                        if (table.Modify_Table == "tb_data_recorder_modify")
                        {
                            ModifyDataRecorder modifyDr = new ModifyDataRecorder();
                            drSummary = modifyDr.GetConfig(table.Modify_ID);
                            dr_id = table.dr_id;
                        }                        
                    }
                    foreach (ModifyTableName table in tableList)
                    {
                        if (table.Modify_Table == "tb_device_modify" && table.dr_id == dr_id)
                        {
                            ModifyDevice modify = new ModifyDevice();
                            deviceList.Add(modify.SelectConfig(table.Modify_ID));
                        }
                        if (table.Modify_Table == "tb_channel_modify")
                        {
                            ModifyChannel modify = new ModifyChannel();
                            channelList.Add(modify.SelectConfig(table.Modify_ID));
                        }
                        if (table.Modify_Table == "tb_sensor_modify")
                        {
                            ModifySensor modify = new ModifySensor();
                            sensorVO = modify.SelectConfig(table.Modify_ID);
                        }
                    }
                }
                //deviceList.
                //drSummary.deviceList = deviceList;
                //msg = JsonConvert.SerializeObject((object)drSummary);//得用12.0版本以上
                //client.Publish(topic, Encoding.UTF8.GetBytes(msg), level, false);
            }
            catch (Exception ex)
            {
                logger.Error("Mqtt publish error, {0}", ex.ToString());
            }
        }
    }
}
