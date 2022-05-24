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
        public int Id { get; set; }
        [Required, MinLength(5), Column(TypeName = "VARCHAR(20)")]
        public string Name { get; set; }
        public string ContactId { get; set; }
        public User Contact { get; set; }
        public ICollection<Message> Messages { get; set; }

    }
}
