using System.Collections.Generic;
using System.Threading.Tasks;
using TAdmin.Core;

namespace TAdmin.Logic
{
    public interface IDatabaseManager
    {
        Task<List<Database>> GetDatabases(string name);
    }
}