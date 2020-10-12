using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using TAdmin.Core;

namespace TAdmin.DataSource.Mssql
{
    public class MssqlDatabase : Database
    {
        public override DatabaseType Type => DatabaseType.Relation;

        private Config _config;

        private ConnectionFactory _connectionFactory;

        public override async Task<List<Table>> GetTables(string? name)
        {
            using var connection = _connectionFactory.Create();
            var sql =
                "SELECT TABLE_CATALOG as Catalog, TABLE_SCHEMA as [Schema], TABLE_NAME as Name, TABLE_TYPE as Type FROM INFORMATION_SCHEMA.TABLES";

            if (!string.IsNullOrEmpty(name))
            {
                sql += " WHERE TABLE_NAME = @name";
            }

            var tablesData = await connection.QueryAsync<TableData>(sql, new {name});

            var tables = new List<Table>();
            foreach (var tableData in tablesData)
            {
                var table = new MssqlTable(tableData.Name, _connectionFactory);
                tables.Add(table);
            }

            return tables;
        }

        public override async Task Setup(IConfigurationSection configurationSection)
        {
            _config = configurationSection.Get<Config>();
            _connectionFactory = new ConnectionFactory(_config.ConnectionString);
        }

        private class Config
        {
            public string ConnectionString { get; set; }
        }

        private class TableData
        {
            public string Catalog { get; set; }
            public string Schema { get; set; }
            public string Name { get; set; }
            public string Type { get; set; }
        }
    }

    public class ConnectionFactory
    {
        private readonly string _connectionString;

        public ConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SqlConnection Create()
        {
            return new SqlConnection(_connectionString);
        }
    }
}