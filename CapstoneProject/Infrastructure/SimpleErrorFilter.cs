using System;
using HotChocolate;

namespace CapstoneProject.Infrastructure
{
    public class SimpleErrorFilter : IErrorFilter
    {
        public IError OnError(IError error)
        {
            if(error.Code != "AUTH_NOT_AUTHENTICATED" && error.Exception?.GetType() != typeof(InvalidOperationException))
                return error
                    .WithMessage(error.Exception?.Message ?? "")
                    .WithCode(error.Exception?.GetType().ToString() ?? "UNCATEGORIZED");
            return error;
        }
    }
}