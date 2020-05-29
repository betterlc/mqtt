using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using BG.Log;
using BG.Contract;

namespace BG.Database
{
    public class CalculateResultDaoImpl
    {
        private BGLog logger = BGLog.GetLogger(typeof(CalculateResultDaoImpl));
        public void InsertResult(string msg)
        {
            try
            {
                MqttCalculateResultVO resultVo = JsonConvert.DeserializeObject<MqttCalculateResultVO>(msg);
                List<Dictionary<string,string>> list = new List<Dictionary<string, string>>();
                
                foreach (var value in resultVo.result)
                {
                    Dictionary<string, string> dic = new Dictionary<string, string>();
                    dic.Add("caValue", value);
                    dic.Add("caTime", "1");
                    list.Add(dic);
                }
                string strResult = "{result:" + JsonConvert.SerializeObject(list) + "}";
                string strtimemillis = "1";
                
                string sqlInsert = "insert into tb_caculate_result(function_id,dno,cal_date,begin_time,end_time,timemillis,value)" +
                "values('" + resultVo.funcID + "', '" + resultVo.dno + "', '" + resultVo.beginTime + "','" + resultVo.beginTime + "','" +
                resultVo.endTime + "', '" + strtimemillis + "', '" + strResult + "') ";
                string[] sqls = new string[] { sqlInsert };
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
                            DBUtil.SetParam(command, "dno", DbType.String, resultVo.dno);

                            command.ExecuteNonQuery();
                        }
                        command.Transaction.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("Calculate insert result data failed, {0}", ex.ToString());
            }
        }
    }
}
