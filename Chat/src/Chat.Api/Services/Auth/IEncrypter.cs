using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Api.Services.Auth
{
    public interface IEncrypter
    {
        byte[] GetHash(string password, byte[] salt);
        byte[] GenerateRandomSalt(int length = 32);
    }
}
