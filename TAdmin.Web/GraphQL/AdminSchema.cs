using System;
using GraphQL.Types;
using GraphQL.Utilities;

namespace TAdmin.Web.GraphQL
{
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