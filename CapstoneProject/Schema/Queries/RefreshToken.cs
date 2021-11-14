#nullable disable
using System;
using HotChocolate;

namespace CapstoneProject.Schema.Queries
{
    public class RefreshToken
    {
        public int Id { get; set; }
        
        [GraphQLNonNullType]
        public string Token { get; set; }
        public DateTime ExpirationDateTime { get; set; }
        
        public DateTime CreationDate { get; set; }
        
        public string CreatedByIp { get; set; }
        
        public DateTime? RevocationDateTime { get; set; }
        
        public string RevokedByIp { get; set; }
        
        public string ReplacedByToken { get; set; }

        public static RefreshToken FromModel(Model.Entities.RefreshToken model)
        {
            return new RefreshToken()
            {
                Id = model.Id,
                Token = model.Token,
                ExpirationDateTime = model.ExpirationDateTime,
                CreationDate = model.CreationDate,
                CreatedByIp = model.CreatedByIp,
                RevocationDateTime = model.RevocationDateTime,
                RevokedByIp = model.RevokedByIp,
                ReplacedByToken = model.ReplacedByToken
            };
        } 
    }
}