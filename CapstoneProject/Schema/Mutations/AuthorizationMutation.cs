using System;
using CapstoneProject.Schema.Infrastructure;
using CapstoneProject.Schema.Mutations.inputObjects;
using CapstoneProject.Schema.Services;
using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Resolvers;
using HotChocolate.Types;
using Microsoft.AspNetCore.Http;
using User = CapstoneProject.Schema.Queries.User;

namespace CapstoneProject.Schema.Mutations
{
    [ExtendObjectType(typeof(RootMutation))]
    public class AuthorizationMutation
    {
        public User Register(
            [Service] IUserService userService,
            InputUser inputUser
        )
        {
            return User.FromModel(userService.Create(inputUser.ToModel()));
        }
        
        public AuthenticateResponse Authenticate(
            IResolverContext resolverContext,
            [Service]IHttpContextAccessor contextAccessor,
            [Service]IUserService userService,
            AuthenticateRequest authenticateRequest
            )
        {
            var response = userService.Authenticate(authenticateRequest, resolverContext.GetIpAddress(contextAccessor.HttpContext));

            resolverContext.SetTokenCookie(contextAccessor.HttpContext, response.RefreshToken);

            return response;
        }
        
        public AuthenticateResponse RefreshToken(
            IResolverContext resolverContext,
            [Service]IHttpContextAccessor contextAccessor,
            [Service]IUserService userService,
            string refreshToken)
        {
            var response = userService.RefreshToken(refreshToken, resolverContext.GetIpAddress(contextAccessor.HttpContext));

            resolverContext.SetTokenCookie(contextAccessor.HttpContext, response.RefreshToken);

            return response;
        }

        [Authorize]
        public string RevokeToken(
            IResolverContext resolverContext,
            [Service]IHttpContextAccessor contextAccessor,
            [Service]IUserService userService,
            string refreshToken)
        {
            userService.RevokeToken(refreshToken, resolverContext.GetIpAddress(contextAccessor.HttpContext));

            return "Revoked";
        }
    }
}