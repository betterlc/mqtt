using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OPCAutomation;
using System.Net;
using BG.Log;

namespace BG.OPC
{
    public class OPCWrapper
    {
        private BGLog logger = BGLog.GetLogger(typeof(OPCWrapper));

        public String GetOPCServerName()
        {
            String hostName = Dns.GetHostName();
            try
            {
                var OPCServer = new OPCServer();
                object names = OPCServer.GetOPCServers(hostName);

                if (names != null && ((Array)names).Length > 0)
                {
                    var name = string.Empty;

                    foreach (var n in ((Array)names))
                    {
                        if (!string.IsNullOrEmpty(n.ToString()))
                        {
                            name = n.ToString();
                            logger.Info("opc server name is {0}", name);
                        }
                    }

                    return name;
                }
            }
            catch (Exception e)
            {
                logger.Error("Get opc server error, {0}", e.ToString());
            }

            return null;
        }

        public OPCServer ConnectRemoteServer(string IP, string OPCServerName)
        {
            try
            {
                var Server = new OPCServer();
                Server.Connect(OPCServerName, IP);

                if (Server.ServerState == (int)OPCServerState.OPCRunning)
                {
                    logger.Info("connected：{0}", Server.ServerName);
                }
                else
                {
                    logger.Warn("state is ：{0}", Server.ServerState.ToString());
                }

                return Server;
            }
            catch (Exception e)
            {
                logger.Error("Connected error, {0}", e.ToString());
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="server"></param>
        /// <param name="groupName"></param>
        /// <param name="writeHandler"></param>
        /// <param name="readHander"></param>
        /// <returns></returns>
        public OPCGroup CreateGroup(OPCServer server, String groupName)
        {
            try
            {
                var OPCGroups = server.OPCGroups;

                var OPCGroup = server.OPCGroups.Add(groupName);
                {
                    server.OPCGroups.DefaultGroupIsActive = true;
                    server.OPCGroups.DefaultGroupDeadband = 0;
                    server.OPCGroups.DefaultGroupUpdateRate = 200;
                    OPCGroup.UpdateRate = 100;
                    OPCGroup.IsSubscribed = true;
                }

                //if (writeHandler != null)
                //{
                //    OPCGroup.AsyncWriteComplete += new DIOPCGroupEvent_AsyncWriteCompleteEventHandler(writeHandler);
                //}

                //if (readHander != null)
                //{
                //    OPCGroup.AsyncReadComplete += new DIOPCGroupEvent_AsyncReadCompleteEventHandler(readHander);
                //}

                return OPCGroup;
            }
            catch (Exception e)
            {
                logger.Error("Create group error , {0}", e.ToString());
                return null;
            }
        }

        public OPCItem AddGroupItem(OPCGroup group, string itemName, int clientHandle)
        {
            try
            {
                var OPCItems = group.OPCItems;
                return OPCItems.AddItem(itemName, clientHandle);
            }
            catch (Exception e)
            {
                logger.Error("add Item failed, {0}", e.ToString());
            }

            return null;
        }

        public void WriteItem(OPCItem item, object obj)
        {
            item.Write(obj);
        }

        public void ReadItem(OPCItem item, out object ItemValues, out object Qualities, out object TimeStamps)
        {
            item.Read(1, out ItemValues, out Qualities, out TimeStamps);
        }

        public object ReadItem(OPCItem item)
        {
            object ItemValues; object Qualities; object TimeStamps;
            item.Read(1, out ItemValues, out Qualities, out TimeStamps);

            return ItemValues;
        }

        public void WriteItemSync(OPCGroup group, OPCItem item, object obj, out Array errors, out int cancelID)
        {
            int[] temp = new int[] { 0, item.ServerHandle };
            Array serverHandles = (Array)temp;
            object[] valueTemp = new object[2] { 0, obj };
            Array values = (Array)valueTemp;

            group.AsyncWrite(2, ref serverHandles, ref values, out errors, 1, out cancelID);
        }

        public void Close(OPCServer server)
        {
            server.OPCGroups.RemoveAll();
            server.Disconnect();
        }
    }
}
