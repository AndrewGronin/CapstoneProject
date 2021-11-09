using System;
using CapstoneProject.Exceptions;
using CapstoneProject.Model.Entities;
using CapstoneProject.Services;
using HotChocolate;
using HotChocolate.Resolvers;
using HotChocolate.Types;
using Microsoft.AspNetCore.Http;
using WebApi.Models;
using User = CapstoneProject.Schema.Queries.User;

namespace CapstoneProject.Schema.Mutations
{
    [ExtendObjectType(typeof(RootMutation))]
    public class AuthorizationMutation
    {
        public User Register(
            [Service] IUserService userService
        )
        {
            return User.FromModel(userService.Create());
        }
        public AuthenticateResponse Authenticate(
            [Service]IHttpContextAccessor contextAccessor,
            [Service]IUserService userService,
            AuthenticateRequest authenticateRequest
            )
        {
            var response = userService.Authenticate(authenticateRequest, GetIpAddress(contextAccessor.HttpContext));

            if (response == null)
                throw new InvalidClientRequestException("Username or password is incorrect");

            SetTokenCookie(contextAccessor.HttpContext, response.RefreshToken);

            return response;
        }
        
        public AuthenticateResponse RefreshToken(
            [Service]IHttpContextAccessor contextAccessor,
            [Service]IUserService userService)
        {
            var refreshToken = contextAccessor.HttpContext?.Request.Cookies["refreshToken"];
            
            if (string.IsNullOrEmpty(refreshToken))
                throw new InvalidClientRequestException("RefreshToken is required");
            
            var response = userService.RefreshToken(refreshToken, GetIpAddress(contextAccessor.HttpContext));

            /*if (response == null)
                return Unauthorized(new { message = "Invalid token" });*/

            SetTokenCookie(contextAccessor.HttpContext, response.RefreshToken);

            return response;
        }

        public string RevokeToken(
            [Service]IHttpContextAccessor contextAccessor,
            [Service]IUserService userService,
            RevokeTokenRequest model)
        {
            // accept token from request body or cookie
            var token = model.Token ?? contextAccessor.HttpContext?.Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(token))
                throw new InvalidClientRequestException("Token is required");

            var response = userService.RevokeToken(token, GetIpAddress(contextAccessor.HttpContext));

            if (!response)
                throw new ResourceNotFoundException("Token not found");

            return "Revoked";
        }
        
        private void SetTokenCookie(HttpContext? context, string token)
        {
            if (context == null)
                throw new InvalidClientRequestException("HttpContext was null");
            
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            context.Response.Cookies.Append("refreshToken", token, cookieOptions);
        }
        
        private string GetIpAddress(HttpContext? context)
        {
            if (context == null)
                throw new InvalidClientRequestException("HttpContext was null");
            if (context.Request.Headers.ContainsKey("X-Forwarded-For"))
                return context.Request.Headers["X-Forwarded-For"];
            return context.Connection.RemoteIpAddress?.MapToIPv4().ToString() ?? "";
        }
    }
}