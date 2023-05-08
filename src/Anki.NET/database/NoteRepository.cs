﻿using AnkiNet.database.model;
using Microsoft.Data.Sqlite;

namespace AnkiNet.database;

internal class NoteRepository : SqliteRepository<note>
{
    protected override string TableName => "notes";

    protected override string[] Columns =>
      new[]
      {
        "id", "guid", "mid",
        "mod", "usn", "tags",
        "flds", "sfld", "csum",
        "flags", "data"
      };

    protected override IEnumerable<SqliteParameter> GetParameters(note i)
    {
        yield return new SqliteParameter("@id", i.id);
        yield return new SqliteParameter("@guid", i.guid);
        yield return new SqliteParameter("@mid", i.mid);
        yield return new SqliteParameter("@mod", i.mod);
        yield return new SqliteParameter("@usn", i.usn);
        yield return new SqliteParameter("@tags", i.tags);
        yield return new SqliteParameter("@flds", i.flds);
        yield return new SqliteParameter("@sfld", i.sfld);
        yield return new SqliteParameter("@csum", i.csum);
        yield return new SqliteParameter("@flags", i.flags);
        yield return new SqliteParameter("@data", i.data);
    }

    protected override note Map(SqliteDataReader reader)
    {
        /*
         * Regarding column 'sfld'.
         * See: https://anki.tenderapp.com/discussions/ankidesktop/32752-bug-in-ankis-database-schema-sfld-an-integer
         * | The sort field is an integer so that when users are sorting on a field that contains only numbers,
         * | they are sorted in numeric instead of lexical order.
         */
        return new note(
            reader.Get<long>("id"),
            reader.Get<string>("guid"),
            reader.Get<long>("mid"),
            reader.Get<long>("mod"),
            reader.Get<long>("usn"),
            reader.Get<string>("tags"),
            reader.Get<string>("flds"),
            reader.Get<string>("sfld"), // 'sfld' is an integer column but contains strings
            reader.Get<long>("csum"),
            reader.Get<long>("flags"),
            reader.Get<string>("data")
        );
    }
}