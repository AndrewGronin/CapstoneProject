using System;

namespace CapstoneProject.Model.Exceptions
{
    public class InvalidClientRequestException : Exception
    {
        public InvalidClientRequestException(string message)
            : base(message)
        {
        }
    }
}