using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using TAdmin.Core;

namespace TAdmin.DataSource.Mssql
{
    public class MssqlTable : RelationTable
    {
        private readonly ConnectionFactory _connectionFactory;
        public List<RowEntity> RelationEntities = new List<RowEntity>();

        public MssqlTable(string name, ConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
            Name = name;
        }

        public override async Task<List<RelationFieldMetadata>> GetMetadata()
        {
            using var connection = _connectionFactory.Create();
            var sql = "SELECT COLUMN_NAME as Name, DATA_TYPE as Type, IS_NULLABLE as Nullable, CHARACTER_MAXIMUM_LENGTH as Length FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = @Name";

            var fieldMetadata = await connection.QueryAsync<FieldMetadataData>(sql, new {Name});

            var result = new List<RelationFieldMetadata>();
            foreach (var fieldMetadataData in fieldMetadata)
            {
                result.Add(new MssqlFieldMetadata()
                {
                    Name = fieldMetadataData.Name,
                    Type = fieldMetadataData.Length.HasValue
                        ? $"{fieldMetadataData.Type}({fieldMetadataData.Length})"
                        : fieldMetadataData.Type
                });
            }

            return result;
        }

        public override async Task<List<RowEntity>> GetEntityData(int count, int skip)
        {
            using var connection = _connectionFactory.Create();
            var sql = $"SELECT * FROM {Name}";
            
            SqlCommand command = new SqlCommand(
                sql,
                connection);
            connection.Open();

            using SqlDataReader reader = await command.ExecuteReaderAsync();
            var result = new List<RowEntity>();

            int fieldCount = reader.FieldCount;
            while (reader.HasRows)
            {
                // Console.WriteLine("\t{0}\t{1}", reader.GetName(0),
                //     reader.GetName(1));

                var rowResult=new RowEntity();
                result.Add(rowResult);
                
                while (reader.Read())
                {
                    for (int i = 0; i < fieldCount; i++)
                    {
                        rowResult.Columns.Add(new Field()
                        {
                            Name = reader.GetName(i),
                            Value = reader.GetValue(i)?.ToString()
                        });
                    }
                    
                }
                reader.NextResult();
            }

            return result;
        }

        private class FieldMetadataData
        {
            public string Name { get; set; }
            public string Type { get; set; }
            public string Nullable { get; set; } //NO, YES
            public int? Length { get; set; }
        }
    }
}