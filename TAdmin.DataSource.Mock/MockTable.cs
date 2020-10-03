using System.Collections.Generic;
using System.Threading.Tasks;
using TAdmin.Core;

namespace TAdmin.DataSource.Mock
{
    public class MockTable : RelationTable
    {
        public List<RelationFieldMetadata> RelationFieldMetadatas = new List<RelationFieldMetadata>();
        public List<RowEntity> RelationEntities = new List<RowEntity>();


        public override async Task<List<RelationFieldMetadata>> GetMetadata()
        {
            return RelationFieldMetadatas;
        }

        public override async Task<List<RowEntity>> GetEntityData(int count, int skip)
        {
            return RelationEntities;
        }
    }
}