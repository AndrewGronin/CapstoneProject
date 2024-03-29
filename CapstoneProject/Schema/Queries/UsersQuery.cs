﻿using System.Collections.Generic;
using System.Linq;
using CapstoneProject.Model.Exceptions;
using CapstoneProject.Schema.Services;
using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Types;

namespace CapstoneProject.Schema.Queries
{
    [ExtendObjectType(typeof(RootQuery))]
    public class UsersQuery
    { 
        [Authorize]
        public IEnumerable<User> Users(
            [Service] IUserService userService,
            List<int>? ids)
        {
            IEnumerable<Model.Entities.User> modelUsers;
            modelUsers = ids != null ? userService.GetByIds(ids) : userService.GetAll();

            return modelUsers.Select(User.FromModel);
        }

        [Authorize]
        public IEnumerable<RefreshToken> GetRefreshTokens([Service] IUserService userService, int userId)
        {
            var user = userService.GetByIds(new []{userId}).Single();
            if (user == null) 
                throw new ResourceNotFoundException($"User with id = {userId} wasn't found");

            return user.RefreshTokens.Select(RefreshToken.FromModel);
        }
    }
}