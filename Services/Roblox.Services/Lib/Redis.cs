using System;
using Npgsql;
using Dapper;
using SqlKata;
using SqlKata.Execution;
using SqlKata.Compilers;
using System.Data.SqlClient;
using StackExchange.Redis;

namespace Roblox.Services
{
    public static class Redis
    {
        private static string connectionString { get; set; }
        public static ConnectionMultiplexer client { get; set; }

        public static void SetConnectionString(string newConnectionString)
        {
            if (connectionString != null)
            {
                throw new Exception("Existing connectionString is not null. It cannot be set.");
            }

            connectionString = newConnectionString;
            client = ConnectionMultiplexer.Connect(newConnectionString);
        }
    }
}