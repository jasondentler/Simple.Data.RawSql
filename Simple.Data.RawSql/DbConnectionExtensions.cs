using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Simple.Data.Extensions;

namespace Simple.Data.RawSql
{
    public static class DbConnectionExtensions
    {

        private const string SqlEmptyOrWhitespace = "Sql statement can't be empty or whitespace.";

        public static IEnumerable<IEnumerable<dynamic>> ToResultSets(this IDbConnection connection, string sql,
                                                                    IDictionary<string, object> parameters)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            if (sql == null) throw new ArgumentNullException("sql");
            if (string.IsNullOrWhiteSpace(sql))
                throw new ArgumentException(SqlEmptyOrWhitespace, "sql");

            parameters = parameters ?? new Dictionary<string, object>();
            var cmd = new DbCommandBuilder().BuildCommand(connection, sql, parameters);
            return connection.WithOpenConnection(() => cmd.ToResultSets());
        }

        public static IEnumerable<IEnumerable<dynamic>> ToResultSets(this IDbConnection connection, string sql,
                                                                    params KeyValuePair<string, object>[] parameters)
        {
            parameters = parameters ?? new KeyValuePair<string, object>[0];
            return connection.ToResultSets(sql, parameters.ToDictionary());
        }

        public static IEnumerable<IEnumerable<dynamic>> ToResultSets(this IDbConnection connection, string sql, object parameters)
        {
            return connection.ToResultSets(sql, parameters.ObjectToDictionary());
        }

        public static IEnumerable<dynamic> ToRows(this IDbConnection connection, string sql,
                                                  IDictionary<string, object> parameters)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            if (sql == null) throw new ArgumentNullException("sql");
            if (string.IsNullOrWhiteSpace(sql))
                throw new ArgumentException(SqlEmptyOrWhitespace, "sql");

            parameters = parameters ?? new Dictionary<string, object>();
            var cmd = new DbCommandBuilder().BuildCommand(connection, sql, parameters);
            return connection.WithOpenConnection(() => cmd.ToRows());
        }

        public static IEnumerable<dynamic> ToRows(this IDbConnection connection, string sql,
                                                  params KeyValuePair<string, object>[] parameters)
        {
            parameters = parameters ?? new KeyValuePair<string, object>[0];
            return connection.ToRows(sql, parameters.ToDictionary());
        }

        public static IEnumerable<dynamic> ToRows(this IDbConnection connection, string sql, object parameters)
        {
            return connection.ToRows(sql, parameters.ObjectToDictionary());
        }

        public static dynamic ToRow(this IDbConnection connection, string sql, IDictionary<string, object> parameters)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            if (sql == null) throw new ArgumentNullException("sql");
            if (string.IsNullOrWhiteSpace(sql))
                throw new ArgumentException(SqlEmptyOrWhitespace, "sql");

            parameters = parameters ?? new Dictionary<string, object>();
            var cmd = new DbCommandBuilder().BuildCommand(connection, sql, parameters);
            return connection.WithOpenConnection(() => cmd.ToRow());
        }

        public static dynamic ToRow(this IDbConnection connection, string sql,
                                    params KeyValuePair<string, object>[] parameters)
        {
            parameters = parameters ?? new KeyValuePair<string, object>[0];
            return connection.ToRow(sql, parameters.ToDictionary());
        }

        public static dynamic ToRow(this IDbConnection connection, string sql, object parameters)
        {
            parameters = parameters ?? new KeyValuePair<string, object>[0];
            return connection.ToRow(sql, parameters.ObjectToDictionary());
        }

        private static T WithOpenConnection<T>(this IDbConnection connection, Func<T> func)
        {
            if (connection.State != ConnectionState.Closed)
                return func();

            try
            {
                connection.Open();
                return func();
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
