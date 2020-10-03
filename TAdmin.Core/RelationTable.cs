using System.Collections.Generic;
using System.Threading.Tasks;

namespace TAdmin.Core
{
    public abstract class RelationTable : Table
    {
        public abstract Task<List<RelationFieldMetadata>> GetMetadata();

        public abstract Task<List<RowEntity>> GetEntityData(int count, int skip);
    }
}