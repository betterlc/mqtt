using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using BG.Utilities;
using BG.Log;
using BG.Database;

namespace BG.Mqtt
{
    public class HeartBeatClient:MqttClientService
    {
        private BGLog logger = BGLog.GetLogger(typeof(HeartBeatClient));
        public override void client_func(string msg)
        {
            string strmsg = "[" + msg + "]";
            string strValue = "";
            try
            {
                List<Dictionary<String, Object>> list = new List<Dictionary<String, Object>>();
                list = JsonHelper.ToList<Dictionary<String, Object>>(strmsg);
                HeartBeatDaoImpl dao = new HeartBeatDaoImpl();
                foreach (var dic in list)
                {
                    strValue = dic["curTime"].ToString();
                    dao.Beat(dic);
                }              
                
                logger.Info(strValue);
            }
            catch (Exception ex)
            {
                logger.Error("Mqtt rec function error, {0}", ex.ToString());
            }
        }
    }
}
