using HotChocolate.Types;

namespace CapstoneProject.Schema.Queries
{
    [ExtendObjectType(typeof(RootQuery))]
    public class AuthorizationQuery
    {
        public int Test => 1;
    }
}