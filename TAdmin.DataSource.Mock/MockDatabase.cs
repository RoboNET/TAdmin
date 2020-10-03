using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using TAdmin.Core;

namespace TAdmin.DataSource.Mock
{
    public class MockDatabase : Database
    {
        public override DatabaseType Type => DatabaseType.Relation;

        private List<Table> _tables = new List<Table>();

        public override async Task<List<Table>> GetTables(string name)
        {
            return _tables;
        }

        public override async Task Setup(IConfigurationSection configurationSection)
        {
            var config = configurationSection.Get<Config>();
            foreach (var configTable in config.Tables)
            {
                _tables.Add(new MockTable()
                {
                    Name = configTable.Key,
                    RelationEntities = configTable.Value.Values.Select(metadataConfig =>
                    {
                        return new RowEntity()
                        {
                            Columns = metadataConfig.Select(pair => new Field()
                            {
                                Name = pair.Key,
                                Value = pair.Value
                            }).ToList()
                        };
                    }).ToList(),
                    RelationFieldMetadatas = configTable.Value.Metadata.Select(metadataConfig =>
                        new MockFieldMetadata()
                        {
                            Name = metadataConfig.Name,
                            Type = metadataConfig.Type
                        } as RelationFieldMetadata).ToList()
                });
            }
        }

        private class Config
        {
            public Dictionary<string, TableConfig> Tables { get; set; }
        }

        private class TableConfig
        {
            public List<MetadataConfig> Metadata { get; set; }
            public List<Dictionary<string, string>> Values { get; set; }
        }

        private class MetadataConfig
        {
            public string Name { get; set; }
            public string Type { get; set; }
        }
    }
}