using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BG.Contract;
using System.Data;
using BG.Log;
using BG.Utilities;
using Oracle.ManagedDataAccess.Client;

namespace BG.Database
{
    public class SSCLagerDaoImpl
    {
        private BGLog logger = BGLog.GetLogger(typeof(SSCLagerDaoImpl));

        //public void ReCalculatePotision(int s, int l)
        //{
        //    string sql = "UPDATE SSC_LAGER SET FACH=FACH-1 WHERE S=:S AND L=:L";

        //    try
        //    {
        //        using (var connection = new OracleConnection(DBUtil.ConnectionString))
        //        {
        //            if (connection.State == ConnectionState.Closed)
        //            {
        //                connection.Open();
        //            }

        //            using (IDbCommand command = connection.CreateCommand())
        //            {
        //                command.CommandText = sql;

        //                DBUtil.SetParam(command, "S", DbType.Int16, s);
        //                DBUtil.SetParam(command, "L", DbType.Int16, l);

        //                int result = command.ExecuteNonQuery();

        //                logger.Info(" ReCalculatePotision 200, S IS {0}, L IS {1}", s, l);
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        logger.Error(" ReCalculatePotision failed, {0}", e.ToString());
        //    }
        //}

        public List<SSCLager> GetAll(int s)
        {
            string sql = "SELECT FACH, S, L, Y, VON, STAT, KENN, LINE,  SKID, TOPCOAT, SPECIAL, BODYCODE FROM SSC_LAGER WHERE S=:S";
            List<SSCLager> result = new List<SSCLager>();

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
                        DBUtil.SetParam(command, "S", DbType.Int32, s);

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                try
                                {
                                    SSCLager lager = new SSCLager();
                                    lager.FACH = DBUtil.GetInt(reader, 0);
                                    lager.S = DBUtil.GetInt(reader, 1);
                                    lager.L = DBUtil.GetInt(reader, 2);
                                    lager.Y = DBUtil.GetInt(reader, 3);
                                    lager.VON = reader.GetDateTime(4);
                                    lager.STAT = DBUtil.GetInt(reader, 5);
                                    lager.KENN = DBUtil.GetInt(reader, 6);
                                    lager.LINE = DBUtil.GetInt(reader, 7);
                                    lager.SKID = DBUtil.GetInt(reader, 8);
                                    lager.TOPCOAT = DBUtil.GetString(reader, 9);
                                    lager.SPECIAL = DBUtil.GetString(reader, 10);
                                    lager.BODYCODE = DBUtil.GetString(reader, 11);
                                    result.Add(lager);
                                }
                                catch (Exception e)
                                {
                                    logger.Error("get ssc data failed, {0}", e.ToString());
                                }
                            }
                        }

                        command.Transaction.Commit();

                    }
                }
            }
            catch (Exception e)
            {
                logger.Error("get ssclager failed, {0}", e.ToString());
            }

            return result;
        }

        public List<SSCLager> GetCarsToLine7(int s, int state)
        {
            string sql = "SELECT FACH, S, L, Y, VON, STAT, KENN, LINE,  SKID, TOPCOAT, SPECIAL, BODYCODE FROM SSC_LAGER WHERE S=:S AND STAT=:STAT AND Y=1";
            List<SSCLager> result = new List<SSCLager>();

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
                        DBUtil.SetParam(command, "S", DbType.Int32, s);
                        DBUtil.SetParam(command, "STAT", DbType.Int32, state);

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                try
                                {
                                    SSCLager lager = new SSCLager();
                                    lager.FACH = DBUtil.GetInt(reader, 0);
                                    lager.S = DBUtil.GetInt(reader, 1);
                                    lager.L = DBUtil.GetInt(reader, 2);
                                    lager.Y = DBUtil.GetInt(reader, 3);
                                    lager.VON = reader.GetDateTime(4);
                                    lager.STAT = DBUtil.GetInt(reader, 5);
                                    lager.KENN = DBUtil.GetInt(reader, 6);
                                    lager.LINE = DBUtil.GetInt(reader, 7);
                                    lager.SKID = DBUtil.GetInt(reader, 8);
                                    lager.TOPCOAT = DBUtil.GetString(reader, 9);
                                    lager.SPECIAL = DBUtil.GetString(reader, 10);
                                    lager.BODYCODE = DBUtil.GetString(reader, 11);
                                    result.Add(lager);
                                }
                                catch (Exception e)
                                {
                                    logger.Error("get ssc data failed, {0}", e.ToString());
                                }
                            }
                        }

                        command.Transaction.Commit();

                    }
                }
            }
            catch (Exception e)
            {
                logger.Error("get ssclager failed, {0}", e.ToString());
            }

            return result;
        }

        public List<SSCLager> GetAll()
        {
            string sql = "SELECT FACH, S, L, Y, VON, STAT, KENN, LINE,  SKID, TOPCOAT, SPECIAL, BODYCODE FROM SSC_LAGER";
            List<SSCLager> result = new List<SSCLager>();

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

                        using (var reader = command.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                try
                                {
                                    SSCLager lager = new SSCLager();
                                    lager.FACH = DBUtil.GetInt(reader, 0);
                                    lager.S = DBUtil.GetInt(reader, 1);
                                    lager.L = DBUtil.GetInt(reader, 2);
                                    lager.Y = DBUtil.GetInt(reader, 3);
                                    lager.VON = reader.GetDateTime(4);
                                    lager.STAT = DBUtil.GetInt(reader, 5);
                                    lager.KENN = DBUtil.GetInt(reader, 6);
                                    lager.LINE = DBUtil.GetInt(reader, 7);
                                    lager.SKID = DBUtil.GetInt(reader, 8);
                                    lager.TOPCOAT = DBUtil.GetString(reader, 9);
                                    lager.SPECIAL = DBUtil.GetString(reader, 10);
                                    lager.BODYCODE = DBUtil.GetString(reader, 11);
                                    result.Add(lager);
                                }
                                catch (Exception e)
                                {
                                    logger.Error("get ssc data failed, {0}", e.ToString());
                                }
                            }
                        }

                        command.Transaction.Commit();

                    }
                }
            }
            catch (Exception e)
            {
                logger.Error("get ssclager failed, {0}", e.ToString());
            }

            return result;
        }

        public void CheckTimeOut(int s, double h)
        {
            if (h == 0) return;

            var dateTime = DateTime.Now.AddHours(-h);

            logger.Info("check time out, interval is {0}, date is {1}", h, dateTime.ToString("yyyy.MM.dd. HH:mm:ss"));

            string sql = "UPDATE SSC_LAGER SET STAT=:STAT WHERE S=:S AND STAT<>2 AND STAT<>3 AND INTIME < TO_DATE('" + dateTime.ToString("yyyy.MM.dd. HH:mm:ss") + "','" + "yyyy.mm.dd. hh24:mi:ss" + "')";

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
                        DBUtil.SetParam(command, "STAT", DbType.Int32, 9);
                        DBUtil.SetParam(command, "S", DbType.Int32, s);
                        var reader = command.ExecuteNonQuery();

                        command.Transaction.Commit();
                    }
                }
            }
            catch (Exception e)
            {
                logger.Error("check time out lager failed, {0}", e.ToString());
            }

        }

        public int GetLagerCount(int s, int l)
        {
            string sql = "SELECT COUNT(*) FROM SSC_LAGER WHERE S=:S AND L=:L";

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
                        DBUtil.SetParam(command, "S", DbType.Int32, s);
                        DBUtil.SetParam(command, "L", DbType.Int32, l);
                        using (var reader = command.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                try
                                {
                                    return DBUtil.GetInt(reader, 0);
                                }
                                catch (Exception e)
                                {
                                    logger.Error("get ssc data failed, {0}", e.ToString());
                                }
                            }
                        }

                        command.Transaction.Commit();

                    }
                }
            }
            catch (Exception e)
            {
                logger.Error("get ssclager failed, {0}", e.ToString());
            }

            return 0;
        }

        public int GetRealLagerCount(int s, int l)
        {
            string sql = "SELECT COUNT(*) FROM SSC_LAGER WHERE S=:S AND L=:L AND STAT<>2";

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
                        DBUtil.SetParam(command, "S", DbType.Int32, s);
                        DBUtil.SetParam(command, "L", DbType.Int32, l);
                        using (var reader = command.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                try
                                {
                                    return DBUtil.GetInt(reader, 0);
                                }
                                catch (Exception e)
                                {
                                    logger.Error("get ssc data failed, {0}", e.ToString());
                                }
                            }
                        }

                        command.Transaction.Commit();

                    }
                }
            }
            catch (Exception e)
            {
                logger.Error("get ssclager failed, {0}", e.ToString());
            }

            return 0;
        }

        public int ClearOutingState(int s)
        {
            string sql = "Update SSC_LAGER SET STAT=1 WHERE STAT=3 AND S=:S";

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
                        DBUtil.SetParam(command, "S", DbType.Int32, s);
                        command.ExecuteNonQuery();
                        command.Transaction.Commit();

                    }
                }
            }
            catch (Exception e)
            {
                logger.Error("clear outing state failed, {0}", e.ToString());
            }

            return 0;
        }

        public int GetOutingCount(int s)
        {
            string sql = "SELECT COUNT(*) FROM SSC_LAGER WHERE STAT=3 AND S=:S";

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
                        DBUtil.SetParam(command, "S", DbType.Int32, s);
                        using (var reader = command.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                try
                                {
                                    return DBUtil.GetInt(reader, 0);
                                }
                                catch (Exception e)
                                {
                                    logger.Error("get outing count failed, {0}", e.ToString());
                                }
                            }
                        }
                        command.Transaction.Commit();

                    }
                }
            }
            catch (Exception e)
            {
                logger.Error("get outing count failed, {0}", e.ToString());
            }

            return 0;
        }

        public List<SSCLager> GetAll(int s, int l)
        {
            string sql = "SELECT FACH, S, L, Y, VON, STAT, KENN, LINE,  SKID, TOPCOAT, SPECIAL, BODYCODE, INTIME FROM SSC_LAGER WHERE S=:S AND L=:L";
            List<SSCLager> result = new List<SSCLager>();

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

                        DBUtil.SetParam(command, "S", DbType.Int32, s);
                        DBUtil.SetParam(command, "L", DbType.Int32, l);
                        using (var reader = command.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                try
                                {
                                    SSCLager lager = new SSCLager();
                                    lager.FACH = DBUtil.GetInt(reader, 0);
                                    lager.S = DBUtil.GetInt(reader, 1);
                                    lager.L = DBUtil.GetInt(reader, 2);
                                    lager.Y = DBUtil.GetInt(reader, 3);
                                    lager.VON = reader.GetDateTime(4);
                                    lager.STAT = DBUtil.GetInt(reader, 5);
                                    lager.KENN = DBUtil.GetInt(reader, 6);
                                    lager.LINE = DBUtil.GetInt(reader, 7);
                                    lager.SKID = DBUtil.GetInt(reader, 8);
                                    lager.TOPCOAT = DBUtil.GetString(reader, 9);
                                    lager.SPECIAL = DBUtil.GetString(reader, 10);
                                    lager.BODYCODE = DBUtil.GetString(reader, 11);
                                    lager.InTime = reader.GetDateTime(12);
                                    result.Add(lager);
                                }
                                catch (Exception e)
                                {
                                    logger.Error("get ssc data failed, {0}", e.ToString());
                                }
                            }
                        }

                        command.Transaction.Commit();

                    }
                }
            }
            catch (Exception e)
            {
                logger.Error("get ssclager failed, {0}", e.ToString());
            }

            return result;
        }

        public SSCLager GetBySection(int s)
        {
            string sql = "SELECT FACH, S, L, Y, VON, STAT, KENN, LINE,  SKID, TOPCOAT, SPECIAL, BODYCODE FROM SSC_LAGER WHERE S=:S";

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
                        DBUtil.SetParam(command, "S", DbType.Int32, s);

                        using (var reader = command.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                SSCLager lager = new SSCLager();
                                lager.FACH = DBUtil.GetInt(reader, 0);
                                lager.S = DBUtil.GetInt(reader, 1);
                                lager.L = DBUtil.GetInt(reader, 2);
                                lager.Y = DBUtil.GetInt(reader, 3);
                                lager.VON = reader.GetDateTime(4);
                                lager.STAT = DBUtil.GetInt(reader, 5);
                                lager.KENN = DBUtil.GetInt(reader, 6);
                                lager.LINE = DBUtil.GetInt(reader, 7);
                                lager.SKID = DBUtil.GetInt(reader, 8);
                                lager.TOPCOAT = DBUtil.GetString(reader, 9);
                                lager.SPECIAL = DBUtil.GetString(reader, 10);
                                lager.BODYCODE = DBUtil.GetString(reader, 11);
                                return lager;
                            }
                        }

                        command.Transaction.Commit();
                    }
                }
            }
            catch (Exception e)
            {
                logger.Error("get ssclager failed, {0}", e.ToString());
            }

            return null;
        }

        public SSCLager Get(int FACH)
        {
            string sql = "SELECT FACH, S, L, Y, VON, STAT, KENN, LINE,  SKID, TOPCOAT, SPECIAL, BODYCODE,INTIME FROM SSC_LAGER WHERE FACH=:FACH";

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
                        DBUtil.SetParam(command, "FACH", DbType.Int32, FACH);

                        using (var reader = command.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                SSCLager lager = new SSCLager();
                                lager.FACH = DBUtil.GetInt(reader, 0);
                                lager.S = DBUtil.GetInt(reader, 1);
                                lager.L = DBUtil.GetInt(reader, 2);
                                lager.Y = DBUtil.GetInt(reader, 3);
                                lager.VON = reader.GetDateTime(4);
                                lager.STAT = DBUtil.GetInt(reader, 5);
                                lager.KENN = DBUtil.GetInt(reader, 6);
                                lager.LINE = DBUtil.GetInt(reader, 7);
                                lager.SKID = DBUtil.GetInt(reader, 8);
                                lager.TOPCOAT = DBUtil.GetString(reader, 9);
                                lager.SPECIAL = DBUtil.GetString(reader, 10);
                                lager.BODYCODE = DBUtil.GetString(reader, 11);
                                lager.InTime = DBUtil.GetDateTime(reader, 12);
                                return lager;
                            }
                        }

                        command.Transaction.Commit();
                    }
                }
            }
            catch (Exception e)
            {
                logger.Error("get ssclager failed, {0}", e.ToString());
            }

            return null;
        }

        public int GetLagerCount(int s)
        {
            string sql = "SELECT COUNT(*) FROM SSC_LAGER WHERE S=:S";

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
                        DBUtil.SetParam(command, "S", DbType.Int32, s);
                        using (var reader = command.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                try
                                {
                                    return DBUtil.GetInt(reader, 0);
                                }
                                catch (Exception e)
                                {
                                    logger.Error("get ssc count failed, {0}", e.ToString());
                                }
                            }
                        }

                        command.Transaction.Commit();

                    }
                }
            }
            catch (Exception e)
            {
                logger.Error("get ssclager count failed, {0}", e.ToString());
            }
            return 0;
        }

        public void DeleteAllRecords()
        {
            string sql = "delete from SSC_LAGER";

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

                        command.ExecuteNonQuery();
                        command.Transaction.Commit();

                        logger.Info("delete all records 200");
                    }
                }
            }
            catch (Exception e)
            {
                logger.Error("delete ssclager failed, {0}", e.ToString());
            }
        }

        public void DeleteOneSection(int section)
        {
            string sql = "delete from SSC_LAGER where S=:S";

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
                        DBUtil.SetParam(command, "S", DbType.Int32, section);
                        command.ExecuteNonQuery();
                        command.Transaction.Commit();

                        logger.Info("delete all records 200");
                    }
                }
            }
            catch (Exception e)
            {
                logger.Error("delete ssclager failed, {0}", e.ToString());
            }
        }

        public void ReCalculatePotision(int s)
        {
            var lagers = GetAll(s, 7);
            lagers = lagers.OrderByDescending(m => m.Y).ToList();

            if (lagers.Count == 0) return;

            using (var connection = new OracleConnection(DBUtil.ConnectionString))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                using (IDbCommand command = connection.CreateCommand())
                {
                    command.Transaction = connection.BeginTransaction();

                    string sql = "delete from SSC_LAGER where FACH=:FACH";

                    command.CommandText = sql;
                    command.Parameters.Clear();
                    DBUtil.SetParam(command, "FACH", DbType.Int32, lagers[0].FACH);
                    command.ExecuteNonQuery();

                    lagers.RemoveAt(0);

                    foreach (var lager in lagers)
                    {
                        lager.Y++;
                        lager.FACH++;

                        string sql1 = "insert into SSC_LAGER(FACH,S,L,Y,VON,STAT,KENN,LINE,SKID,TOPCOAT,SPECIAL,BODYCODE,INTIME) values(:FACH,:S,:L,:Y,:VON,:STAT,:KENN,:LINE,:SKID,:TOPCOAT,:SPECIAL,:BODYCODE,:INTIME)";

                        command.CommandText = sql1;
                        command.Parameters.Clear();
                        DBUtil.SetParam(command, "FACH", DbType.Int32, lager.FACH);
                        DBUtil.SetParam(command, "S", DbType.Int32, lager.S);
                        DBUtil.SetParam(command, "L", DbType.Int32, lager.L);
                        DBUtil.SetParam(command, "Y", DbType.Int32, lager.Y);
                        DBUtil.SetParam(command, "VON", DbType.DateTime, lager.VON);
                        DBUtil.SetParam(command, "STAT", DbType.Int32, lager.STAT);
                        DBUtil.SetParam(command, "KENN", DbType.Int32, lager.KENN);
                        DBUtil.SetParam(command, "LINE", DbType.Int32, lager.LINE);
                        DBUtil.SetParam(command, "SKID", DbType.Int32, lager.SKID);
                        DBUtil.SetParam(command, "TOPCOAT", DbType.String, lager.TOPCOAT);
                        DBUtil.SetParam(command, "SPECIAL", DbType.String, lager.SPECIAL);
                        DBUtil.SetParam(command, "BODYCODE", DbType.String, lager.BODYCODE);
                        DBUtil.SetParam(command, "INTIME", DbType.DateTime, lager.InTime);
                        command.ExecuteNonQuery();


                        command.CommandText = sql;
                        command.Parameters.Clear();
                        DBUtil.SetParam(command, "FACH", DbType.Int32, lager.FACH - 1);
                        command.ExecuteNonQuery();

                    }

                    command.Transaction.Commit();
                }
            }
        }

        public void ReCalculatePotision(int s, int l)
        {
            var lagers = GetAll(s, l);
            lagers = lagers.OrderBy(m => m.Y).ToList();

            if (lagers.Count == 0) return;

            using (var connection = new OracleConnection(DBUtil.ConnectionString))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                using (IDbCommand command = connection.CreateCommand())
                {
                    command.Transaction = connection.BeginTransaction();

                    string sql = "delete from SSC_LAGER where FACH=:FACH";

                    command.CommandText = sql;
                    command.Parameters.Clear();
                    DBUtil.SetParam(command, "FACH", DbType.Int32, lagers[0].FACH);
                    command.ExecuteNonQuery();

                    lagers.RemoveAt(0);

                    foreach (var lager in lagers)
                    {
                        lager.Y--;
                        lager.FACH--;

                        string sql1 = "insert into SSC_LAGER(FACH,S,L,Y,VON,STAT,KENN,LINE,SKID,TOPCOAT,SPECIAL,BODYCODE,INTIME) values(:FACH,:S,:L,:Y,:VON,:STAT,:KENN,:LINE,:SKID,:TOPCOAT,:SPECIAL,:BODYCODE,:INTIME)";

                        command.CommandText = sql1;
                        command.Parameters.Clear();
                        DBUtil.SetParam(command, "FACH", DbType.Int32, lager.FACH);
                        DBUtil.SetParam(command, "S", DbType.Int32, lager.S);
                        DBUtil.SetParam(command, "L", DbType.Int32, lager.L);
                        DBUtil.SetParam(command, "Y", DbType.Int32, lager.Y);
                        DBUtil.SetParam(command, "VON", DbType.DateTime, lager.VON);
                        DBUtil.SetParam(command, "STAT", DbType.Int32, lager.STAT);
                        DBUtil.SetParam(command, "KENN", DbType.Int32, lager.KENN);
                        DBUtil.SetParam(command, "LINE", DbType.Int32, lager.LINE);
                        DBUtil.SetParam(command, "SKID", DbType.Int32, lager.SKID);
                        DBUtil.SetParam(command, "TOPCOAT", DbType.String, lager.TOPCOAT);
                        DBUtil.SetParam(command, "SPECIAL", DbType.String, lager.SPECIAL);
                        DBUtil.SetParam(command, "BODYCODE", DbType.String, lager.BODYCODE);
                        DBUtil.SetParam(command, "INTIME", DbType.DateTime, lager.InTime);
                        command.ExecuteNonQuery();


                        command.CommandText = sql;
                        command.Parameters.Clear();
                        DBUtil.SetParam(command, "FACH", DbType.Int32, lager.FACH + 1);
                        command.ExecuteNonQuery();

                    }

                    command.Transaction.Commit();
                }
            }
        }

        public void Delete(int FACH)
        {
            string sql = "delete from SSC_LAGER where FACH=:FACH";

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
                        DBUtil.SetParam(command, "FACH", DbType.Int32, FACH);
                        command.ExecuteNonQuery();
                        command.Transaction.Commit();

                        logger.Info("delete record 200, fach is {0}", FACH);
                    }
                }
            }
            catch (Exception e)
            {
                logger.Error("delete ssclager failed, {0}", e.ToString());
            }
        }

        public void DeleteOneLineInSection(int section, int line)
        {
            string sql = "delete from SSC_LAGER where S=:S and L=:L";

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
                        DBUtil.SetParam(command, "S", DbType.Int32, section);
                        DBUtil.SetParam(command, "L", DbType.Int32, line);
                        command.ExecuteNonQuery();
                        command.Transaction.Commit();

                        logger.Info("delete all records 200");
                    }
                }
            }
            catch (Exception e)
            {
                logger.Error("delete ssclager failed, {0}", e.ToString());
            }
        }

        /// <summary>
        /// FACH,S,L,Y,VON,STAT,KENN,LINE,SKID,TOPCOAT,SPECIAL,BODYCODE
        /// </summary>
        /// <param name="lager"></param>
        public void Update(SSCLager lager)
        {
            string sql = @"update SSC_LAGER set 
                           LINE=:LINE,
                           KENN=:KENN,
                           SPECIAL=:SPECIAL,
                           BODYCODE=:BODYCODE,
                           SKID=:SKID,
                           TOPCOAT=:TOPCOAT where FACH=:FACH";

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

                        DBUtil.SetParam(command, "LINE", DbType.Int32, lager.LINE);
                        DBUtil.SetParam(command, "KENN", DbType.Int32, lager.KENN);
                        DBUtil.SetParam(command, "SPECIAL", DbType.String, lager.SPECIAL);
                        DBUtil.SetParam(command, "BODYCODE", DbType.String, lager.BODYCODE);
                        DBUtil.SetParam(command, "SKID", DbType.Int32, lager.SKID);
                        DBUtil.SetParam(command, "TOPCOAT", DbType.String, lager.TOPCOAT);
                        DBUtil.SetParam(command, "FACH", DbType.Int32, lager.FACH);

                        int result = command.ExecuteNonQuery();
                        command.Transaction.Commit();

                        logger.Info("update lager 200, FACH IS {0}", lager.FACH);
                    }
                }
            }
            catch (Exception e)
            {
                logger.Error("update lager failed, {0}", e.ToString());
            }
        }

        public void UpdateState(String topcoat, String bodyCode, int state, int s)
        {
            string sql = "UPDATE SSC_LAGER SET STAT=:STAT WHERE BODYCODE=:BODYCODE AND TOPCOAT=:TOPCOAT AND (STAT=1 OR STAT=4) AND S=:S";

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

                        DBUtil.SetParam(command, "STAT", DbType.Int32, state);
                        DBUtil.SetParam(command, "BODYCODE", DbType.String, bodyCode);
                        DBUtil.SetParam(command, "TOPCOAT", DbType.String, topcoat);
                        DBUtil.SetParam(command, "S", DbType.Int32, s);

                        int result = command.ExecuteNonQuery();
                        command.Transaction.Commit();
                    }
                }
            }
            catch (Exception e)
            {
                logger.Error("update state failed, {0}", e.ToString());
            }
        }

        //public void ClearLockState(String topcoat, String bodyCode, int state, int section)
        // {
        //    string sql = "UPDATE SSC_LAGER SET STAT=:STAT WHERE BODYCODE=:BODYCODE AND TOPCOAT=:TOPCOAT AND STAT=6 AND S=:S AND Y=1";

        //    if (topcoat == "****")
        //    {
        //        sql = "UPDATE SSC_LAGER SET STAT=:STAT WHERE BODYCODE=:BODYCODE AND STAT=6 AND S=:S AND Y=1";
        //    }

        //    if (bodyCode == "****")
        //    {
        //        sql = "UPDATE SSC_LAGER SET STAT=:STAT WHERE TOPCOAT=:TOPCOAT AND STAT=6 AND S=:S AND Y=1";
        //    }

        //    try
        //    {
        //        using (var connection = new OracleConnection(DBUtil.ConnectionString))
        //        {
        //            if (connection.State == ConnectionState.Closed)
        //            {
        //                connection.Open();
        //            }

        //            using (IDbCommand command = connection.CreateCommand())
        //            {
        //                command.Transaction = connection.BeginTransaction();
        //                command.CommandText = sql;

        //                DBUtil.SetParam(command, "STAT", DbType.Int32, state);
        //                if (!bodyCode.Equals("****"))
        //                {
        //                    DBUtil.SetParam(command, "BODYCODE", DbType.String, bodyCode);
        //                }

        //                if (!topcoat.Equals("****"))
        //                {
        //                    DBUtil.SetParam(command, "TOPCOAT", DbType.String, topcoat);
        //                }

        //                DBUtil.SetParam(command, "S", DbType.Int32, section);

        //                int result = command.ExecuteNonQuery();
        //                command.Transaction.Commit();
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        logger.Error("update state failed, {0}", e.ToString());
        //    }
        //}
        //@@@@@@@@@@@@@
        public void ClearLockState( int line, int y, int state, int section)
        {
            string sql = "UPDATE SSC_LAGER SET STAT=:STAT WHERE LINE=:LINE AND STAT=6 AND S=:S AND Y=：Y";


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

                        DBUtil.SetParam(command, "STAT", DbType.Int32, state);
                        DBUtil.SetParam(command, "LINE", DbType.Int32, line);
                        DBUtil.SetParam(command, "Y", DbType.Int32, y);
                        DBUtil.SetParam(command, "S", DbType.Int32, section);

                        int result = command.ExecuteNonQuery();
                        command.Transaction.Commit();
                    }
                }
            }
            catch (Exception e)
            {
                logger.Error("update state failed, {0}", e.ToString());
            }
        }

        public void UpdateSection(int fach, int s)
        {
            string sql = "UPDATE SSC_LAGER SET S=:S WHERE FACH=:FACH";

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

                        DBUtil.SetParam(command, "S", DbType.Int32, s);
                        DBUtil.SetParam(command, "FACH", DbType.Int32, fach);

                        int result = command.ExecuteNonQuery();
                        command.Transaction.Commit();

                        logger.Info("update sction 200, FACH IS {0}, sction IS {1}", fach, s);
                    }
                }
            }
            catch (Exception e)
            {
                logger.Error("update state failed, {0}", e.ToString());
            }
        }


        public void UpdateState(int fach, int state)
        {
            string sql = "UPDATE SSC_LAGER SET STAT=:STAT WHERE FACH=:FACH";

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

                        DBUtil.SetParam(command, "STAT", DbType.Int32, state);
                        DBUtil.SetParam(command, "FACH", DbType.Int32, fach);

                        int result = command.ExecuteNonQuery();
                        command.Transaction.Commit();

                        logger.Info("update state 200, FACH IS {0}, STATE IS {1}", fach, state);
                    }
                }
            }
            catch (Exception e)
            {
                logger.Error("update state failed, {0}", e.ToString());
            }
        }
        public void UpdateTime(int fach, DateTime time)
        {
            string sql = "UPDATE SSC_LAGER SET VON=:VON, INTIME=:INTIME WHERE FACH=:FACH";

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

                        DBUtil.SetParam(command, "VON", DbType.DateTime, time);
                        DBUtil.SetParam(command, "INTIME", DbType.DateTime, time);
                        DBUtil.SetParam(command, "FACH", DbType.Int32, fach);

                        int result = command.ExecuteNonQuery();
                        command.Transaction.Commit();
                    }
                }
            }
            catch (Exception e)
            {
                logger.Error("update time failed, {0}", e.ToString());
            }
        }
        public void ClearFollowState(string color)
        {
            string sql = "UPDATE SSC_LAGER SET STAT=1 WHERE TOPCOAT=:TOPCOAT AND Y = 1 AND STAT = 7";

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

                        DBUtil.SetParam(command, "TOPCOAT", DbType.String, color);

                        int result = command.ExecuteNonQuery();
                        command.Transaction.Commit();
                    }
                }
            }
            catch (Exception e)
            {
                logger.Error("update state failed, {0}", e.ToString());
            }
        }

        public void ClearFollowState()
        {
            string sql = "update SSC_LAGER set STAT=1 where STAT=7";

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

                        command.ExecuteNonQuery();
                        command.Transaction.Commit();
                        logger.Info("clear follow state 200");
                    }
                }
            }
            catch (Exception e)
            {
                logger.Error("clear follow state failed, {0}", e.ToString());
            }
        }

        public void Create(SSCLager lager)
        {
            string sql = "insert into SSC_LAGER(FACH,S,L,Y,VON,STAT,KENN,LINE,SKID,TOPCOAT,SPECIAL,BODYCODE,INTIME) values(:FACH,:S,:L,:Y,:VON,:STAT,:KENN,:LINE,:SKID,:TOPCOAT,:SPECIAL,:BODYCODE,:INTIME)";

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

                        DBUtil.SetParam(command, "FACH", DbType.Int32, lager.FACH);
                        DBUtil.SetParam(command, "S", DbType.Int32, lager.S);
                        DBUtil.SetParam(command, "L", DbType.Int32, lager.L);
                        DBUtil.SetParam(command, "Y", DbType.Int32, lager.Y);
                        DBUtil.SetParam(command, "VON", DbType.DateTime, lager.VON);
                        DBUtil.SetParam(command, "STAT", DbType.Int32, lager.STAT);
                        DBUtil.SetParam(command, "KENN", DbType.Int32, lager.KENN);
                        DBUtil.SetParam(command, "LINE", DbType.Int32, lager.LINE);
                        DBUtil.SetParam(command, "SKID", DbType.Int32, lager.SKID);
                        DBUtil.SetParam(command, "TOPCOAT", DbType.String, lager.TOPCOAT);
                        DBUtil.SetParam(command, "SPECIAL", DbType.String, lager.SPECIAL);
                        DBUtil.SetParam(command, "BODYCODE", DbType.String, lager.BODYCODE);
                        DBUtil.SetParam(command, "INTIME", DbType.DateTime, lager.InTime);

                        command.ExecuteNonQuery();
                        command.Transaction.Commit();

                        logger.Info("create ssc lager 200, FACH IS {0}", lager.FACH);
                    }
                }
            }
            catch (Exception e)
            {
                logger.Error("create ssclager failed, {0}", e.ToString());
            }
        }

        public SSCLager Conver(OPCContract contract)
        {
            try
            {
                SSCLager result = new SSCLager();
                if (contract.Section < 4)
                {
                    result.S = contract.Section;
                }
                else
                {
                    result.S = contract.Section - 48;
                }
                result.L = contract.L;
                result.Y = contract.Y;
                result.SPECIAL = contract.Spec_ID;
                result.SKID = int.Parse(contract.Skid_Nr);
                result.BODYCODE = contract.Body;
                result.KENN = int.Parse(contract.Body_ID);
                result.STAT = contract.State;
                result.TOPCOAT = contract.color;
                //*****
                if (contract.InTime != null)
                {
                    result.VON = contract.InTime.Value;
                }
                else
                {
                    result.VON = DateTime.Now;
                }
                result.LINE = contract.Line;
                if (contract.InTime != null)
                {
                    result.InTime = contract.InTime.Value;
                }
                else
                {
                    result.InTime = DateTime.Now;
                }
                // result.VON = DateTime.Now;
                //result.LINE = contract.Line;
                //result.InTime = DateTime.Now;

                if (!String.IsNullOrEmpty(contract.FACH))
                {
                    result.FACH = int.Parse(contract.FACH);
                }
                else
                {
                    result.FACH = int.Parse(result.S + contract.L.ToString() + contract.Y.ToString("00"));
                }

                return result;
            }
            catch (Exception e)
            {
                logger.Error("convert error, {0}", e.ToString());
            }

            return null;
        }

        public OPCContract ConverToContract(SSCLager lager)
        {
            try
            {
                OPCContract result = new OPCContract();
                result.Section = lager.S;
                result.L = lager.L;
                result.Y = lager.Y;
                result.Spec_ID = lager.SPECIAL;
                result.Skid_Nr = lager.SKID.ToString("0000");
                result.Body = lager.BODYCODE;
                result.Body_ID = lager.KENN.ToString("00000000");
                result.State = lager.STAT;
                result.color = lager.TOPCOAT;
                result.Line = lager.LINE;
                result.FACH = lager.FACH.ToString();
                result.To_Des = lager.S == lager.LINE ? "3715" : "4190";
                result.To_Src = lager.FACH.ToString();
                result.SpotFrom = lager.FACH.ToString();
                return result;
            }
            catch (Exception e)
            {
                logger.Error("convert error, {0}", e.ToString());
            }

            return null;
        }
        public List<SSCLager> GetLockCars(String topcoat, String bodyCode, int state, int section)
        {
            string sql = "";

            //@@
            if((topcoat == "x") && (bodyCode == "x"))
            {
                sql = "SELECT FACH, S, L, Y, VON, STAT, KENN, LINE,  SKID, TOPCOAT, SPECIAL, BODYCODE FROM SSC_LAGER WHERE S=:S AND Y=1  AND STAT=6";
            }
            else if (topcoat != "****" && bodyCode != "****")
            {
                sql = "SELECT FACH, S, L, Y, VON, STAT, KENN, LINE,  SKID, TOPCOAT, SPECIAL, BODYCODE FROM SSC_LAGER WHERE BODYCODE=:BODYCODE AND TOPCOAT=:TOPCOAT AND S=:S AND Y=1  AND STAT=6";
            }
            else if (topcoat == "****")
            {
                sql = "SELECT FACH, S, L, Y, VON, STAT, KENN, LINE,  SKID, TOPCOAT, SPECIAL, BODYCODE FROM SSC_LAGER WHERE BODYCODE=:BODYCODE AND S=:S AND Y=1  AND STAT=6";
            }
            else
            {
                sql = "SELECT FACH, S, L, Y, VON, STAT, KENN, LINE,  SKID, TOPCOAT, SPECIAL, BODYCODE FROM SSC_LAGER WHERE  TOPCOAT=:TOPCOAT AND S=:S AND Y=1  AND STAT=6";
            }

            List<SSCLager> result = new List<SSCLager>();
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

                        //DBUtil.SetParam(command, "STAT", DbType.Int32, state);

                        if (bodyCode != "****")
                        {
                            DBUtil.SetParam(command, "BODYCODE", DbType.String, bodyCode);
                        }
                        if (topcoat != "****")
                        {
                            DBUtil.SetParam(command, "TOPCOAT", DbType.String, topcoat);
                        }
                        DBUtil.SetParam(command, "S", DbType.Int32, section);

                        using (var reader = command.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                SSCLager lager = new SSCLager();
                                lager.FACH = DBUtil.GetInt(reader, 0);
                                lager.S = DBUtil.GetInt(reader, 1);
                                lager.L = DBUtil.GetInt(reader, 2);
                                lager.Y = DBUtil.GetInt(reader, 3);
                                lager.VON = reader.GetDateTime(4);
                                lager.STAT = DBUtil.GetInt(reader, 5);
                                lager.KENN = DBUtil.GetInt(reader, 6);
                                lager.LINE = DBUtil.GetInt(reader, 7);
                                lager.SKID = DBUtil.GetInt(reader, 8);
                                lager.TOPCOAT = DBUtil.GetString(reader, 9);
                                lager.SPECIAL = DBUtil.GetString(reader, 10);
                                lager.BODYCODE = DBUtil.GetString(reader, 11);
                                result.Add(lager);
                            }
                        }

                        command.Transaction.Commit();
                    }
                }
            }
            catch (Exception e)
            {
                logger.Error("get ssclager failed, {0}", e.ToString());
            }

            return result;
        }
        public int GetManCars(int section)
        {
            //@@
            string sql = "SELECT COUNT(*) FROM SSC_LAGER WHERE STAT=4 AND S=:S AND Y=1";

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
                        DBUtil.SetParam(command, "S", DbType.Int32, section);
                        using (var reader = command.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                try
                                {
                                    return DBUtil.GetInt(reader, 0);
                                }
                                catch (Exception e)
                                {
                                    logger.Error("get outing count failed, {0}", e.ToString());
                                }
                            }
                        }
                        command.Transaction.Commit();

                    }
                }
            }
            catch (Exception e)
            {
                logger.Error("get outing count failed, {0}", e.ToString());
            }

            return 0;
        }
    }
}
