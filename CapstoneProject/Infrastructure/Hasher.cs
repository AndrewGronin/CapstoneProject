using System;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace CapstoneProject.Infrastructure
{
    public static class Hasher
    {
        private static readonly SHA1CryptoServiceProvider _cryptoProvider = new();

        public static string Hash(string password)
        {
            var sha1data = _cryptoProvider.ComputeHash(Encoding.ASCII.GetBytes(password));
            return Encoding.ASCII.GetString(sha1data);
        }
    }
}