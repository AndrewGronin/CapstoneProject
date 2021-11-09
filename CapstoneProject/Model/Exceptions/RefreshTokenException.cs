using System;

namespace CapstoneProject.Model.Exceptions
{
    public class RefreshTokenException : Exception
    {
        public RefreshTokenException(string message)
            : base(message)
        {
        }
    }
}