using System.Security.Cryptography;
using System.Text;

namespace CapstoneProject.Infrastructure
{
    public static class Hasher
    {
        private static readonly SHA1CryptoServiceProvider CryptoProvider = new();

        public static string Hash(string password)
        {
            var sha1data = CryptoProvider.ComputeHash(Encoding.ASCII.GetBytes(password));
            return Encoding.ASCII.GetString(sha1data);
        }
    }
}