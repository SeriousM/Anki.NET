using Microsoft.Data.Sqlite;

namespace AnkiNet.database;

internal abstract class SqliteRepository<T>
{
    protected abstract string TableName { get; }
    protected abstract string[] Columns { get; }

    protected abstract IEnumerable<SqliteParameter> GetParameters(T item);

    protected abstract T Map(SqliteDataReader reader);

    public async Task<List<T>> ReadAll(SqliteConnection connection)
    {
        var result = new List<T>();

        var readAllSqlQuery = $"SELECT {string.Join(",", Columns.Select(c => $"[{c}]"))} FROM [{TableName}]";

        try
        {
            await using var command = new SqliteCommand(readAllSqlQuery, connection);
            await using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                var item = Map(reader);
                result.Add(item);
            }
        }
        catch (Exception e)
        {
            throw new IOException($"Cannot ReadAll {typeof(T).Name}", e);
        }

        return result;
    }

    public async Task Add(SqliteConnection connection, List<T> items)
    {
        foreach (var chunk in items.Chunk(500))
        {
            var writeSqlQuery = $@"
            INSERT INTO [{TableName}]
            ({string.Join(",", Columns.Select(c => $"[{c}]"))})
            VALUES ";

            var parameterGroups = chunk.Select(i => GetParameters(i).ToArray()).ToArray();
            for (var i = 0; i < parameterGroups.Length; i++)
            {
                var parameterGroup = parameterGroups[i];
                foreach (var parameter in parameterGroup)
                {
                    parameter.ParameterName += $"_{i}";
                }

                writeSqlQuery += "(" + string.Join(", ", parameterGroup.Select(pg => pg.ParameterName)) + "),";
            }

            writeSqlQuery = writeSqlQuery.Remove(writeSqlQuery.Length - 1);

            try
            {
                await using var command = new SqliteCommand(writeSqlQuery, connection);
                command.Parameters.AddRange(parameterGroups.SelectMany(x => x));
                var i = await command.ExecuteNonQueryAsync();
            }
            catch (Exception e)
            {
                throw new IOException($"Cannot Add {typeof(T).Name}", e);
            }
        }
    }
}