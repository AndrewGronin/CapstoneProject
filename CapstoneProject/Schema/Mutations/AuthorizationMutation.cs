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
            //var refreshToken = contextAccessor.HttpContext?.Request.Cookies["refreshToken"];
            
            /*if (string.IsNullOrEmpty(refreshToken))
                throw new InvalidClientRequestException("RefreshToken is required");*/
            
            var response = userService.RefreshToken(refreshToken, resolverContext.GetIpAddress(contextAccessor.HttpContext));

            resolverContext.SetTokenCookie(contextAccessor.HttpContext, response.RefreshToken);

            return response;
        }

        [Authorize]
        public string RevokeToken(
            IResolverContext resolverContext,
            [Service]IHttpContextAccessor contextAccessor,
            [Service]IUserService userService,
            RevokeTokenRequest model)
        {
            // accept token from request body or cookie
            var token = model.Token ?? contextAccessor.HttpContext?.Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(token))
                throw new InvalidOperationException("Token is required");

            userService.RevokeToken(token, resolverContext.GetIpAddress(contextAccessor.HttpContext));

            return "Revoked";
        }
    }
}