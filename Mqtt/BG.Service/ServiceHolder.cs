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
            while(!tc1.InitClient())
            {
                Thread.Sleep(1000 * 5);
            }
            while (true)
            {
                tc1.MqttPub("wamnv", Constants.js.ToString(), 2);
                Thread.Sleep(1000 * 10);
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
