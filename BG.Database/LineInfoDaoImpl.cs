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
    public class LineInfoDaoImpl
    {
        private BGLog logger = BGLog.GetLogger(typeof(LineInfoDaoImpl));

        public void UpdateLineState(int s, int l, Boolean lineIn, int value)
        {
            String sql = "UPDATE DEF_LANE SET ";

            if (lineIn)
            {
                sql += "LOCK_ROLL_IN=:LOCK_ROLL_IN ";
            }
            else
            {
                sql += "LOCK_ROLL_IN=:LOCK_ROLL_OUT ";
            }

            sql += "WHERE S=:S AND L=:L";

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

                        if (lineIn)
                        {
                            DBUtil.SetParam(command, "LOCK_ROLL_IN", DbType.Int32, value);
                        }
                        else
                        {
                            DBUtil.SetParam(command, "LOCK_ROLL_OUT", DbType.Int32, value);
                        }

                        DBUtil.SetParam(command, "S", DbType.Int32, s);
                        DBUtil.SetParam(command, "L", DbType.Int32, l);

                        command.ExecuteNonQuery();

                        command.Transaction.Commit();
                    }
                }
            }
            catch (Exception e)
            {
                logger.Error("Update line state failed, {0}", e.ToString());
            }
        }

        public void UpdateLineTopcoat(String topcoat, int s, int l)
        {
            String sql = "UPDATE DEF_LANE SET TOPCOAT = :TOPCOAT WHERE (S = :S) AND (L = :L)";

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

                        DBUtil.SetParam(command, "TOPCOAT", DbType.String, topcoat);
                        DBUtil.SetParam(command, "S", DbType.Int32, s);
                        DBUtil.SetParam(command, "L", DbType.Int32, l);

                        command.ExecuteNonQuery();
                        command.Transaction.Commit();
                    }
                }
            }
            catch (Exception e)
            {
                logger.Error("Update line topcoat failed, {0}", e.ToString());
            }
        }
        public void UpdateLine(LineInfo line, int s, int l)
        {
            String sql = "UPDATE DEF_LANE SET TOPCOAT = :TOPCOAT , ROLE=:ROLE,ID_CONDITION=:ID_CONDITION WHERE (S = :S) AND (L = :L)";

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

                        DBUtil.SetParam(command, "TOPCOAT", DbType.String, line.Color);
                        //DBUtil.SetParam(command, "TOPCOAT", DbType.String, line.Body);
                        DBUtil.SetParam(command, "ROLE", DbType.String, line.Role);
                        DBUtil.SetParam(command, "ID_CONDITION", DbType.Int32, line.Codition);
                        DBUtil.SetParam(command, "S", DbType.Int32, s);
                        DBUtil.SetParam(command, "L", DbType.Int32, l);

                        command.ExecuteNonQuery();
                        command.Transaction.Commit();
                    }
                }
            }
            catch (Exception e)
            {
                logger.Error("Update line topcoat failed, {0}", e.ToString());
            }
        }
        public List<LineInfo> GetAllLines()
        {
            List<LineInfo> result = new List<LineInfo>();

            string sql = "SELECT \n  S, L, LOCK_ROLL_IN, LOCK_ROLL_OUT, ROLE, TOPCOAT, ID_CONDITION \nFROM DEF_LANE ";

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
                                LineInfo line = new LineInfo();
                                line.Section = DBUtil.GetInt(reader,0);
                                line.Line = DBUtil.GetInt(reader,1);
                                line.LockType = 0;

                                if (DBUtil.GetInt(reader,2) == 1)
                                {
                                    line.LockType = 1;
                                }

                                if (DBUtil.GetInt(reader,3) == 1)
                                {
                                    if (line.LockType == 1)
                                    {
                                        line.LockType = 3;
                                    }
                                    else
                                    {
                                        line.LockType = 2;
                                    }
                                }

                                line.Role = DBUtil.GetString(reader,4);
                                line.Color = DBUtil.GetString(reader,5);
                                line.Codition = DBUtil.GetInt(reader,6);

                                result.Add(line);
                            }
                        }

                        command.Transaction.Commit();

                        logger.Info("Get all line 200");
                    }

                    return result;
                }
            }
            catch (Exception e)
            {
                logger.Error("Get all line failed, {0}", e.ToString());
            }

            return result;
        }

        public LineInfo GetLine(int s, int l)
        {
            List<LineInfo> result = new List<LineInfo>();

            string sql = "SELECT \n  S, L, LOCK_ROLL_IN, LOCK_ROLL_OUT, ROLE, TOPCOAT, ID_CONDITION \nFROM DEF_LANE WHERE S=:S AND L = :L";

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
                        DBUtil.SetParam(command, "S", DbType.Int16, s);
                        DBUtil.SetParam(command, "L", DbType.Int16, l);

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                LineInfo line = new LineInfo();
                                line.Section = DBUtil.GetInt(reader,0);
                                line.Line = DBUtil.GetInt(reader,1);
                                line.LockType = 0;

                                if (DBUtil.GetInt(reader,2) == 1)
                                {
                                    line.LockType = 1;
                                }

                                if (DBUtil.GetInt(reader,3) == 1)
                                {
                                    if (line.LockType == 1)
                                    {
                                        line.LockType = 3;
                                    }
                                    else
                                    {
                                        line.LockType = 2;
                                    }
                                }

                                line.Role = DBUtil.GetString(reader,4);
                                line.Color = DBUtil.GetString(reader,5);
                                line.Codition = DBUtil.GetInt(reader,6);

                                return line;
                            }
                        }

                        command.Transaction.Commit();

                        logger.Info("Get all unlcok out line 200");
                    }
                }
            }
            catch (Exception e)
            {
                logger.Error("Get all unlcok out line failed, {0}", e.ToString());
            }

            return null;
        }

        public List<LineInfo> GetAllUnlockOutLines(int s)
        {
            List<LineInfo> result = new List<LineInfo>();

            string sql = "SELECT \n  S, L, LOCK_ROLL_IN, LOCK_ROLL_OUT, ROLE, TOPCOAT, ID_CONDITION \nFROM DEF_LANE WHERE LOCK_ROLL_OUT=0 AND L <> 7";

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
                        DBUtil.SetParam(command, "S", DbType.Int16, s);

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                LineInfo line = new LineInfo();
                                line.Section = DBUtil.GetInt(reader,0);
                                line.Line = DBUtil.GetInt(reader,1);
                                line.LockType = 0;

                                if (DBUtil.GetInt(reader,2) == 1)
                                {
                                    line.LockType = 1;
                                }

                                if (DBUtil.GetInt(reader,3) == 1)
                                {
                                    if (line.LockType == 1)
                                    {
                                        line.LockType = 3;
                                    }
                                    else
                                    {
                                        line.LockType = 2;
                                    }
                                }

                                line.Role = DBUtil.GetString(reader,4);
                                line.Color = DBUtil.GetString(reader,5);
                                line.Codition = DBUtil.GetInt(reader,6);

                                result.Add(line);
                            }
                        }

                        command.Transaction.Commit();

                        logger.Info("Get all unlcok out line 200");
                    }

                    return result;
                }
            }
            catch (Exception e)
            {
                logger.Error("Get all unlcok out line failed, {0}", e.ToString());
            }

            return result;
        }


        public List<LineInfo> GetLockedInLines()
        {
            List<LineInfo> result = new List<LineInfo>();

            string sql = "SELECT  DL.S, DL.L, DL.ROLE,  DL.LOCK_ROLL_IN, DL.LOCK_ROLL_OUT,  DL.TOPCOAT, DL.ID_CONDITION, DC.TOPCOAT, DC.BODYCODE FROM DEF_LANE DL, DEF_CONDITION_DEFINITION DC WHERE DL.LOCK_ROLL_IN=1 AND DL.ID_CONDITION = DC.ID_CONDITION(+)";

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
                                LineInfo line = new LineInfo();
                                line.Section = DBUtil.GetInt(reader,0);
                                line.Line = DBUtil.GetInt(reader,1);
                                line.Role = DBUtil.GetString(reader,2);
                                line.Color = DBUtil.GetString(reader,5);
                                line.Codition = DBUtil.GetInt(reader,6);

                                if (line.Codition != 0)
                                {
                                    line.Color = DBUtil.GetString(reader,7);
                                    line.Body = DBUtil.GetString(reader,8);
                                }

                                result.Add(line);
                            }
                        }

                        command.Transaction.Commit();

                        logger.Info("Get all in-unlock line 200");
                    }

                    return result;
                }
            }
            catch (Exception e)
            {
                logger.Error("Get all in-unlock  line failed, {0}", e.ToString());
            }

            return result;
        }

        public List<LineInfo> GetUnLockInLines()
        {
            List<LineInfo> result = new List<LineInfo>();

            string sql = "SELECT  DL.S, DL.L, DL.ROLE,  DL.LOCK_ROLL_IN, DL.LOCK_ROLL_OUT,  DL.TOPCOAT, DL.ID_CONDITION, DC.TOPCOAT, DC.BODYCODE, DC.SPECIAL FROM DEF_LANE DL, DEF_CONDITION_DEFINITION DC WHERE DL.LOCK_ROLL_IN=0 AND DL.L <> 7 AND DL.ID_CONDITION = DC.ID_CONDITION(+)";

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
                                LineInfo line = new LineInfo();
                                line.Section = DBUtil.GetInt(reader,0);
                                line.Line = DBUtil.GetInt(reader,1);
                                line.Role = DBUtil.GetString(reader,2);
                                line.Color = DBUtil.GetString(reader,5);
                                line.Codition = DBUtil.GetInt(reader,6);

                                if (line.Codition != 0)
                                {
                                    line.Color = DBUtil.GetString(reader,7);
                                    line.Body = DBUtil.GetString(reader,8);
                                    line.Spic = DBUtil.GetString(reader, 9);
                                }

                                result.Add(line);
                            }
                        }

                        command.Transaction.Commit();

                        logger.Info("Get all in-unlock line 200");
                    }

                    return result;
                }
            }
            catch (Exception e)
            {
                logger.Error("Get all in-unlock  line failed, {0}", e.ToString());
            }

            return result;
        }
    }
}
