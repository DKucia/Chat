using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Api.Services
{
    public static class Extensions
    {
        public static string ToBase64string(this byte[] value) => Convert.ToBase64String(value);
        public static byte[] FromBase64ToBytes(this string base64) => Convert.FromBase64String(base64);
    }
}
