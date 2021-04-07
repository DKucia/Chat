using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Api.Settings
{
    public class JwtSettings
    {
        public string Secret { get; set; }
        public int Expired { get; set; }
    }
}
