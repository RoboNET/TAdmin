using System.Collections.Generic;
using System.Threading.Tasks;
using TAdmin.Core;

namespace TAdmin.DataSource.Mock
{
    public class MockTable : RelationTable
    {
        public List<RelationFieldMetadata> RelationFieldMetadatas = new List<RelationFieldMetadata>();
        public List<RelationEntity> RelationEntities = new List<RelationEntity>();


        public override async Task<List<RelationFieldMetadata>> GetMetadata()
        {
            return RelationFieldMetadatas;
        }

        public override async Task<List<RelationEntity>> GetEntityData(int count, int skip)
        {
            return RelationEntities;
        }
    }
}