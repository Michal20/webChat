using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Web;
using System.Linq;
using System.Collections.Generic;

namespace webChat.Models
{
    public class Conversation
    {
        public Conversation()
        {
            Messages = new List<Message>();
        }
        [Key]
        public string UserId { get; set; }
        [Key]
        public string ContactId { get; set; }
        public string Name { get; set; }
        public string Server { get; set; }
        public string Text { get; set; }
        public string ProfilePicture { get; set; }
        public DateTime sendTime { get; set; }
        public ICollection<Message> Messages { get; set; }

    }
}
