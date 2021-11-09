using HotChocolate;

#nullable disable
namespace CapstoneProject.Schema.Queries
{
    public class User
    {
        public int Id { get; set; }
        
        [GraphQLNonNullType]
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        [GraphQLNonNullType]
        public string Email { get; set; }
        
        [GraphQLNonNullType]
        public string PasswordHash { get; set; }

        public static User FromModel(Model.Entities.User model)
        {
            return new User()
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PasswordHash = model.PasswordHash
            };
        }
        
        public Model.Entities.User ToModel()
        {
            return new Model.Entities.User()
            {
                Id = Id,
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                PasswordHash = PasswordHash
            };
        }
    }
}