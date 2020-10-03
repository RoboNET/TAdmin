using System;
using GraphQL;
using GraphQL.Types;
using GraphQL.Utilities;
using TAdmin.Core;
using TAdmin.DataSource.Mssql;
using TAdmin.Logic;

namespace TAdmin.GraphQL
{
    public class DatabaseType : ObjectGraphType<Database>
    {
        public DatabaseType()
        {
            Name = "Database";

            Field(d => d.Name).Description("The name of the database.");

            FieldAsync<ListGraphType<TableInterface>>("tables",
                resolve: async context => { return await context.Source.GetTables(); });
        }
    }


    public class TableInterface : InterfaceGraphType<Table>
    {
        public TableInterface()
        {
            Name = "Table";
            Field(t => t.Name);
        }
    }

    public class RelationTableType : ObjectGraphType<RelationTable>
    {
        public RelationTableType()
        {
            Name = "RelationTable";
            Field(t => t.Name).Description("Test description of relaton table");
            FieldAsync<ListGraphType<RelationFieldMetadataType>>("fields",
                resolve: async context => { return await context.Source.GetMetadata(); });

            Interface<TableInterface>();
        }
    }

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

    public class AdminQuery : ObjectGraphType<object>
    {
        public AdminQuery(IDatabaseManager databaseManager)
        {
            Name = "Query";

            Field<ListGraphType<DatabaseType>>("databases", resolve: context => databaseManager.GetDatabases());
        }
    }

    public class AdminMutation : ObjectGraphType
    {
        public AdminMutation()
        {
            Name = "Mutation";
        }
    }

    public class AdminSchema : Schema
    {
        public AdminSchema(IServiceProvider provider)
            : base(provider)
        {
            Query = provider.GetRequiredService<AdminQuery>();
            Mutation = provider.GetRequiredService<AdminMutation>();
            RegisterType<RelationTableType>();
        }
    }
}