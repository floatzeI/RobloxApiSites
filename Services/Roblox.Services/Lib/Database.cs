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
        private static NpgsqlConnection integrationTestConnection { get; set; }

        public static NpgsqlConnection client => integrationTestConnection ?? new NpgsqlConnection(connectionString);

        public static void SetConnectionString(string newConnectionString)
        {
            if (connectionString != null)
            {
                throw new Exception("Existing connectionString is not null. It cannot be set.");
            }

            connectionString = newConnectionString;
        }

        public static void SetConnectionForIntegrationTests(NpgsqlConnection conn)
        {
            integrationTestConnection = conn;
        }
    }
}