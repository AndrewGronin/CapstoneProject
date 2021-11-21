using System;

#nullable disable

namespace CapstoneProject.Model.Entities
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public DateTime ExpirationDateTime { get; set; }
        public DateTime CreationDate { get; set; }
        public string CreatedByIp { get; set; }
        public DateTime? RevocationDateTime { get; set; }
        public string RevokedByIp { get; set; }
        public string ReplacedByToken { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; }
        
        public bool IsActive => RevocationDateTime == null && !IsExpired;
        public bool IsExpired => DateTime.UtcNow >= ExpirationDateTime;


    }
}
