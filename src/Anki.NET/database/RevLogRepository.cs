using AnkiNet.database.model;
using Microsoft.Data.Sqlite;

namespace AnkiNet.database;

internal class RevLogRepository : SqliteRepository<revLog>
{
    protected override string TableName => "revlog";

    protected override string[] Columns =>
        new[] { "id", "cid", "usn", "ease", "ivl", "lastIvl", "factor", "time", "type" };

    protected override IEnumerable<SqliteParameter> GetParameters(revLog i)
    {
        yield return new SqliteParameter("@id", i.id);
        yield return new SqliteParameter("@cid", i.cid);
        yield return new SqliteParameter("@usn", i.usn);
        yield return new SqliteParameter("@ease", i.ease);
        yield return new SqliteParameter("@ivl", i.ivl);
        yield return new SqliteParameter("@lastIvl", i.lastIvl);
        yield return new SqliteParameter("@factor", i.factor);
        yield return new SqliteParameter("@time", i.time);
        yield return new SqliteParameter("@type", i.type);
    }

    protected override revLog Map(SqliteDataReader reader)
    {
        return new revLog(
            reader.Get<long>("id"),
            reader.Get<long>("cid"),
            reader.Get<long>("usn"),
            reader.Get<long>("ease"),
            reader.Get<long>("ivl"),
            reader.Get<long>("lastIvl"),
            reader.Get<long>("factor"),
            reader.Get<long>("time"),
            reader.Get<long>("type")
        );
    }
}