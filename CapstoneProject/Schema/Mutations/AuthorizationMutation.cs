using HotChocolate.Types;

namespace CapstoneProject.Schema.Mutations
{
    [ExtendObjectType(typeof(RootMutation))]
    public class AuthorizationMutation
    {
        public int Test => 1;
    }
}