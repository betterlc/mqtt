using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BG.Utilities;
using BG.Log;
using BG.Database;

namespace BG.Mqtt
{
    public class CalculateClient : MqttClientService
    {
        private BGLog logger = BGLog.GetLogger(typeof(HeartBeatClient));
        public override void client_func(string msg)
        {
            string strmsg = msg;
            try
            {
                CalculateResultDaoImpl dao = new CalculateResultDaoImpl();
                dao.InsertResult(strmsg);
            }
            catch (Exception ex)
            {
                logger.Error("Mqtt rec function error, {0}", ex.ToString());
            }
        }
    }
}
