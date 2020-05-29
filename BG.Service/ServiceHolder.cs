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
using Newtonsoft.Json;

namespace BG.Service
{
    public class ServiceHolder
    {
        private BGLog logger = BGLog.GetLogger(typeof(ServiceHolder));

        //private OPCService HealthOPCService = new OPCService();
        private HeartBeatClient Heart = new HeartBeatClient();
        private CalculateClient Calculate = new CalculateClient();
        private UploadParamClient UploadParam = new UploadParamClient();
        private DownloadParamClient DownloadParam = new DownloadParamClient();     

        public void Run()
        {
            //ThreadPool.QueueUserWorkItem(new WaitCallback(KEEPALIVEListener));
            //ThreadPool.QueueUserWorkItem(new WaitCallback(HeartRecvive));
            //ThreadPool.QueueUserWorkItem(new WaitCallback(CalculateRecvive));
            //ThreadPool.QueueUserWorkItem(new WaitCallback(UploadParamRecvive));
            ThreadPool.QueueUserWorkItem(new WaitCallback(DownloadParamPublish));
        }

       
        private void HeartRecvive(object obj)
        {
            while (!Heart.InitClient(Constants.ServerIP,Constants.ServerPort,Constants.MqttTopicHeart))
            {
                Thread.Sleep(1000 * 5);
            }
            //while (true)
            //{
                Heart.MqttSubscribe(Constants.MqttTopicHeart);
                //string strMsg = JsonHelper.ToJson(Processor.resultArr);//fft  64
                //tc2.MqttPub("wamnv", strMsg.ToString(), 1);
                Thread.Sleep(1000 * 1);
            //}
        }
        private void CalculateRecvive(object obj)
        {
            while (!Calculate.InitClient(Constants.ServerIP, Constants.ServerPort, Constants.MqttTopicCalculate))
            {
                Thread.Sleep(1000 * 5);
            }
            Calculate.MqttSubscribe(Constants.MqttTopicCalculate+"123");
            Thread.Sleep(1000 * 1);
        }
        private void UploadParamRecvive(object obj)
        {
            while (!UploadParam.InitClient(Constants.ServerIP, Constants.ServerPort, Constants.MqttTopicUploadParam))
            {
                Thread.Sleep(1000 * 5);
            }
            UploadParam.MqttSubscribe(Constants.MqttTopicUploadParam + "123");
            Thread.Sleep(1000 * 1);
        }
        private void DownloadParamPublish(object obj)
        {
            while (!DownloadParam.InitClient(Constants.ServerIP, Constants.ServerPort, Constants.MqttTopicUploadParam))
            {
                Thread.Sleep(1000 * 5);
            }
            DownloadParam.MqttMsgPublish(Constants.MqttTopicUploadParam + "123","",0);
            Thread.Sleep(1000 * 1);
        }

        public void Stop()
        {
            Heart.MqttClose();
            Calculate.MqttClose();
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
