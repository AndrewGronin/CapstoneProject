using System.ComponentModel.DataAnnotations;

namespace CapstoneProject.Schema.Mutations
{
    public class AuthenticateRequest
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}