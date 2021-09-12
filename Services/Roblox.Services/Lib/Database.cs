using System;
using Npgsql;
using Dapper;
using SqlKata;
using SqlKata.Execution;
using SqlKata.Compilers;
using System.Data.SqlClient;

namespace Roblox.Services
{
    public static class Db
    {
        private static string connectionString { get; set; }
        private static PostgresCompiler compiler { get; set; }
        public static NpgsqlConnection client => new NpgsqlConnection(connectionString);

        public static void SetConnectionString(string newConnectionString)
        {
            if (connectionString != null)
            {
                throw new Exception("Existing connectionString is not null. It cannot be set.");
            }

            connectionString = newConnectionString;
        }
    }
}