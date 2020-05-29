using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;
using BG.Log;
using BG.Contract;
using BG.Utilities;

namespace BG.Database
{
    public class ModifyChannel
    {
        private BGLog logger = BGLog.GetLogger(typeof(ModifyDataRecorder));
        public ChannelSummaryVO SelectConfig(string id)
        {
            string sqlSelect_config = "select * from tb_channel_modify where id=?id";
            try
            {
                //using (var connection = new MySqlConnection(DBUtil.ConnectionString))
                using (var connection = new MySqlConnection("server=localhost;user id=root;password=root;database=railway"))
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }

                    //using (IDbCommand command = connection.CreateCommand())
                    using (MySqlCommand command = connection.CreateCommand())
                    {
                        command.Transaction = connection.BeginTransaction();
                        command.CommandText = sqlSelect_config;

                        DBUtil.SetParam(command, "id", DbType.Int16, Convert.ToInt16(id));
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ChannelSummaryVO modify = new ChannelSummaryVO();
                                modify.connectorIndex = DBUtil.GetInt(reader, 1);
                                modify.channelIndex = DBUtil.GetInt(reader, 2);
                                modify.name = DBUtil.GetString(reader, 3);
                                modify.filter = DBUtil.GetString(reader, 4);
                                modify.precision = DBUtil.GetInt(reader, 6);
                                modify.sampleRate = DBUtil.GetInt(reader, 7);                                
                                modify.behz = DBUtil.GetInt(reader, 10);
                                modify.unit = DBUtil.GetString(reader, 11);
                                modify.zerovalue = DBUtil.GetInt(reader, 12);
                                modify.sensorName = DBUtil.GetString(reader, 15);
                                modify.th= DBUtil.GetString(reader, 18);
                                modify.ts = DBUtil.GetString(reader, 19);
                                modify.maxvalue = DBUtil.GetInt(reader, 20);
                                modify.minvalue = DBUtil.GetInt(reader, 21);
                                modify.maxvaluespeed = DBUtil.GetInt(reader, 22);
                                modify.minvaluespeed = DBUtil.GetInt(reader, 23);
                                modify.maxvaluetime = DBUtil.GetString(reader, 24);
                                modify.minvaluetime = DBUtil.GetString(reader, 25);
                                modify.rms = DBUtil.GetInt(reader, 26);

                                return modify;
                            }
                        }
                        command.Transaction.Commit();
                    }
                }
            }
            catch (Exception e)
            {
                logger.Error("device dao implement failed, {0}", e.ToString());
            }
            return null;
        }
    }
}
