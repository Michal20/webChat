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
        // public User user { get; set; }
        //public Contact contact { get; set; }
        //public bool isUserSend { get; set; }
        public int ConverId { get; set; }
        public Conversation Conver { get; set; }
        public string Text { get; set; }
        public DateTime sendTime { get; set; }
    }
}
