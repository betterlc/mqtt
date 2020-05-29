using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;
using BG.Log;
using BG.Contract;
using Newtonsoft.Json;

namespace BG.Database
{
    public class ModifyTableDaoImpl
    {
        private BGLog logger = BGLog.GetLogger(typeof(ModifyTableDaoImpl));
        string sqlSelect_modify = "select modify_table, modify_id, dr_id from tb_modify where status=?status";
        string sqlUpdate_modify = "update into tb_device(sno,model,sample_ratetype,ip,create_time) values(?sno,?model,?sample_ratetype,?ip,?create_time)";

        public List<ModifyTableName> Query(int status)
        {
            try
            {
                List<ModifyTableName> modifyList = new List<ModifyTableName>();
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
                        command.CommandText = sqlSelect_modify;

                        DBUtil.SetParam(command, "status", DbType.Int16, status);
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ModifyTableName modify = new ModifyTableName();
                                modify.Modify_Table = DBUtil.GetString(reader, 0);
                                modify.Modify_ID = DBUtil.GetString(reader, 1);

                                modifyList.Add(modify);
                            }                            
                        }
                        command.Transaction.Commit();
                        return modifyList;
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
