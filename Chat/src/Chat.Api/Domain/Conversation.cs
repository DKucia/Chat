using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Api.Domain
{
    public class Conversation
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<string> MemberUsernames { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
