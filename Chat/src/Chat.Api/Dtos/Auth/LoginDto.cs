﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Api.Dtos.Auth
{
    
    public class LoginDto 
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
