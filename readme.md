# Simple.Data.RawSql #
## Raw sql extensions to Simple.Data ##

Query a single row:

	Database db = Database.Open(); // Notice statically typed the db variable as Database
	var sql = @"SELECT TOP 1 * FROM tblData WHERE Name = @name ORDER BY Id";
	var row = db.ToRows(sql, new {name = "Jason Dentler"});
	// rows is a dynamic
	
	var id = (Guid) row.Id;
	var name = (string) row.Name;

Query several rows:

	Database db = Database.Open(); // Notice statically typed the db variable as Database
	var sql = @"SELECT * FROM tblData WHERE Name = @name";
	var rows = db.ToRows(sql, new {name = "Jason Dentler"});
	
	// rows is IEnumerable<dynamic>
	foreach (var row in rows)
	{
		var id = (Guid) row.Id;
		var name = (string) row.Name;
	}

Batch several queries together:

	Database db = Database.Open(); // Notice statically typed the db variable as Database
	var sql = @"
		SELECT TOP 5 * FROM tblData WHERE Name = @name ORDER BY Name;
		SELECT COUNT(*) AS [Count] FROM tblData WHERE Name = @name";
	// Sql queries delimited by semicolon
	
	var resultSets = db.ToResultSets(sql, new {name = "Jason Dentler"});
	// resultSets is IEnumerable<IEnumerable<dynamic>>
	
	rows = resultSets.ElementAt(0).ToArray();
	var count = (long) resultSets.ElementAt(1).Single().Count;
	
	Console.WriteLine("Showing results 0 through {0} of {1}", rows.Count(), count);
	foreach (var row in rows)
	{
		var id = (Guid) row.Id;
		var name = (string) row.Name;
	}
