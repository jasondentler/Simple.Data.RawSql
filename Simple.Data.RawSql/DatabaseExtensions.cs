﻿using System;
using System.Collections.Generic;
using Simple.Data.Ado;
using Simple.Data.Extensions;

namespace Simple.Data.RawSql
{
    public static class DatabaseExtensions
    {
        public static IEnumerable<IEnumerable<dynamic>> ToResultSets(
            this Database db,
            string sql,
            IDictionary<string, object> parameters)
        {
            return db.GetAdoAdapter().ToResultSets(sql, parameters);
        }

        public static IEnumerable<IEnumerable<dynamic>> ToResultSets(this Database db, string sql,
                                                                     params KeyValuePair<string, object>[] parameters)
        {
            return db.ToResultSets(sql, parameters.ToDictionary());
        }

        public static IEnumerable<IEnumerable<dynamic>> ToResultSets(this Database db, string sql,
                                                                     object parameters)
        {
            return db.ToResultSets(sql, parameters.ObjectToDictionary());
        }

        public static IEnumerable<dynamic> ToRows(this Database db, string sql,
                                                  IDictionary<string, object> parameters)
        {
            return new SimpleList(db.GetAdoAdapter().ToRows(sql, parameters));
        }

        public static IEnumerable<dynamic> ToRows(this Database db, string sql,
                                                  params KeyValuePair<string, object>[] parameters)
        {
            return db.ToRows(sql, parameters.ToDictionary());
        }

        public static IEnumerable<dynamic> ToRows(this Database db, string sql, object parameters)
        {
            return db.ToRows(sql, parameters.ObjectToDictionary());
        }

        public static dynamic ToRow(this Database db, string sql, IDictionary<string, object> parameters)
        {
            return db.GetAdoAdapter().ToRow(sql, parameters);
        }

        public static dynamic ToRow(this Database db, string sql,
                                    params KeyValuePair<string, object>[] parameters)
        {
            return db.ToRow(sql, parameters.ToDictionary());
        }

        public static dynamic ToRow(this Database db, string sql, object parameters)
        {
            return db.ToRow(sql, parameters.ObjectToDictionary());
        }

        public static object ToScalar(this Database db, string sql, IDictionary<string, object> parameters)
        {
            return db.GetAdoAdapter().ToScalar(sql, parameters);
        }

        public static object ToScalar(this Database db, string sql,
            params KeyValuePair<string, object>[] parameters)
        {
            return db.GetAdoAdapter().ToScalar(sql, parameters.ToDictionary());
        }

        public static object ToScalar(this Database db, string sql, object parameters)
        {
            return db.GetAdoAdapter().ToScalar(sql, parameters.ObjectToDictionary());
        }

        public static int Execute(this Database db, string sql, IDictionary<string, object> parameters)
        {
            return db.GetAdoAdapter().ExecuteNonQuery(sql, parameters);
        }

        public static int Execute(this Database db, string sql,
            params KeyValuePair<string, object>[] parameters)
        {
            return db.GetAdoAdapter().ExecuteNonQuery(sql, parameters.ToDictionary());
        }

        public static int Execute(this Database db, string sql, object parameters)
        {
            return db.GetAdoAdapter().ExecuteNonQuery(sql, parameters.ObjectToDictionary());
        }

        private static AdoAdapter GetAdoAdapter(this Database db)
        {
            var adapter = db.GetAdapter();
            var adoAdapter = adapter as AdoAdapter;
            if (adoAdapter == null)
                throw new NotSupportedException("Only Simple.Data.Ado adapters are supported by Simple.Data.RawSql");
            return adoAdapter;
        }
    }
}
