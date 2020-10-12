using System.Collections.Generic;

namespace TAdmin.Core
{
    public class RowEntity
    {
        public RowEntity()
        {
            Columns = new List<Field>();
        }

        public List<Field> Columns { get; set; }
    }
}