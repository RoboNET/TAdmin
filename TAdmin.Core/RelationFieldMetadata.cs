namespace TAdmin.Core
{
    public abstract class RelationFieldMetadata
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public bool IsForeignKey { get; set; }
        public string? ForeignKeyTableName { get; set; }
    }
}