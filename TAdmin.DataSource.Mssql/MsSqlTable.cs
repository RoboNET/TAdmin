using System.Collections.Generic;
using System.Threading.Tasks;
using TAdmin.Core;

namespace TAdmin.DataSource.Mssql
{
    public class MsSqlTable : RelationTable
    {
        public override async Task<List<RelationFieldMetadata>> GetMetadata()
        {
            return new List<RelationFieldMetadata>()
            {
                new MsSqlFieldMetadata()
                {
                    Name = "Id",
                    Type = "uniquieid"
                },
                new MsSqlFieldMetadata()
                {
                    Name = "TestField",
                    Type = "nvarchar(50)"
                }
            };
        }

        public override Task<List<RelationEntity>> GetEntityData(int count, int skip)
        {
            throw new System.NotImplementedException();
        }
    }
}