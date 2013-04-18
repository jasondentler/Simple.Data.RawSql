using System.Collections.Generic;
using System.Data;
using System.Linq;
using Simple.Data.Extensions;

namespace Simple.Data.RawSql
{
    public class DbCommandBuilder
    {
        public IDbCommand BuildCommand(IDbConnection connection, string sql, object parameters)
        {
            return BuildCommand(connection, sql, parameters.ObjectToDictionary());
        }

        public IDbCommand BuildCommand(IDbConnection connection, string sql, params KeyValuePair<string, object>[] parameters)
        {
            return BuildCommand(connection, sql, parameters.ToDictionary());
        }

        public IDbCommand BuildCommand(IDbConnection connection, string sql, IEnumerable<KeyValuePair<string, object>> parameters)
        {
            return BuildCommand(connection, sql,
                                parameters.ToDictionary());
        }

        public IDbCommand BuildCommand(IDbConnection connection, string sql, IDictionary<string, object> parameters)
        {
            var cmd = connection.CreateCommand();
            cmd.Connection = connection;
            cmd.CommandText = sql;

            parameters.ToList()
                .ForEach(cmd.AddParameter);

            return cmd;
        }
    }
}
