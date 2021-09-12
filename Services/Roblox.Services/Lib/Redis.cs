using System;
using System.Collections.Generic;
using Npgsql;
using Dapper;
using SqlKata;
using SqlKata.Execution;
using SqlKata.Compilers;
using System.Data.SqlClient;
using RedLockNet.SERedis;
using RedLockNet.SERedis.Configuration;
using StackExchange.Redis;

namespace Roblox.Services
{
    public static class Redis
    {
        private static string connectionString { get; set; }
        public static ConnectionMultiplexer client { get; set; }
        public static RedLockFactory redlockFactory { get; set; }

        public static void SetConnectionString(string newConnectionString)
        {
            if (connectionString != null)
            {
                throw new Exception("Existing connectionString is not null. It cannot be set.");
            }

            connectionString = newConnectionString;
            client = ConnectionMultiplexer.Connect(newConnectionString);
            // todo: we will likely want to support multiple ConnectionMultiplexers in the future for high availability... see: https://redis.io/topics/distlock
            var multiplexers = new List<RedLockMultiplexer>
            {
                client,
            };
            redlockFactory = RedLockFactory.Create(multiplexers);
        }
    }
}