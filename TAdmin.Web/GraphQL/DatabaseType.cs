using GraphQL;
using GraphQL.Types;
using TAdmin.Core;

namespace TAdmin.Web.GraphQL
{
    public class DatabaseType : ObjectGraphType<Database>
    {
        public DatabaseType()
        {
            Name = "Database";

            Field(d => d.Name).Description("The name of the database.");

            Field<ListGraphType<TableInterface>>("tables",
                arguments: new QueryArguments(
                    new QueryArgument<StringGraphType> {Name = "tableName"}
                ),
                resolve: context => context.Source.GetTables(context.GetArgument<string>("tableName")));
        }
    }
}