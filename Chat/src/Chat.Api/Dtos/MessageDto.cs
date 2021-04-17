using System;

namespace Chat.Api.Dtos
{
    public class MessageDto
    {
        public string Id { get; set; }
        public string Content { get; set; }
        public string Username { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}