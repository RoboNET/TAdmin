using System.Collections.Generic;
using System.Threading.Tasks;

namespace TAdmin.Core
{
    public abstract class Database
    {
        public abstract string Name { get; }
        public abstract DatabaseType Type { get; }

        public abstract  Task<List<Table>> GetTables();
    }
}