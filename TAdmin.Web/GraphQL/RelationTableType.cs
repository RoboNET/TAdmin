using GraphQL;
using GraphQL.Types;
using TAdmin.Core;

namespace TAdmin.Web.GraphQL
{
    public class RelationTableType : ObjectGraphType<RelationTable>
    {
        public RelationTableType()
        {
            Name = "RelationTable";
            Field(t => t.Name);
            FieldAsync<ListGraphType<RelationFieldMetadataType>>("fields",
                resolve: async context => { return await context.Source.GetMetadata(); });

            FieldAsync<ListGraphType<RowEntityType>>("values",
                arguments: new QueryArguments(
                    new QueryArgument<IntGraphType> {Name = "count"},
                    new QueryArgument<IntGraphType> {Name = "skip"}
                ),
                resolve: async context =>
                {
                    return await context.Source.GetEntityData(
                        context.GetArgument<int>("count", 50),
                        context.GetArgument<int>("skip"));
                });

            Interface<TableInterface>();
        }
    }
}