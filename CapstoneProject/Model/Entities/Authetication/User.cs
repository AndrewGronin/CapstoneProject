using System.Collections.Generic;

namespace CapstoneProject.Model.Entities
{
    public class User
    {
        public User()
        {
            RefreshTokens = new HashSet<RefreshToken>();
        }

        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public virtual ICollection<RefreshToken> RefreshTokens { get; set; }
    }
}
