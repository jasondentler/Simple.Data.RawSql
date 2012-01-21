using System;
using System.Collections.Generic;
using System.Data;

namespace Simple.Data.RawSql
{
    public static class DbCommandExtensions
    {
        public static IEnumerable<IEnumerable<dynamic>> ToResultSets(this IDbCommand command)
        {
            using (var rdr = command.ExecuteReader())
            {
                return rdr.ToResultSets();
            }
        }

        public static IEnumerable<dynamic> ToRows(this IDbCommand command)
        {
            using (var rdr = command.ExecuteReader())
            {
                return rdr.ToRows();
            }
        }

        public static dynamic ToRow(this IDbCommand command)
        {
            using (var rdr = command.ExecuteReader())
            {
                return rdr.ToRow();
            }
        }

        public static void AddParameter(this IDbCommand command, KeyValuePair<string, object> parameterData)
        {
            var parameter = command.CreateParameter();
            parameter.ParameterName = parameterData.Key;
            parameter.Value = parameterData.Value ?? DBNull.Value;
            command.Parameters.Add(parameter);
        }

    }
}
