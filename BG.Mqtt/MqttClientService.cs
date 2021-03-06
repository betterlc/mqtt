﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Threading;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using BG.Log;
using BG.Utilities;
using System.Data;

namespace BG.Mqtt
{
    public class MqttClientService
    {
        private BGLog logger = BGLog.GetLogger(typeof(MqttClientService));
        public MqttClient client { get; set; }
        private byte[] qosLevels = new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE};

        private string Host { get; set; }
        private int Serverport { get; set; }
        private string Topic { get; set; }

        private string ClientID = Guid.NewGuid().ToString();
        private string UserName = null;
        private string UserPwd = null;

        public bool InitClient(string _host, int _serverport,string _topic)
        {
            this.Host = _host;this.Serverport = _serverport;this.Topic = _topic;
            try
            {
                this.client = new MqttClient(this.Host, this.Serverport, false, MqttSslProtocols.None, null, null);
                client.ProtocolVersion = MqttProtocolVersion.Version_3_1;
                client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;// register to message received
                client.ConnectionClosed += MqttConnectClosed;

                if (String.IsNullOrEmpty(this.UserName))
                {
                    int num2 = client.Connect(ClientID, null, null, true, 60);
                }
                else
                {
                    client.Connect(ClientID, this.UserName, this.UserPwd, true, 60);
                }

                if (client.IsConnected)
                {
                    logger.Info("Connected mqtt server.");
                    MqttSubscribe(this.Topic);
                    return true;
                }
                else
                {
                    logger.Info("Connect mqtt server failed.");
                    return false;
                }
            }
            catch (Exception e)
            {
                logger.Error("Connect mqtt server error, {0}", e.ToString());
            }
            return false;
        }

        public bool ReInitClient()
        {
            try
            {
                this.client = new MqttClient(this.Host, this.Serverport, false, MqttSslProtocols.None, null, null);
                client.ProtocolVersion = MqttProtocolVersion.Version_3_1;
                client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;
                client.ConnectionClosed += MqttConnectClosed;

                if (String.IsNullOrEmpty(this.UserName))
                {
                    client.Connect(ClientID, null, null, true, 60);
                }
                else
                {
                    client.Connect(ClientID, this.UserName, this.UserPwd, true, 60);
                }


                if (client.IsConnected)
                {
                    logger.Info("Reconnected mqtt server.");
                    MqttSubscribe(this.Topic);
                    return true;
                }
                else
                {
                    logger.Info("Reconnect mqtt server failed.");
                    return false;
                }
            }
            catch (Exception e)
            {
                logger.Error("Reconnect mqtt server error, {0}", e.ToString());
            }
            return false;
        }

        public virtual void MqttSubscribe(string topic)
        {
            if(!string.IsNullOrEmpty(topic))
            {
                client.Subscribe(new string[] { topic }, this.qosLevels);
            }
        }

        public virtual void MqttMsgPublish(string topic, string msg,byte level)
        {
            client.Publish(topic, Encoding.UTF8.GetBytes(msg), level, false);
        }

        public void MqttConnectClosed(object sender, EventArgs e)
        {
            if (client.IsConnected) return;
            Thread retry = new Thread(new ThreadStart(delegate
            {
                while(client == null || !client.IsConnected)
                {
                    ReInitClient();
                    if(!client.IsConnected)
                    {
                        Thread.Sleep(1000 * 3);
                    }
                    else
                    {
                        break;
                    }
                }
            }));
            retry.Start();
        }

        public void MqttClose()
        {
            client.Disconnect();
        }

        public void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            string msg = System.Text.Encoding.Default.GetString(e.Message);
            try
            {
                client_func(msg);
            }
            catch (Exception ex)
            {
                logger.Error("Mqtt rec function error, {0}", ex.ToString());
            }

        }

        public virtual void client_func(string msg)
        {
        }

        
    }
}
