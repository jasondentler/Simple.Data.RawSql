using System;
using System.Linq;
using Simple.Data;
using Simple.Data.RawSql;

namespace Runner
{
    class Program
    {
        static void Main(string[] args)
        {

            MultipleResultSets();
            SingleResultSet();
            SingleRow();

            Console.WriteLine();
            Console.WriteLine("Press any key to quit");
            Console.ReadKey();
        }

        private static void MultipleResultSets()
        {

            const string sql = @"
                -- Data Query
                SELECT TOP 5 Album.AlbumId, Artist.Name, Album.Title
                FROM Artist 
	                INNER JOIN Album 
		                ON Album.ArtistId = Artist.ArtistId
                WHERE Album.Title LIKE @filter
                ORDER BY Album.Title, Artist.Name;

                -- Count Query
                SELECT COUNT(*) AS [Count] 
                FROM Artist 
                    INNER JOIN Album 
                        ON Album.ArtistId = Artist.ArtistId
                WHERE Album.Title LIKE @filter";

            Database db = Database.Open();
            var data = db.ToResultSets(sql, new { filter = "a%" }).ToArray();

            var rows = data.ElementAt(0).ToArray();
            var count = (long)data.ElementAt(1).Single().Count;

            Console.WriteLine("Displaying rows {0} through {1} of {2}", rows.Any() ? 1 : 0, rows.Length, count);
            Console.WriteLine();

            foreach (var row in rows)
            {
                Console.WriteLine("{0} by {1}", row.Title, row.Name);
            }
        }

        private static void SingleResultSet()
        {
            const string sql = @"
                -- Data Query
                SELECT TOP 5 Album.AlbumId, Artist.Name, Album.Title
                FROM Artist 
	                INNER JOIN Album 
		                ON Album.ArtistId = Artist.ArtistId
                WHERE Album.Title LIKE @filter
                ORDER BY Album.Title, Artist.Name";

            Database db = Database.Open();
            var rows = db.ToRows(sql, new {filter = "a%"}).ToArray();

            Console.WriteLine("Displaying rows {0} through {1}", rows.Any() ? 1 : 0, rows.Length);
            Console.WriteLine();

            foreach (var row in rows)
            {
                Console.WriteLine("{0} by {1}", row.Title, row.Name);
            }
        }

        private static void SingleRow()
        {
            const string sql = @"
                -- Data Query
                SELECT TOP 1 Album.AlbumId, Artist.Name, Album.Title
                FROM Artist 
	                INNER JOIN Album 
		                ON Album.ArtistId = Artist.ArtistId
                WHERE Album.Title LIKE @filter
                ORDER BY Album.Title, Artist.Name";

            Database db = Database.Open();
            var row = db.ToRow(sql, new {filter = "a%"});

            Console.WriteLine("Displaying a single row");
            Console.WriteLine();

            Console.WriteLine("{0} by {1}", row.Title, row.Name);
        }



    }
}
