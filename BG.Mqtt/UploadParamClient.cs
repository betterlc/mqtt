using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BG.Log;
using BG.Database;

namespace BG.Mqtt
{
    public class UploadParamClient : MqttClientService
    {
        private BGLog logger = BGLog.GetLogger(typeof(HeartBeatClient));
        public override void client_func(string msg)
        {
            string strmsg = msg;
            try
            {
                
                DataRecorderDaoImpl dao1 = new DataRecorderDaoImpl();
                long drID = dao1.Implement(strmsg);
                DeviceDaoImpl dao2 = new DeviceDaoImpl();
                dao2.Implement(drID,strmsg);
            }
            catch (Exception ex)
            {
                logger.Error("Mqtt rec function error, {0}", ex.ToString());
            }
        }
    }
}
