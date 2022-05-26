using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Web;
using System.Linq;
using System.Collections.Generic;
namespace webChat.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string ContactId { get; set; }
        public string UserId { get; set; }
        public bool sent { get; set; }
        public string Text { get; set; }
        public DateTime sendTime { get; set; }
    }
}
