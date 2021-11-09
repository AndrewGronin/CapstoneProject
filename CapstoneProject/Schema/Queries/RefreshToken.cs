using System;

namespace CapstoneProject.Schema.Queries
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public DateTime ExpirationDateTime { get; set; }

        public static RefreshToken FromModel(Model.Entities.RefreshToken model)
        {
            return new RefreshToken()
            {
                Id = model.Id,
                Token = model.Token,
                ExpirationDateTime = model.ExpirationDateTime
            };
        } 
    }
}