using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Simple.Data.Extensions;

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

        public static object ToScalar(this IDbCommand command)
        {
            var result = command.ExecuteScalar();
            return result == DBNull.Value ? null : result;
        }

        public static void AddParameter(this IDbCommand command, KeyValuePair<string, object> parameterData)
        {
            var parameter = command.CreateParameter();
            parameter.ParameterName = parameterData.Key;
            parameter.Value = parameterData.Value ?? DBNull.Value;
            command.Parameters.Add(parameter);
        }

        public static void AddParameters(this IDbCommand command, params KeyValuePair<string, object>[] parametersData)
        {
            command.AddParameters(parametersData.ToDictionary());
        }

        public static void AddParameters(this IDbCommand command, object parametersData)
        {
            command.AddParameters(parametersData.ObjectToDictionary());
        }

        public static void AddParameters(this IDbCommand command, IDictionary<string, object> parametersData)
        {
            parametersData.ToList().ForEach(command.AddParameter);
        }
    }
}
