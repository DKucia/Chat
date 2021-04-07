using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Api.Services.Auth
{
    public class Encrypter : IEncrypter
    {
        public byte[] GetHash(string password, byte[] salt)
        {
            using (var hashAlg = new HMACSHA256(salt))
            {
                return hashAlg.ComputeHash(Encoding.UTF8.GetBytes(password));
            };

        }

        public byte[] GenerateRandomSalt(int length = 32)
        {
            using (var random = new RNGCryptoServiceProvider())
            {
                var salt = new byte[length];
                random.GetNonZeroBytes(salt);
                return salt;
            }
        }
    }
}
