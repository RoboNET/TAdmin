using System.Collections.Generic;
using System.Threading.Tasks;
using TAdmin.Core;

namespace TAdmin.DataSource.Mssql
{
    public class MsSqlDatabase : Database
    {
        private readonly string _name;

        public MsSqlDatabase(string name)
        {
            _name = name;
        }

        public override string Name => _name;
        public override DatabaseType Type => DatabaseType.Relation;

        public override async Task<List<Table>> GetTables()
        {
            return new List<Table>()
            {
                new MsSqlTable()
                {
                    Name = "TestTable"
                },
                new MsSqlTable()
                {
                    Name = "RelationTable"
                }
            };
        }
    }
}