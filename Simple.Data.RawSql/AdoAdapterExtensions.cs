using System;
using System.Collections.Generic;
using System.Data;
using Simple.Data.Ado;
using Simple.Data.Extensions;

namespace Simple.Data.RawSql
{
    public static class AdoAdapterExtensions
    {
        public static IEnumerable<IEnumerable<dynamic>> ToResultSets(
            this AdoAdapter adapter,
            string sql,
            IDictionary<string, object> parameters)
        {
            if (!adapter.ConnectionProvider.SupportsCompoundStatements)
                throw new NotSupportedException(string.Format("{0} does not support compound statements",
                                                              adapter.GetType()));
            return adapter.GetConnection().ToResultSets(sql, parameters, adapter.AdoOptions);
        }

        public static IEnumerable<IEnumerable<dynamic>> ToResultSets(this AdoAdapter adapter, string sql,
                                                                     params KeyValuePair<string, object>[] parameters)
        {
            return adapter.ToResultSets(sql, parameters.ToDictionary());
        }

        public static IEnumerable<IEnumerable<dynamic>> ToResultSets(this AdoAdapter adapter, string sql,
                                                                     object parameters)
        {
            return adapter.ToResultSets(sql, parameters.ObjectToDictionary());
        }

        public static IEnumerable<dynamic> ToRows(this AdoAdapter adapter, string sql,
                                                  IDictionary<string, object> parameters)
        {
            return adapter.GetConnection().ToRows(sql, parameters, adapter.AdoOptions);
        }

        public static IEnumerable<dynamic> ToRows(this AdoAdapter adapter, string sql,
                                                  params KeyValuePair<string, object>[] parameters)
        {
            return adapter.ToRows(sql, parameters.ToDictionary());
        }

        public static IEnumerable<dynamic> ToRows(this AdoAdapter adapter, string sql, object parameters)
        {
            return adapter.ToRows(sql, parameters.ObjectToDictionary());
        }

        public static dynamic ToRow(this AdoAdapter adapter, string sql, IDictionary<string, object> parameters)
        {
            return adapter.GetConnection().ToRow(sql, parameters, adapter.AdoOptions);
        }

        public static dynamic ToRow(this AdoAdapter adapter, string sql,
                                    params KeyValuePair<string, object>[] parameters)
        {
            return adapter.ToRow(sql, parameters.ToDictionary());
        }

        public static dynamic ToRow(this AdoAdapter adapter, string sql, object parameters)
        {
            return adapter.ToRow(sql, parameters.ObjectToDictionary());
        }

        public static object ToScalar(this AdoAdapter adapter, string sql, IDictionary<string, object> parameters)
        {
            return adapter.GetConnection().ToScalar(sql, parameters, adapter.AdoOptions);
        }

        public static object ToScalar(this AdoAdapter adapter, string sql, 
            params KeyValuePair<string, object>[] parameters)
        {
            return adapter.ToScalar(sql, parameters.ToDictionary());
        }
        
        public static object ToScalar(this AdoAdapter adapter, string sql, object parameters)
        {
            return adapter.ToScalar(sql, parameters.ObjectToDictionary());
        }

        public static int ExecuteNonQuery(this AdoAdapter adapter, string sql, IDictionary<string, object> parameters)
        {
            return adapter.GetConnection().Execute(sql, parameters, adapter.AdoOptions);
        }

        public static int ExecuteNonQuery(this AdoAdapter adapter, string sql,
            params KeyValuePair<string, object>[] parameters)
        {
            return adapter.ExecuteNonQuery(sql, parameters.ToDictionary());
        }

        public static int ExecuteNonQuery(this AdoAdapter adapter, string sql, object parameters)
        {
            return adapter.ExecuteNonQuery(sql, parameters.ObjectToDictionary());
        }

        private static IDbConnection GetConnection(this AdoAdapter adapter)
        {
            return adapter.CreateConnection();
        }
    }
}
