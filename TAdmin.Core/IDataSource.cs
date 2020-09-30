using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TAdmin.Core
{
    public interface IDatabase
    {
        public string Name { get; set; }
        public List<ITable> Tables { get; set; }
    }

    
    public interface ITable
    {
        public string Name { get; set; }
    }

    public class RelationTable : ITable
    {
        public string Name { get; set; }
        public List<RelationFieldMetadata> Fields { get; set; }
    }

    public class DocumentTable : ITable
    {
        public string Name { get; set; }
    }


    public interface IFieldMetadata
    {
        public string Name { get; set; }
        public string Type { get; set; }
    }

    //TODO: Переписать на рекорды, когда райдер начнёт поддерживать
    public class RelationFieldMetadata : IFieldMetadata
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public bool IsForeignKey { get; set; }
        public string? ForeignKeyTableName { get; set; }
    }

    public class Field
    {
        public IFieldMetadata Metadata { get; set; }
        public string Value { get; set; }
    }

    public interface IRelationDatabase : IDatabase
    {
    }

    public class MssqlDatabase : IRelationDatabase
    {
        public string ConnectionString { get; set; }
        public string Name { get; set; }
        public List<ITable> Tables { get; set; }
    }
}