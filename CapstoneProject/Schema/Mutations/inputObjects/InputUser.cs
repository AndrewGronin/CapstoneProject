using CapstoneProject.Infrastructure;
using HotChocolate;

namespace CapstoneProject.Schema.Mutations.inputObjects
{
    public class InputUser
    {
        public string? FirstName { get; set; }
        
        public string? LastName { get; set; }
        
        [GraphQLNonNullType]
        public string Email { get; set; }
        
        [GraphQLNonNullType]
        public string Password { get; set; }
        
        [GraphQLIgnore]
        public Model.Entities.User ToModel()
        {
            return new Model.Entities.User()
            {
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                PasswordHash = Hasher.Hash(Password)
            };
        }
    }
}