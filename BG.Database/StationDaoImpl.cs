using BG.Contract;
using BG.Log;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG.Database
{
    public class StationDaoImpl
    {
        private BGLog logger = BGLog.GetLogger(typeof(StationDaoImpl));

        public Boolean Exist(int spot)
        {
            string sql = "SELECT * FROM SSC_MELDE WHERE SPOT=:SPOT";

            try
            {
                using (var connection = new OracleConnection(DBUtil.ConnectionString))
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }

                    using (IDbCommand command = connection.CreateCommand())
                    {
                        command.Transaction = connection.BeginTransaction();
                        command.CommandText = sql;
                        DBUtil.SetParam(command, "SPOT", DbType.Int32, spot);
                        using (var reader = command.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                return true;
                            }
                        }
                        command.Transaction.Commit();

                    }
                }
            }
            catch (Exception e)
            {
                logger.Error("get station failed, {0}", e.ToString());
            }

            return false;
        }

        public void Delete(int spot)
        {
            string sql = "delete from SSC_MELDE WHERE SPOT=:SPOT";

            try
            {
                using (var connection = new OracleConnection(DBUtil.ConnectionString))
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }

                    using (IDbCommand command = connection.CreateCommand())
                    {
                        command.Transaction = connection.BeginTransaction();
                        command.CommandText = sql;
                        DBUtil.SetParam(command, "SPOT", DbType.Int32, spot);
                        command.ExecuteNonQuery();

                        command.Transaction.Commit();
                    }
                }
            }
            catch (Exception e)
            {
                logger.Error("delete station failed, {0}", e.ToString());
            }
        }

        public void Update(Station station)
        {
            string sql = @"update SSC_MELDE set 
                           KENN=:KENN,
                           LINE=:LINE,
                           SKID=:SKID,
                           TOPCOAT=:TOPCOAT,
                           SPECIAL=:SPECIAL,
                           BODYCODE=:BODYCODE 
                           where SPOT=:SPOT";

            try
            {
                using (var connection = new OracleConnection(DBUtil.ConnectionString))
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }

                    using (IDbCommand command = connection.CreateCommand())
                    {
                        command.Transaction = connection.BeginTransaction();
                        command.CommandText = sql;

                        DBUtil.SetParam(command, "KENN", DbType.Int32, station.KENN);
                        DBUtil.SetParam(command, "LINE", DbType.Int32, station.LINE);
                        DBUtil.SetParam(command, "SKID", DbType.Int32, station.SKID);
                        DBUtil.SetParam(command, "TOPCOAT", DbType.String, station.TOPCOAT);
                        DBUtil.SetParam(command, "SPECIAL", DbType.String, station.SPECIAL);
                        DBUtil.SetParam(command, "BODYCODE", DbType.String, station.BODYCODE);
                        DBUtil.SetParam(command, "SPOT", DbType.Int32, station.SPOT);

                        command.ExecuteNonQuery();
                        command.Transaction.Commit();
                        logger.Info("update station 200, Spot IS {0}", station.SPOT);
                    }
                }
            }
            catch (Exception e)
            {
                logger.Error("update station failed, {0}", e.ToString());
            }
        }

        public void Create(Station station)
        {
            string sql = "insert into SSC_MELDE(SPOT,NOT_TIME,KENN,LINE,SKID,TOPCOAT,SPECIAL,BODYCODE,FA) values(:SPOT,:NOT_TIME,:KENN,:LINE,:SKID,:TOPCOAT,:SPECIAL,:BODYCODE,:FA)";

            try
            {
                using (var connection = new OracleConnection(DBUtil.ConnectionString))
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }

                    using (IDbCommand command = connection.CreateCommand())
                    {
                        command.Transaction = connection.BeginTransaction();
                        command.CommandText = sql;

                        DBUtil.SetParam(command, "SPOT", DbType.Int32, station.SPOT);
                        DBUtil.SetParam(command, "NOT_TIME", DbType.DateTime, station.NOT_TIME);
                        DBUtil.SetParam(command, "KENN", DbType.Int32, station.KENN);
                        DBUtil.SetParam(command, "LINE", DbType.Int32, station.LINE);
                        DBUtil.SetParam(command, "SKID", DbType.Int32, station.SKID);
                        DBUtil.SetParam(command, "TOPCOAT", DbType.String, station.TOPCOAT);
                        DBUtil.SetParam(command, "SPECIAL", DbType.String, station.SPECIAL);
                        DBUtil.SetParam(command, "BODYCODE", DbType.String, station.BODYCODE);
                        DBUtil.SetParam(command, "FA", DbType.Int32, 1);

                        command.ExecuteNonQuery();
                        command.Transaction.Commit();
                        logger.Info("create station 200, spot is {0}", station.SPOT);
                    }
                }
            }
            catch (Exception e)
            {
                logger.Error("create station failed, {0}", e.ToString());
            }
        }

        public Station Convert(OPCContract contract)
        {
            Station result = new Station();
            result.SPOT = int.Parse(contract.SpotFrom);
            result.SPECIAL = contract.Spec_ID;
            result.SKID = int.Parse(contract.Skid_Nr);
            result.BODYCODE = contract.Body;
            result.KENN = int.Parse(contract.Body_ID);
            result.TOPCOAT = contract.color;
            result.NOT_TIME = DateTime.Now;
            result.LINE = contract.Section;
            return result;
        }

    }
}
