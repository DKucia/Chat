using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Api.Dtos
{
    public class CreateConversationDto 
    {
        public string Name { get; set; }
        public List<string> Usernames { get; set; }
    }
}
