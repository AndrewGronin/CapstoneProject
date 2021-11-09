using System.Collections.Generic;
using CapstoneProject.Exceptions;
using CapstoneProject.Services;
using HotChocolate;
using HotChocolate.Types;

namespace CapstoneProject.Schema.Queries
{
    [ExtendObjectType(typeof(RootQuery))]
    public class AuthorizationQuery
    {
        public IEnumerable<User> GetAll([Service] IUserService userService)
        {
            var users = userService.GetAll();
            return users;
        }

        public User GetById([Service] IUserService userService, int id)
        {
            var user = userService.GetById(id);
            if (user == null)
                throw new ResourceNotFoundException("");

            return user;
        }

        public IEnumerable<RefreshToken> GetRefreshTokens([Service] IUserService userService, int id)
        {
            var user = userService.GetById(id);
            if (user == null) 
                throw new ResourceNotFoundException("");

            return user.RefreshTokens;
        }
    }
}