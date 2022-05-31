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
        public string id { get; set; }
        public string name { get; set; }
        public string server { get; set; }
        public string? last { get; set; }
        public DateTime lastdate { get; set; }
        public string ProfilePicture { get; set; }
        public ICollection<Message> Messages { get; set; }

    }
}
