using AnkiNet.database.model;
using Microsoft.Data.Sqlite;

namespace AnkiNet.database;

internal class GraveRepository : SqliteRepository<grave>
{
    protected override string TableName => "graves";

    protected override string[] Columns => new[] { "usn", "oid", "type" };

    protected override IEnumerable<SqliteParameter> GetParameters(grave i)
    {
        yield return new SqliteParameter("@usn", i.usn);
        yield return new SqliteParameter("@oid", i.oid);
        yield return new SqliteParameter("@type", i.type);
    }

    protected override grave Map(SqliteDataReader reader)
    {
        return new grave(
            reader.Get<int>("usn"),
            reader.Get<int>("oid"),
            reader.Get<int>("type")
        );
    }
}
