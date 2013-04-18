using System.Collections.Generic;
using System.Data;
using System.Linq;
using Simple.Data.Ado;

namespace Simple.Data.RawSql
{
    public static class DataReaderExtensions
    {
        public static IEnumerable<IEnumerable<dynamic>> ToResultSets(this IDataReader reader)
        {
            return reader.ToMultipleDictionaries().ToResultSets();
        }

        public static IEnumerable<dynamic> ToRows(this IDataReader reader)
        {
            return reader.ToDictionaries().ToRows();
        }

        public static dynamic ToRow(this IDataReader reader)
        {
            return reader.Read() ? ((IDataRecord) reader).ToRow() : null;
        }

        public static dynamic ToRow(this IDataRecord record)
        {
            return record.ToDictionary().ToRow();
        }

        public static object ToScalar(this IDataReader reader)
        {
            return reader.Read() ? ((IDataRecord) reader).ToScalar() : null;
        }

        public static object ToScalar(this IDataRecord record)
        {
            return record.ToDictionary().Single().Value;
        }

        private static IEnumerable<IEnumerable<dynamic>> ToResultSets(this IEnumerable<IEnumerable<IDictionary<string, object>>> data)
        {
            return data.Select(ToRows).ToArray().AsEnumerable();
        }

        private static IEnumerable<dynamic> ToRows(this IEnumerable<IDictionary<string, object>> rsData)
        {
            return new SimpleList(rsData.Select(ToRow).ToArray().AsEnumerable());
        }

        private static SimpleRecord ToRow(this IDictionary<string, object> rowData)
        {
            return new SimpleRecord(rowData);
        }
    }
}
