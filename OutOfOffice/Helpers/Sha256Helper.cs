using IdentityModel;
using System.Security.Cryptography;
using System.Text;

namespace OutOfOffice.Helpers
{
    public static class Sha256Helper
    {
        public static string ComputeHash(string rawData)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawData));
            var hash = Base64Url.Encode(bytes);
            return hash;
        }
    }  
}
