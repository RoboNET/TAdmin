using System.Collections.Generic;

namespace TAdmin.Core
{
    public class RelationEntity
    {
        public string Id { get; set; }
        public List<Field> Fields { get; set; }
    }
}