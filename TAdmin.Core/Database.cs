using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace TAdmin.Core
{
    public abstract class Database
    {
        public string Name { get; set; }
        public abstract DatabaseType Type { get; }

        public abstract Task<List<Table>> GetTables();

        public abstract Task Setup(IConfigurationSection configuration);
    }
}