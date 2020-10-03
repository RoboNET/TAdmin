using GraphQL.Types;
using TAdmin.Core;

namespace TAdmin.Web.GraphQL
{
    public class RelationFieldMetadataType : ObjectGraphType<RelationFieldMetadata>
    {
        public RelationFieldMetadataType()
        {
            Name = "RelationFieldMetadata";
            Field(t => t.Name);
            Field(t => t.Type);
            Field(t => t.IsForeignKey);
            Field(t => t.ForeignKeyTableName);
        }
    }

    public class FieldType : ObjectGraphType<Field>
    {
        public FieldType()
        {
            Name = "Field";
            Field(t => t.Name);
            Field(t => t.Value);
        }
    }

    public class RowEntityType : ObjectGraphType<RowEntity>
    {
        public RowEntityType()
        {
            Name = "RowEntity";
            Field<ListGraphType<FieldType>>("columns", resolve: context => context.Source.Columns);
        }
    }
}