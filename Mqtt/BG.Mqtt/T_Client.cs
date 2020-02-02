using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BG.Log;
using BG.Mqtt;
using BG.Utilities;

namespace BG.Service
{
    public class T_Client : Mq
    {
        private BGLog logger = BGLog.GetLogger(typeof(T_Client));

        public override void client_func(string msg)
        {

            Constants.js = Constants.js + 1;
            //string msg = System.Text.Encoding.Default.GetString(e.Message);
            MqttPub("rec", "this is a test.", 2);
        }
    }


}
