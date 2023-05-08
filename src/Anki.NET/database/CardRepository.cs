using AnkiNet.database.model;
using Microsoft.Data.Sqlite;

namespace AnkiNet.database;

internal class CardRepository : SqliteRepository<card>
{
    protected override string TableName => "cards";

    protected override string[] Columns =>
        new[]
        {
            "id", "nid", "did", "ord", "mod",
            "usn", "type", "queue", "due", "ivl",
            "factor", "reps", "lapses", "left", "odue",
            "odid", "flags", "data"
        };

    protected override IEnumerable<SqliteParameter> GetParameters(card i)
    {
        yield return new SqliteParameter("@id", i.id);
        yield return new SqliteParameter("@nid", i.nid);
        yield return new SqliteParameter("@did", i.did);
        yield return new SqliteParameter("@ord", i.ord);
        yield return new SqliteParameter("@mod", i.mod);
        yield return new SqliteParameter("@usn", i.usn);
        yield return new SqliteParameter("@type", i.type);
        yield return new SqliteParameter("@queue", i.queue);
        yield return new SqliteParameter("@due", i.due);
        yield return new SqliteParameter("@ivl", i.ivl);
        yield return new SqliteParameter("@factor", i.factor);
        yield return new SqliteParameter("@reps", i.reps);
        yield return new SqliteParameter("@lapses", i.lapses);
        yield return new SqliteParameter("@left", i.left);
        yield return new SqliteParameter("@odue", i.odue);
        yield return new SqliteParameter("@odid", i.odid);
        yield return new SqliteParameter("@flags", i.flags);
        yield return new SqliteParameter("@data", i.data);
    }

    protected override card Map(SqliteDataReader reader)
    {
        return new card(
            reader.Get<long>("id"),
            reader.Get<long>("nid"),
            reader.Get<long>("did"),
            reader.Get<long>("ord"),
            reader.Get<long>("mod"),
            reader.Get<long>("usn"),
            reader.Get<long>("type"),
            reader.Get<long>("queue"),
            reader.Get<long>("due"),
            reader.Get<long>("ivl"),
            reader.Get<long>("factor"),
            reader.Get<long>("reps"),
            reader.Get<long>("lapses"),
            reader.Get<long>("left"),
            reader.Get<long>("odue"),
            reader.Get<long>("odid"),
            reader.Get<long>("flags"),
            reader.Get<string>("data")
        );
    }
}