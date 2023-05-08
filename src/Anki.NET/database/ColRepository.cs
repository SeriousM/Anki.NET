using AnkiNet.database.model;
using Microsoft.Data.Sqlite;

namespace AnkiNet.database;

internal class ColRepository : SqliteRepository<col>
{
    protected override string TableName => "col";

    protected override string[] Columns =>
        new[]
        {
            "id", "crt", "mod", "scm", "ver",
            "dty", "usn", "ls", "conf", "models",
            "decks", "dconf", "tags"
        };

    protected override IEnumerable<SqliteParameter> GetParameters(col i)
    {
        yield return new SqliteParameter("@id", i.id);
        yield return new SqliteParameter("@crt", i.crt);
        yield return new SqliteParameter("@mod", i.mod);
        yield return new SqliteParameter("@scm", i.scm);
        yield return new SqliteParameter("@ver", i.ver);
        yield return new SqliteParameter("@dty", i.dty);
        yield return new SqliteParameter("@usn", i.usn);
        yield return new SqliteParameter("@ls", i.ls);
        yield return new SqliteParameter("@conf", i.conf);
        yield return new SqliteParameter("@models", i.models);
        yield return new SqliteParameter("@decks", i.decks);
        yield return new SqliteParameter("@dconf", i.dconf);
        yield return new SqliteParameter("@tags", i.tags);
    }

    protected override col Map(SqliteDataReader reader)
    {
        return new col(
            reader.Get<long>("id"),
            reader.Get<long>("crt"),
            reader.Get<long>("mod"),
            reader.Get<long>("scm"),
            reader.Get<long>("ver"),
            reader.Get<long>("dty"),
            reader.Get<long>("usn"),
            reader.Get<long>("ls"),
            reader.Get<string>("conf"),
            reader.Get<string>("models"),
            reader.Get<string>("decks"),
            reader.Get<string>("dconf"),
            reader.Get<string>("tags")
        );
    }
}