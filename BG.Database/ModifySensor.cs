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
    public class ModifySensor
    {
        private BGLog logger = BGLog.GetLogger(typeof(ModifyDataRecorder));
        public SensorSummaryVO SelectConfig(string id)
        {
            string sqlSelect_config = "select dno,config_version,configDate,datarate from tb_sensor_modify where id=?id";
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
                                SensorSummaryVO modify = new SensorSummaryVO();
                                //modify.sno = DBUtil.GetString(reader, 0);
                                //modify.model = DBUtil.GetString(reader, 1);
                                //modify.sampleRateType = DBUtil.GetString(reader, 2);

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
