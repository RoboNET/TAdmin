using GraphQL.Types;

namespace TAdmin.Web.GraphQL
{
    public class AdminMutation : ObjectGraphType
    {
        public AdminMutation()
        {
            Name = "Mutation";
        }
    }
}