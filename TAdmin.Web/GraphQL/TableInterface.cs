using GraphQL.Types;
using TAdmin.Core;

namespace TAdmin.Web.GraphQL
{
    public class TableInterface : InterfaceGraphType<Table>
    {
        public TableInterface()
        {
            Name = "Table";
            Field(t => t.Name);
        }
    }
}