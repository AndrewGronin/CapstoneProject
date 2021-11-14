using System;
using HotChocolate.Resolvers;
using Microsoft.AspNetCore.Http;

namespace CapstoneProject.Schema.Infrastructure
{
    public static class ResolverContextExtensions
    {
        public static void SetTokenCookie(this IResolverContext _, HttpContext? context, string token)
        {
            if (context == null)
                throw new InvalidOperationException("HttpContext was null");
            
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            context.Response.Cookies.Append("refreshToken", token, cookieOptions);
        }
        
        public static string GetIpAddress(this IResolverContext _, HttpContext? context)
        {
            if (context == null)
                throw new InvalidOperationException("HttpContext was null");
            if (context.Request.Headers.ContainsKey("X-Forwarded-For"))
                return context.Request.Headers["X-Forwarded-For"];
            return context.Connection.RemoteIpAddress?.MapToIPv4().ToString() ?? "";
        }
    }
}