using System.Linq;
using GraphQL;
using GraphQL.Types;
using TAdmin.Core;
using TAdmin.Logic;

namespace TAdmin.Web.GraphQL
{
    public class AdminQuery : ObjectGraphType<object>
    {
        public AdminQuery(IDatabaseManager databaseManager)
        {
            Name = "Query";

            Field<ListGraphType<DatabaseType>>("databases",
                arguments: new QueryArguments(
                    new QueryArgument<StringGraphType> {Name = "name"}
                ),
                resolve: context => databaseManager.GetDatabases(context.GetArgument<string>("name")));

            FieldAsync<ListGraphType<TableInterface>>("tables",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> {Name = "dbName"},
                    new QueryArgument<StringGraphType> {Name = "tableName"}
                ),
                resolve: async context =>
                {
                    var database = await databaseManager.GetDatabases(context.GetArgument<string>("dbName"));
                    return await database.FirstOrDefault().GetTables(context.GetArgument<string>("tableName"));
                });

            FieldAsync<ListGraphType<RowEntityType>>("values",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> {Name = "dbName"},
                    new QueryArgument<StringGraphType> {Name = "tableName"},
                    new QueryArgument<IntGraphType> {Name = "count"},
                    new QueryArgument<IntGraphType> {Name = "skip"}
                ),
                resolve: async context =>
                {
                    var database = (await databaseManager.GetDatabases(context.GetArgument<string>("dbName")))
                        .FirstOrDefault();
                    var table =
                        (await database.GetTables(context.GetArgument<string>("tableName"))).FirstOrDefault() as
                        RelationTable;

                    return await table.GetEntityData(
                        context.GetArgument<int>("count", 50),
                        context.GetArgument<int>("skip"));
                });
        }
    }
}