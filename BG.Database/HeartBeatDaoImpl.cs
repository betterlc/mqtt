using BG.Log;
using MySql.Data;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG.Database
{
    public class HeartBeatDaoImpl
    {
        private BGLog logger = BGLog.GetLogger(typeof(HeartBeatDaoImpl));

        public void Beat(Dictionary<String, Object> dicHeart)
        {
            string strDno, strConfigVersion, strDateTime;
            string strGps_latitude, strGps_longitude, strGps_altitude, strGps_speed;
            string strDrid = "2";
            strDno = "11";// dicHeart["dno"].ToString();
            strConfigVersion = dicHeart["curTime"].ToString();
            strGps_latitude = dicHeart["gps_Latitude"].ToString();
            strGps_longitude = dicHeart["gps_Longitude"].ToString();
            strGps_altitude = dicHeart["gps_Altitude"].ToString();
            strGps_speed = dicHeart["gps_Speed"].ToString();
            strDateTime = dicHeart["curTime"].ToString();
            string sqlUpdate = "update tb_data_recorder set gps_latitude="+ strGps_latitude+ ",gps_longitude=" + strGps_longitude + ",gps_altitude=" + strGps_altitude + ",gps_speed=" + strGps_speed+ ",datatime='"+ strDateTime + "' where dno='" + strDno + "'";
            string sqlInsert = "insert into tb_data_recorder_his(dr_id,dno,config_version,datatime,gps_latitude,gps_longitude,gps_altitude,gps_speed)"+
                "values('" + strDrid + "', '" + strDno + "', '" + strConfigVersion+"','"+ strDateTime+"','"+
                strGps_latitude + "', '" + strGps_longitude + "', '" + strGps_altitude + "', '" + strGps_speed + "') ";
            string[] sqls = new string[] { sqlUpdate, sqlInsert };
            try
            {
                using (var connection = new MySqlConnection(DBUtil.ConnectionString))
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }

                    using (IDbCommand command = connection.CreateCommand())
                    {
                        command.Transaction = connection.BeginTransaction();
                        foreach (string item in sqls)
                        {
                            command.CommandText = item;
                            DBUtil.SetParam(command, "dno", DbType.String, strDno);

                            command.ExecuteNonQuery();
                        }
                        command.Transaction.Commit();
                    }
                }
            }
            catch (Exception e)
            {
                logger.Error("Beat update date time failed, {0}", e.ToString());
            }
        }
    }
}
