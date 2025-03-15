﻿using System.Data;
using Elsa.Sql.Client;
using Elsa.Sql.Models;
using Microsoft.Data.Sqlite;

namespace Elsa.Sql.Sqlite;

public class SqliteClient : BaseSqlClient, ISqlClient
{
    private string? _connectionString;

    /// <summary>
    /// Sqlite client implementation.
    /// </summary>
    /// <param name="connectionString"></param>
    public SqliteClient(string? connectionString) => _connectionString = connectionString;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public async Task<int?> ExecuteCommandAsync(EvaluatedQuery evaluatedQuery)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();
        var command = new SqliteCommand(evaluatedQuery.Query, connection);
        AddParameters(command, evaluatedQuery.Parameters);

        var result = await command.ExecuteNonQueryAsync();
        return result;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public async Task<object?> ExecuteScalarAsync(EvaluatedQuery evaluatedQuery)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();
        var command = new SqliteCommand(evaluatedQuery.Query, connection);
        AddParameters(command, evaluatedQuery.Parameters);

        var result = await command.ExecuteScalarAsync();
        return result;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public async Task<DataSet?> ExecuteQueryAsync(EvaluatedQuery evaluatedQuery)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();
        var command = new SqliteCommand(evaluatedQuery.Query, connection);
        AddParameters(command, evaluatedQuery.Parameters);

        using var reader = await command.ExecuteReaderAsync();
        return await Task.FromResult(ReadAsDataSet(reader));
    }

    /// <summary>
    /// Inject parameters into the query to prevent SQL injection.
    /// </summary>
    /// <param name="command">Command to add the parameters to</param>
    /// <param name="parameters">Parameters to add</param>
    /// <returns></returns>
    private SqliteCommand AddParameters(SqliteCommand command, Dictionary<string, object?> parameters)
    {
        foreach (var param in parameters)
        {
            command.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
        }
        return command;
    }
}