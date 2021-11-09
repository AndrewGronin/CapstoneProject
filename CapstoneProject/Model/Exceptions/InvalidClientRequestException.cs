using System;

namespace CapstoneProject
{
    public class InvalidClientRequestException : Exception
    {
        public InvalidClientRequestException(string message)
            : base(message)
        {
        }
    }
}