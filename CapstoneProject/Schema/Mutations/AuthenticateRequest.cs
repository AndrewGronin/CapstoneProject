#nullable disable
using HotChocolate;

namespace CapstoneProject.Schema.Mutations
{
    public class AuthenticateRequest
    {
        [GraphQLNonNullType]
        public string Email { get; set; }

        [GraphQLNonNullType]
        public string Password { get; set; }
    }
}