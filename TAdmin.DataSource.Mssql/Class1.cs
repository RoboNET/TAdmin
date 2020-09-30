using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TAdmin.Core;

namespace TAdmin.DataSource.Mssql
{
    public interface IDataSource
    {
        Task<IDatabase> GetDatabaseInfo();
        Task<List<Field>> GetTableData(string tableName, int countOnPage, int skip);
        Task InsertFields();
        Task UpdateFields(string tableName, Dictionary<string, string> fields);
    }

    public class MssqlDataSource : IDataSource
    {
        public async Task<IDatabase> GetDatabaseInfo()
        {
            return new MssqlDatabase()
            {
                Tables = new List<ITable>()
                {
                    new RelationTable()
                    {
                        Fields = new List<RelationFieldMetadata>()
                        {
                            new RelationFieldMetadata()
                            {
                                Name = "Test",
                                Type = "nvarchar(50)",
                            }
                        }
                    }
                }
            };
        }

        public async Task<List<Field>> GetTableData(string tableName, int countOnPage, int skip)
        {
            var metadata = await GetDatabaseInfo();

            var table = metadata.Tables.FirstOrDefault(t => t.Name == tableName) as RelationTable;

            var items = new List<Field>();

            foreach (var field in table.Fields)
            {
                items.Add(new Field()
                {
                    Metadata = field,
                    Value = "Test"
                });
            }

            return items;
        }

        public async Task InsertFields()
        {
            
        }
        
        public async Task UpdateFields(string tableName, Dictionary<string, string> fields)
        {
            var metadata = await GetDatabaseInfo();

            var table = metadata.Tables.FirstOrDefault(t => t.Name == tableName) as RelationTable;
        }
    }
}