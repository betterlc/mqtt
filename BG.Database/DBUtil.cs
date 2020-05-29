
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BG.Utilities;

namespace BG.Database
{
    public class DBUtil
    {
        public static String ConnectionString = Configurations.Get("ConnectionString");

        public static IDbDataParameter SetParam(IDbCommand inCommand, string inParamName, DbType inType, object inValue)
        {
            IDbDataParameter parameter;
            int index = inCommand.Parameters.IndexOf(inParamName);
            if (index < 0)
            {
                parameter = inCommand.CreateParameter();
                inCommand.Parameters.Add(parameter);
                parameter.ParameterName = inParamName;
                parameter.DbType = inType;
            }
            else
            {
                parameter = (IDbDataParameter)inCommand.Parameters[index];
            }
            parameter.Value = inValue;
            parameter.Direction = ParameterDirection.Output;
            return parameter;
        }

        public static String GetString(IDataReader reader, int index)
        {
            if (reader.IsDBNull(index))
            {
                return "";
            }

            return reader.GetString(index);
        }

        public static DateTime GetDateTime(IDataReader reader, int index)
        {
            if (reader.IsDBNull(index))
            {
                return new DateTime(0L);
            }
            return reader.GetDateTime(index);
        }

        public static int GetInt(IDataReader reader, int index)
        {
            if (reader.IsDBNull(index))
            {
                return 0;
            }

            return reader.GetInt32(index);
        }
    }
}
