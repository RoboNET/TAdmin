using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using TAdmin.Core;

namespace TAdmin.Logic
{
    public class DatabaseManager : IDatabaseManager
    {
        private readonly IConfiguration _configuration;

        private static List<Database> _databases;

        private static readonly SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1, 1);

        public DatabaseManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<Database>> GetDatabases(string name)
        {
            if (_databases == null)
            {
                try
                {
                    await _semaphoreSlim.WaitAsync();
                    if (_databases == null)
                    {
                        _databases = await GetDatabaseAssemblies();
                    }
                }
                finally
                {
                    _semaphoreSlim.Release();
                }
            }

            if (string.IsNullOrEmpty(name))
                return _databases;
            return _databases.Where(d => d.Name == name).ToList();
        }

        private async Task<List<Database>> GetDatabaseAssemblies()
        {
            var settingsSections = _configuration.GetSection("Databases").GetChildren()
                .Select(s => s.Get<DatabaseSettings>());


            var method = typeof(Database).GetMethod("Setup");

            List<Type> databaseTypes = new List<Type>();
            var files = Directory.GetFiles(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                "*DataSource*.dll");
            foreach (var file in files)
            {
                var assembly = Assembly.LoadFile(file);
                var types = assembly.GetTypes().Where(t => typeof(Database).IsAssignableFrom(t));
                databaseTypes.AddRange(types);
            }

            var result = new List<Database>();

            foreach (var databaseSettings in settingsSections)
            {
                var type = databaseTypes.FirstOrDefault(
                    t => t.Name == databaseSettings.Type);
                var database = Activator.CreateInstance(type) as Database;
                database.Name = databaseSettings.Name;
                Task setupResult = (Task) method.Invoke(database, new[] {databaseSettings.Settings});
                await setupResult;

                result.Add(database);
            }


            return result;
        }

        private class DatabaseSettings
        {
            public string Name { get; set; }
            public string Type { get; set; }
            public IConfigurationSection Settings { get; set; }
        }
    }
}