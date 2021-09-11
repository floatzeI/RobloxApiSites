using System;
using Npgsql;
using Dapper;
using SqlKata;
using SqlKata.Execution;
using SqlKata.Compilers;
using System.Data.SqlClient;

namespace Roblox.Services
{
    public class Db
    {
        private static string connectionString { get; set; }
        private static PostgresCompiler compiler { get; set; }
        public static NpgsqlConnection client => new NpgsqlConnection(connectionString);

        public static QueryFactory queryFactory
        {
            get
            {
                var factory = new QueryFactory(client, compiler);
#if DEBUG
                factory.Logger = compiled => {
                    Console.WriteLine(compiled.ToString().Replace("\n", ""));
                };
#endif
                return factory;
            }
        }
        public Db(string connectionStringParam)
        {
            connectionString = connectionStringParam;
            compiler = new PostgresCompiler();
        }

        public static Query Query(string column)
        {
            return queryFactory.Query(column);
        }
    }
}