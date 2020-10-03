using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TAdmin.Core;
using TAdmin.DataSource.Mssql;

namespace TAdmin.Logic
{
    public class DatabaseManager : IDatabaseManager
    {
        public async Task<List<Database>> GetDatabases()
        {
            return new List<Database>()
            {
                new MsSqlDatabase("Test")
                {
                }
            };
        }

        public async Task<Database> GetDatabase(string name)
        {
            return new MsSqlDatabase(name)
            {
            };
        }
    }
}