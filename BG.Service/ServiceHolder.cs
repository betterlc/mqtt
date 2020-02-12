using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BG.Log;
using System.Threading;
using BG.Utilities;
using BG.Contract;
using BG.Database;
using BG.Mqtt;

namespace BG.Service
{
    public class ServiceHolder
    {
        private BGLog logger = BGLog.GetLogger(typeof(ServiceHolder));

        //private OPCService HealthOPCService = new OPCService();
        private T_Client tc1 = new T_Client();
        private T_Client tc2 = new T_Client();


        public void Run()
        {

            //ThreadPool.QueueUserWorkItem(new WaitCallback(KEEPALIVEListener));
            ThreadPool.QueueUserWorkItem(new WaitCallback(Tcl1));
            ThreadPool.QueueUserWorkItem(new WaitCallback(Tcl2));

        }


        private void Tcl1(object ob)
        {
            while(!tc1.InitClient("127.0.0.1", 1883, "lift")) //("118.31.5.131",1883,"lift"))
            {
                Thread.Sleep(1000 * 5);//5秒重连
            }
            string strLift = "00"; //二进制 111
            //gps
            float f_x = 125.33017F;
            float f_y_max = 43.953903F, f_y_min = 43.79607F;
            float f_y_interval = (f_y_max - f_y_min) / 100;
            float f_y = f_y_min;
            while (true)
            {
                DateTime dt = DateTime.Now;
                string strTime = dt.ToString(); //time
                strLift = GeneralMethods.InverseBit(strLift); //lift
                string strg_x = f_x.ToString();
                f_y += f_y_interval;
                string strg_y = f_y.ToString();
                var msgVar = new { time = strTime, lift = strLift, g_x = strg_x, g_y = strg_y };
                string strMsg = JsonHelper.ToJson(msgVar);
                //string strMsg = "{lift:\"1\",gps:\"123.123456_431.123456\"}";
                //{"time":"2020-02-08 15:16:17","lift":489745194,"g_x":1234567,"g_y":1234567}
                tc1.MqttPub("lift", strMsg,1);//Constants.js.ToString(), 1);
                Thread.Sleep(1000 * 2);//2秒发布
            }

        }
        private void Tcl2(object ob)
        {
            while (!tc2.InitClient("127.0.0.1",1883,"wamnv"))
            {
                Thread.Sleep(1000 * 5);
            }
            while (true)
            {
                tc2.MqttPub("wamnv", DateTime.Now.AddDays(2).ToString(), 2);
                Thread.Sleep(1000 * 5);
            }

        }



        public void Stop()
        {
            tc1.MqttClose();
            tc2.MqttClose();
        }



        private void KEEPALIVEListener(object obj)
        {
            //KeepAliveOPCService.InitOPCService(Constants.OPCKEEPALIVEPATH, Constants.KeepAliveKeys);

            while (true)
            {
                try
                {
                    //KeepAliveOPCService.HeartBeat();
                }
                catch (Exception e)
                {
                    logger.Error("read keep alive from OPC failed, {0}", e.ToString());
                }

                Thread.Sleep((int)(double.Parse(Constants.KeepAliveDuration) * 1000));
            }
        }

    }
}
