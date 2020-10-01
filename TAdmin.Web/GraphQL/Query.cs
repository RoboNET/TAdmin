using System;
using GraphQL;
using GraphQL.Types;
using GraphQL.Utilities;

namespace TAdmin.GraphQL
{
    public class AdminQuery : ObjectGraphType<object>
    {
        public AdminQuery()
        {
            Name = "Query";
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
        }
    }
}